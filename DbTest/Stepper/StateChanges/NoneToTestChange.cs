﻿using DbTest.Interface;
using DbTest.Repositories.GeneralRepository.Stepper;
using DbTest.RLS;
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
                HasPermissionToFullfillChange = new Model.RLS.SecurityObject[] { currentUserProvider.Identity },
                TaskAssignType = TaskAssignType.AssignOne
            };
        }
    }
}
