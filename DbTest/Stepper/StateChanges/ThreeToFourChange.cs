using DbTest.Model.RLS;
using DbTest.Repositories.GeneralRepository.Stepper;
using DbTest.RLS;
using DbTest.Stepper.Model;
using DbTest.Stepper.Model.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTest.Stepper.StateChanges
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
                HasPermissionToFullfillChange = new Model.RLS.SecurityObject[]
                {
                    new SecurityIdentity { Id = CurrentUserProvider.Identity.Id },
                }
            };
        }
    }
}
