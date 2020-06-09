using DbTest.Interface.Stepper;
using DbTest.Repositories.GeneralRepository.Stepper;
using DbTest.Stepper;
using DbTest.Stepper.Model.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTest.Stepper
{
    public abstract class StateChangeHasTackPrerequityState<T, C, L> : StateChangeHasTackPrerequityState<T, C>, IStateChange<T, C, L>
        where T : class, IStateChangeEntityBase<C>
        where C : Enum
        where L : class
    {
        public StateChangeHasTackPrerequityState(IBasicTaskRepository basicTaskRepository) : base(basicTaskRepository)
        {
        }

        public void IsValid(L model)
        {
        }

        public void StateAction(L model)
        {
        }
    }

    public abstract class StateChange<T, C, L> : StateChange<T, C>, IStateChange<T, C, L>
    where T : class, IStateChangeEntityBase<C>
    where C : Enum
    where L : class
    {
        public virtual void StateAction(L model)
        {
        }

        public virtual void IsValid(L model)
        {
        }
    }

    public interface IStateChange<T, C, L> : IStateChange<T, C>, IStateChangeWithViewModel<L>
        where T : class, IStateChangeEntityBase<C>
        where C : Enum
        where L : class
    {

    }

    public abstract class StateChangeHasTackPrerequityState<T, C> : StateChange<T, C>, IHasTaskPrerequity
        where T : class, IStateChangeEntityBase<C>
        where C : Enum
    {
        public StateChangeHasTackPrerequityState(IBasicTaskRepository basicTaskRepository)
        {
            TasksStore = basicTaskRepository.GetAll();
        }

        /// <summary>
        /// A unique identifier used to identify tasks connected to the tasks
        ///  those are that have been made after state change
        /// </summary>
        private string NameOfPreviousObject { get; set; }
        private IQueryable<ITask> TasksStore { get; set; }

        public ITask[] getCreatedTask()
        {
            if (TasksStore == null)
            {
                throw new Exception($"Empty {nameof(TasksStore)} on the change object!");
            }
            if (string.IsNullOrEmpty(NameOfPreviousObject))
            {
                throw new Exception($"Empty {nameof(NameOfPreviousObject)} on the change object! " +
                    $"Call {nameof(SetPreviousStep)} with previous change name!");
            }
            var tasksToCheck = TasksStore.Where(x =>
                x.EntityId == Entity.Id &&
                x.UniqueID == NameOfPreviousObject);

            return tasksToCheck.ToArray();
        }

        public void SetPreviousStep(string nameOfPreviousObject)
        {
            NameOfPreviousObject = nameOfPreviousObject;
        }
    }

    /// <summary>
    /// Két állapot közötti átmenet base osztály.
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <typeparam name="C">Enum type</typeparam>
    public abstract class StateChange<T, C> : IStateChange<T, C>
        where T : class, IStateChangeEntityBase<C>
        where C : Enum
    {
        /// <summary>
        /// Can be used for validation
        /// </summary>
        public T Entity { get; set; }

        public StateChange()
        {
        }

        /// <summary>
        /// This method needs to be seperate from the constructor because of dependency injection.
        /// </summary>
        /// <param name="entity"></param>
        public void SetEntity(T entity)
        {
            Entity = entity;
        }

        public virtual StateChangeConfiguration GetConfiguration()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Decides whether the transition should be fullfilled or not. Before status change.
        /// </summary>
        /// <param name="message">Optional validation message. A StateTransitionException exception will be thrown with the message.</param>
        /// <returns></returns>
        public virtual void IsValid()
        {
        }

        /// <summary>
        /// Called after status change. Should perform every action related to the transition.
        /// </summary>
        public virtual void StateAction()
        {
        }
    }

    public interface IStateChangeWithViewModel<L>
        where L : class
    {
        void StateAction(L model);

        void IsValid(L model);
    }

    public interface IStateChange<T, C>
        where T : class, IStateChangeEntityBase<C>
        where C : Enum
    {
        T Entity { get; set; }
        StateChangeConfiguration GetConfiguration();
        void IsValid();
        void StateAction();
    }
}
