using Test.Db.RLS;
using Test.Db.Repositories.SecureRepository.Stepper;
using Test.Db.Repositories.GeneralRepository.Stepper;
using Test.Db.Model.RLS;
using Test.Db.Stepper.Model;
using Test.Db.Stepper.Model.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Db.Stepper.StateChanges
{
    public class ThreeToFourChange : StateChangeHasTackPrerequityState<TestEntityModel, TestEntityStates>
    {
        public CurrentUserProvider CurrentUserProvider { get; }

        public ThreeToFourChange(IBasicTaskRepository taskSecureStore, CurrentUserProvider currentUserProvider) :
            base(taskSecureStore)
        {
            CurrentUserProvider = currentUserProvider;
        }

        public override StateChangeConfiguration GetConfiguration()
        {
            return new StateChangeConfiguration
            {
                AllowedEndStates = new Enum[]
                {
                    TestEntityStates.FourthPlace
                },
                HasPermissionToFullfillChange = new SecurityObject[]
                {
                    new SecurityIdentity { Id = CurrentUserProvider.Identity.Id },
                }
            };
        }
    }
}
