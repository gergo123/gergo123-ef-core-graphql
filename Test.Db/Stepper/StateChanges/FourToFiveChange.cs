using Test.Db.Model.RLS;
using Test.Db.Stepper.Model;
using Test.Db.Stepper.Model.Workflow;
using Test.Db.Stepper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Db.Stepper.StateChanges
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
                throw new StateTransitionException("Some kind of validation error happened bla bla bla");
            }
        }

        public override void StateAction(FourToFiveVM model)
        {
            if (model.FailChange)
            {
                //throw new StateTransitionException("Some kind of validation error happened bla bla bla");
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
                HasPermissionToFullfillChange = new SecurityObject[]
                {
                    new SecurityIdentity { Id = 3 },
                    new SecurityIdentity { Id = 2 },
                    new SecurityIdentity { Id = 1 }
                }
            };
        }
    }
}
