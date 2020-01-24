using Test.Db.Model.RLS;
using Test.Db.RLS;
using Test.Db.Stepper.Model;
using Test.Db.Stepper.Model.Workflow;
using Test.Db.Interface;

namespace Test.Db.Stepper.StateChanges
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
                HasPermissionToFullfillChange = new SecurityObject[]
                {
                    new SecurityIdentity { Id = currentUserProvider.Identity.Id },
                },
                TaskAssignType = TaskAssignType.AssignOne
            };
        }
    }
}
