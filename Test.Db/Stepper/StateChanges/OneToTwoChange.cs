using Test.Db.Model.RLS;
using Test.Db.RLS;
using Test.Db.Stepper.Model;
using Test.Db.Stepper.Model.Workflow;

namespace Test.Db.Stepper.StateChanges
{
    public class OneToTwoChange : StateChange<TestEntityModel, TestEntityStates>
    {
        private CurrentUserProvider currentUserProvider { get; }
        public OneToTwoChange(CurrentUserProvider currentUserProvider) : base()
        {
            this.currentUserProvider = currentUserProvider;
        }

        public override StateChangeConfiguration GetConfiguration()
        {
            return new StateChangeConfiguration
            {
                AllowedEndStates = new System.Enum[]
                {
                    TestEntityStates.SecondPlace
                },
                HasPermissionToFullfillChange = new SecurityObject[]
                {
                    new SecurityIdentity { Id = currentUserProvider.Identity.Id }
                }
            };
        }

        public void additionalLogic()
        {
            //throw new NotImplementedException();
        }
    }
}
