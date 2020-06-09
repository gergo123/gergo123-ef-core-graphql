using DbTest.Model.RLS;
using DbTest.Stepper.Model;
using DbTest.Stepper.Model.Workflow;
using DbTest.Stepper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTest.Stepper.StateChanges
{
    public class FourToFiveChange : StateChange<TestEntityModel, TestEntityStates, FourToFiveVM>
    {
        public FourToFiveChange() : base()
        {
        }

        public override void IsValid(FourToFiveVM model)
        {
            if (model.FailChange)
            {
                throw new StateTransitionException("Hiba történt bla bla bla");
            }
        }

        public override void StateAction(FourToFiveVM model)
        {
            if (model.FailChange)
            {
                //throw new StateTransitionException("Hiba történt bla bla bla");
            }
        }

        public override StateChangeConfiguration GetConfiguration()
        {
            return new StateChangeConfiguration
            {
                AllowedEndStates = new Enum[]
                {
                    TestEntityStates.FifthPlace
                },
                HasPermissionToFullfillChange = new Model.RLS.SecurityObject[]
                {
                    new SecurityIdentity { Id = 3 },
                    new SecurityIdentity { Id = 1 }
                }
            };
        }
    }
}
