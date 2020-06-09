using DbTest.Model.RLS;
using DbTest.RLS;
using DbTest.Stepper.Model;
using DbTest.Stepper.Model.Workflow;

namespace DbTest.Stepper.StateChanges
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
                HasPermissionToFullfillChange = new Model.RLS.SecurityObject[]
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
