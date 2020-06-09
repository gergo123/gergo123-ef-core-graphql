using DbTest.Interface;
using DbTest.Interface.Stepper;
using DbTest.Interfaces.Repositories;
using DbTest.Interfaces.RLS;
using DbTest.Interfaces.Stepper;
using DbTest.Model.RLS;
using DbTest.RLS;
using DbTest.Stepper;
using DbTest.Stepper.Model;
using DbTest.Stepper.Model.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbTest.Stepper
{
    public class StateManagger : IStateManagger
    {
        /// <summary>
        /// Contains current user securityObject ids
        ///  current user identity id and groups user has
        /// </summary>
        private long[] CurrentUserGoups;

        /// <summary>
        /// Should contain the users every securityObjectID. Group ids and its own id.
        /// </summary>
        /// <param name="userGroups"></param>
        public StateManagger(IUserSecurityObjectsHandler userSecurityObjectIdsHandler)
        {
            CurrentUserGoups = userSecurityObjectIdsHandler.SecurityObjects.ToArray();
        }

        /// <summary>
        /// !!! Calls SaveChanges on <see cref="IRLSRepository{Entity, ACLEntity}"/>
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <typeparam name="C">State</typeparam>
        /// <typeparam name="K">Task entity</typeparam>
        /// <param name="change"></param>
        public K[] ManageTransition<T, C, K, AclEntity>(IStateChange<T, C> change, C newState, IRLSRepository<K, AclEntity> RlsRepository)
            where T : class, IStateChangeEntityBase<C>
            where C : Enum
            where K : TaskEntity, new()
            where AclEntity : class, IACLEntity, new()
        {
            CheckCorrectOverloadForViewModel(change);
            BasicWorkFlowManage(change, newState);

            change.IsValid();
            change.StateAction();

            var tasks = TaskCreationManage<T, C, K, AclEntity>(change, RlsRepository);
            ChangeStatus(change, newState);
            return tasks;
        }

        public void ManageTransition<T, C>(IStateChange<T, C> change, C newState)
            where T : class, IStateChangeEntityBase<C>
            where C : Enum
        {
            CheckCorrectOverloadForViewModel(change);
            CheckCorrectOverloadForTasks(change);
            BasicWorkFlowManage(change, newState);

            change.IsValid();
            change.StateAction();

            ChangeStatus(change, newState);
        }

        public K[] ManageTransition<T, C, K, L, AclEntity>(IStateChange<T, C, L> change, C newState, L model, IRLSRepository<K, AclEntity> RlsRepository)
            where T : class, IStateChangeEntityBase<C>
            where C : Enum
            where K : TaskEntity, new()
            where L : class
            where AclEntity : class, IACLEntity, new()
        {
            BasicWorkFlowManage(change, newState);

            change.IsValid(model);
            change.StateAction(model);

            var tasks = TaskCreationManage<T, C, K, AclEntity>(change, RlsRepository);
            ChangeStatus(change, newState);
            return tasks;
        }

        public void ManageTransition<T, C, L>(IStateChange<T, C, L> change, C newState, L model)
            where T : class, IStateChangeEntityBase<C>
            where C : Enum
            where L : class
        {
            CheckCorrectOverloadForTasks(change);

            BasicWorkFlowManage(change, newState);
            change.IsValid(model);
            change.StateAction(model);

            ChangeStatus(change, newState);
        }

        private void CheckCorrectOverloadForViewModel<T, C>(IStateChange<T, C> change)
            where T : class, IStateChangeEntityBase<C>
            where C : Enum
        {
            if (change.GetType().BaseType.GenericTypeArguments.Length == 3)
            {
                throw new Exception($"Wront overload method! Change has a third ViewModel generic type! Use the other overload method of {nameof(BasicWorkFlowManage)} " +
                    $"or remove the view model from the generic type arguments of change basetype");
            }
        }

        private void CheckCorrectOverloadForTasks<T, C>(IStateChange<T, C> change)
            where T : class, IStateChangeEntityBase<C>
            where C : Enum
        {
            var implementedInterfaces = change.GetType().GetInterfaces().Select(x => x.Name);
            if (implementedInterfaces.Any(x => x.IndexOf("IProducesTask") == 0))
            {
                throw new Exception($"Change implements {nameof(IProducesTask<TaskEntity>)} interface! Use the other overload method of {nameof(BasicWorkFlowManage)} " +
                    $"or remove interface from change!");
            }

            if (implementedInterfaces.Any(x => x.IndexOf("IHasTaskPrerequity") == 0))
            {
                throw new Exception($"Change implements {nameof(IHasTaskPrerequity)} interface! Use the other overload method of {nameof(BasicWorkFlowManage)} " +
                    $"or remove interface from change!");
            }
        }

        private K[] TaskCreationManage<T, C, K, AclEntity>(IStateChange<T, C> change, IRLSRepository<K, AclEntity> RlsRepository)
            where T : class, IStateChangeEntityBase<C>
            where C : Enum
            where K : TaskEntity, new()
            where AclEntity : class, IACLEntity, new()
        {
            K[] CreateTasksIfNeeded()
            {
                if (change is IProducesTask<K> taskChange)
                {
                    var config = change.GetConfiguration();
                    var tasksAdded = new List<K>();
                    var acls = new List<AclEntity>();
                    var secObjectsToAssignTasksTo = taskChange.AssignTaskToSecurityObjects();
                    if (config.TaskAssignType == TaskAssignType.Default)
                    {
                        throw new Exception($"{nameof(config.TaskAssignType)} is default! Please set up the setting correctly!");
                    }
                    if (config.TaskAssignType != TaskAssignType.Default)
                    {
                        if (config.TaskAssignType == TaskAssignType.AssignMany)
                        {
                            foreach (var securityObject in secObjectsToAssignTasksTo)
                            {
                                var task = new K
                                {
                                    EntityId = change.Entity.Id,
                                    Status = TaskStatus.Pending,
                                    UniqueID = change.GetType().FullName
                                };
                                tasksAdded.Add(task);
                            }
                        }
                        else
                        {
                            var task = new K
                            {
                                EntityId = change.Entity.Id,
                                Status = TaskStatus.Pending,
                                UniqueID = change.GetType().FullName
                            };
                            tasksAdded.Add(task);
                        }
                    }

                    foreach (var task in tasksAdded)
                    {
                        RlsRepository.Add(task);
                    }
                    // Must call this, to let the tasks have a generated id
                    RlsRepository.SaveChanges();

                    foreach (var task in tasksAdded)
                    {
                        if (config.TaskAssignType == TaskAssignType.AssignMany)
                        {
                            foreach (var user in secObjectsToAssignTasksTo)
                            {
                                acls.Add(new AclEntity
                                {
                                    EntityID = task.Id,
                                    Permission = PermissionEnum.Full,
                                    SecurityObjectID = user
                                });
                            }
                        }
                        else
                        {
                            acls.Add(new AclEntity
                            {
                                EntityID = task.Id,
                                Permission = PermissionEnum.Full,
                                SecurityObjectID = secObjectsToAssignTasksTo.First()
                            });
                        }
                    }
                    RlsRepository.AddAcls(acls.ToArray());
                    RlsRepository.SaveChanges();

                    return tasksAdded.ToArray();
                }

                return null;
            }
            void CheckForTasksIfNeeded()
            {
                if (change is IHasTaskPrerequity changeHasTasks)
                {
                    var tasksToCheck = changeHasTasks.getCreatedTask();
                    if (tasksToCheck.Any(x => x.Status == TaskStatus.Pending))
                    {
                        throw new StateTransitionException($"Can't performe state transition because there are tasks to be finished! " +
                            $"{string.Join(", ", tasksToCheck.Where(x => x.Status == TaskStatus.Pending).Select(x => x.Id))}");
                    }
                }
            }
            void CallAlterTasks(K[] tasks2)
            {
                if (change is IValidateTasks<K> validateTask && (tasks2 != null))
                {
                    foreach (var task in tasks2)
                    {
                        validateTask.AlterTasks(task);
                    }
                }
            }

            var tasks = CreateTasksIfNeeded();

            CheckForTasksIfNeeded();

            CallAlterTasks(tasks);

            return tasks;
        }

        private void BasicWorkFlowManage<T, C>(IStateChange<T, C> change, C newState)
            where T : class, IStateChangeEntityBase<C>
            where C : Enum
        {
            void HasSufficentPermission()
            {
                var changeConfiguration = change.GetConfiguration();
                if (changeConfiguration != null &&
                    changeConfiguration.HasPermissionToFullfillChange != null &&
                    changeConfiguration.HasPermissionToFullfillChange.Length > 0)
                {
                    if (changeConfiguration.HasPermissionToFullfillChange.Any(x => CurrentUserGoups.Contains(x.Id)))
                    {
                        return;
                    }
                    else
                    {
                        throw new StateTransitionException($"User has no permission to perform this transition. Change type: {change.GetType().Name}");
                    }
                }
                else
                {
                    throw new Exception($"Invalid {nameof(StateChangeConfiguration)}. Override {nameof(change.GetConfiguration)} properly!");
                }
            }
            void StateTransitionValid()
            {
                var changeConfiguration = change.GetConfiguration();
                if (changeConfiguration == null)
                {
                    throw new Exception($"Invalid {nameof(StateChangeConfiguration)} object!");
                }

                if (changeConfiguration.AllowedStartStates != null &&
                    changeConfiguration.AllowedStartStates.Length > 0)
                {
                    if (!changeConfiguration.AllowedStartStates.Contains(change.Entity.CurrentState))
                    {
                        throw new StateTransitionException($"Tried transition from {change.Entity.CurrentState} to {newState}, which is disallowed! " +
                            $"(possible state change outside of {nameof(StateManagger)}");
                    }
                }

                if (changeConfiguration.AllowedEndStates != null &&
                    changeConfiguration.AllowedEndStates.Length > 0)
                {
                    if (!changeConfiguration.AllowedEndStates.Contains(newState))
                    {
                        throw new StateTransitionException($"Tried transition to '{newState}', which is disallowed end state!");
                    }
                }
            }
            void TestChangeRule()
            {
                change.IsValid();
            }

            HasSufficentPermission();

            StateTransitionValid();

            TestChangeRule();
        }

        private void ChangeStatus<T, C>(IStateChange<T, C> change, C newState)
            where T : class, IStateChangeEntityBase<C>
            where C : Enum
        {
            change.Entity.CurrentState = newState;
        }

        public void ChangeStatusTask(ITask[] tasks, SecurityIdentity securityObject, TaskStatus @enum)
        {
            foreach (var task in tasks)
            {
                finishTask(task, securityObject, @enum);
            }
        }

        public void ChangeStatusTask(ITask task, SecurityIdentity securityObject, TaskStatus @enum)
        {
            finishTask(task, securityObject, @enum);
        }

        private void finishTask(ITask task, SecurityIdentity securityObject, TaskStatus @enum)
        {
            task.Status = @enum;
            task.ChangeStatus(securityObject, @enum);
        }
    }

    public interface IStateManagger
    {
        void ManageTransition<T, C>(IStateChange<T, C> change, C newState)
            where T : class, IStateChangeEntityBase<C>
            where C : Enum;
        K[] ManageTransition<T, C, K, AclEntity>(IStateChange<T, C> change, C newState, IRLSRepository<K, AclEntity> RlsRepository)
            where T : class, IStateChangeEntityBase<C>
            where C : Enum
            where K : TaskEntity, new()
            where AclEntity : class, IACLEntity, new();

        // ---------------- Action with Model ---------------------
        K[] ManageTransition<T, C, K, L, AclEntity>(IStateChange<T, C, L> change, C newState, L model, IRLSRepository<K, AclEntity> RlsRepository)
            where T : class, IStateChangeEntityBase<C>
            where C : Enum
            where K : TaskEntity, new()
            where L : class
            where AclEntity : class, IACLEntity, new();
        void ManageTransition<T, C, L>(IStateChange<T, C, L> change, C newState, L model)
            where T : class, IStateChangeEntityBase<C>
            where C : Enum
            where L : class;

        /// <summary>
        /// Task state change
        /// </summary>
        /// <param name="task"></param>
        /// <param name="securityObject">Current state changing user</param>
        /// <param name="newStatus">New state of the task</param>
        void ChangeStatusTask(ITask[] task, SecurityIdentity securityObject, TaskStatus newStatus);
        void ChangeStatusTask(ITask task, SecurityIdentity securityObject, TaskStatus newStatus);
    }
}
