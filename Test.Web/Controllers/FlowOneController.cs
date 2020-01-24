﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Test.Db.Repositories.GeneralRepository.Stepper;
using Test.Db.Repositories.SecureRepository.Stepper;
using Test.Db.RLS;
using Test.Db.Stepper;
using Test.Db.Stepper.Model;
using Test.Db.Stepper.Model.Workflow;
using Test.Db.Stepper.StateChanges;
using Test.Db.Stepper.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    public class FlowOneController : Controller
    {
        public IStateManagger StateManagger { get; }
        public StateChangeFactory StateChangeFactory { get; }
        public ITestEntityRepository TestEntityRepository { get; }
        public IBasicTaskSecureRepository secureBasicTaskRepository { get; }
        public CurrentUserProvider CurrentUserProvider { get; }
        public IUserSecurityObjectsHandler UserSecurityObjectsHandler { get; }

        public FlowOneController(IStateManagger stateManagger, StateChangeFactory stateChangeFactory,
            ITestEntityRepository testEntityRepository, IBasicTaskSecureRepository basicTaskRepository,
            CurrentUserProvider currentUserProvider, IUserSecurityObjectsHandler userSecurityObjectsHandler)
        {
            StateManagger = stateManagger;
            StateChangeFactory = stateChangeFactory;
            TestEntityRepository = testEntityRepository;
            this.secureBasicTaskRepository = basicTaskRepository;
            CurrentUserProvider = currentUserProvider;
            UserSecurityObjectsHandler = userSecurityObjectsHandler;
        }

        [HttpPost]
        public IActionResult Create([FromBody]string message)
        {
            var entity = new TestEntityModel
            {
                Message = message
            };
            TestEntityRepository.Add(entity);
            TestEntityRepository.SaveChanges();

            return Json(new { entity.Id });
        }

        [HttpPost]
        public IActionResult StepOne(long entityId)
        {
            var entity = TestEntityRepository.GetById(entityId);
            var change = StateChangeFactory.CreateChange(typeof(OneToTwoChange)) as OneToTwoChange;
            change.SetEntity(entity);

            StateManagger.ManageTransition(change, TestEntityStates.SecondPlace);
            TestEntityRepository.SaveChanges();

            return Json(new { });
        }

        [HttpPost]
        public IActionResult StepTwo(long entityId)
        {
            var entity = TestEntityRepository.GetById(entityId);
            var change = StateChangeFactory.CreateChange(typeof(TwoToThreeChange)) as TwoToThreeChange;
            change.SetEntity(entity);

            var tasks = StateManagger.ManageTransition<TestEntityModel, TestEntityStates, BasicTask, BasicTaskAcl>(change, TestEntityStates.ThirdPlace, secureBasicTaskRepository);
            TestEntityRepository.SaveChanges();

            return Json(new { ids = tasks.Select(x => x.Id) });
        }

        [HttpPost]
        public IActionResult StepThree(long entityId)
        {
            var entity = TestEntityRepository.GetById(entityId);
            var change = StateChangeFactory.CreateChange(typeof(ThreeToFourChange)) as ThreeToFourChange;
            change.SetEntity(entity);
            change.SetPreviousStep(typeof(TwoToThreeChange).FullName);

            StateManagger.ManageTransition<TestEntityModel, TestEntityStates, BasicTask, BasicTaskAcl>(change, TestEntityStates.FourthPlace, secureBasicTaskRepository);
            TestEntityRepository.SaveChanges();

            return Json(new { });
        }

        [HttpPost]
        public IActionResult StepFour(long entityId, [FromBody]FourToFiveVM fourToFiveVM)
        {
            var entity = TestEntityRepository.GetById(entityId);
            var change = StateChangeFactory.CreateChange(typeof(FourToFiveChange)) as FourToFiveChange;
            change.SetEntity(entity);

            StateManagger.ManageTransition(change, TestEntityStates.FifthPlace, fourToFiveVM);
            TestEntityRepository.SaveChanges();

            return Json(new { });
        }

        [HttpPost]
        public IActionResult FinishTask(long taskId)
        {
            var task = secureBasicTaskRepository.GetById(taskId);
            StateManagger.ChangeStatusTask(task, CurrentUserProvider.Identity, TaskStatus.Approved);
            secureBasicTaskRepository.SaveChanges();

            return Json(new { });
        }

        public IActionResult GetTasks()
        {
            var tasks = secureBasicTaskRepository.GetAll();

            return Json(new { tasks });
        }

        public IActionResult GetById(long entityId)
        {
            var entity = TestEntityRepository.GetById(entityId);
            if (entity == null)
            {
                throw new Exception($"Record not found! id: {entityId}");
            }

            return Json(new { entity });
        }

        public IActionResult GetSpecificTasks(long entityId, int changeId = 1)
        {
            var changeUniqueId = string.Empty;
            switch (changeId)
            {
                case 1: changeUniqueId = typeof(TwoToThreeChange).FullName; break;
            }
            var tasks = secureBasicTaskRepository.Find(x => x.EntityId == entityId && x.UniqueID == changeUniqueId);
            foreach (var task in tasks)
            {
                task.ApprovedBySecurityIdentity.AssignedTasks = new List<BasicTask>();
            }

            return Json(new { tasks });
        }
    }
}
