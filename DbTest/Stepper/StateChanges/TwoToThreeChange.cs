using DbTest.Model.RLS;
using DbTest.RLS;
using DbTest.Stepper.Model;
using DbTest.Stepper.Model.Workflow;
using DbTest.Interface;

namespace DbTest.Stepper.StateChanges
{
    public class TwoToThreeChange : StateChange<TestEntityModel, TestEntityStates>, IProducesTask<BasicTask>
    {
        private readonly CurrentUserProvider currentUserProvider;

        public TwoToThreeChange(CurrentUserProvider currentUserProvider)
        {
            this.currentUserProvider = currentUserProvider;
        }

        public long[] AssignTaskToSecurityObjects()
        {
            return new long[] { currentUserProvider.Identity.Id };
        }

        public void AlterTasks(BasicTask task)
        {

        }

        public override StateChangeConfiguration GetConfiguration()
        {
            return new StateChangeConfiguration
            {
                AllowedEndStates = new System.Enum[]
                {
                    TestEntityStates.ThirdPlace
                },
                AllowedStartStates = new System.Enum[]
                {
                    TestEntityStates.SecondPlace
                },
                HasPermissionToFullfillChange = new Model.RLS.SecurityObject[]
                {
                    new SecurityIdentity { Id = currentUserProvider.Identity.Id },
                },
                TaskAssignType = TaskAssignType.AssignOne
            };
        }
    }
}
