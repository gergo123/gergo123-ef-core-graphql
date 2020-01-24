using Test.Db.Repositories.GeneralRepository.Stepper;
using Test.Db.RLS;
using Test.Db.Stepper.Model;
using Test.Db.Stepper.Model.Workflow;
using Test.Db.Stepper.ViewModel;
using Test.Db.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Db.Stepper.StateChanges
{
    public class NoneToTestChange : StateChangeHasTackPrerequityState<TestEntityModel, TestEntityStates, FourToFiveVM>, IProducesTask<BasicTask>
    {
        private readonly CurrentUserProvider currentUserProvider;

        public NoneToTestChange(IBasicTaskRepository basicTaskRepository, CurrentUserProvider currentUserProvider) : base(basicTaskRepository)
        {
            this.currentUserProvider = currentUserProvider;
        }

        public void AlterTasks(BasicTask task)
        {
            //throw new NotImplementedException();
        }

        public long[] AssignTaskToSecurityObjects()
        {
            return new long[] { currentUserProvider.Identity.Id };
        }

        public override StateChangeConfiguration GetConfiguration()
        {
            return new StateChangeConfiguration
            {
                AllowedStartStates = new Enum[]
                {
                    TestEntityStates.@default
                },
                AllowedEndStates = new Enum[]
                {
                    TestEntityStates.SecondPlace
                },
                HasPermissionToFullfillChange = new Test.Db.Model.RLS.SecurityObject[] { currentUserProvider.Identity },
                TaskAssignType = TaskAssignType.AssignOne
            };
        }
    }
}
