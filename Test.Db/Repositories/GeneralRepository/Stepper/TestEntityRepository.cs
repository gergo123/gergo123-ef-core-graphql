﻿using Test.Db.Stepper.Model.Workflow;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Db.Model;

namespace Test.Db.Repositories.GeneralRepository.Stepper
{
    public class TestEntityRepository : Interfaces.Repositories.Repository<TestEntityModel>, ITestEntityRepository
    {
        private readonly Model.CoreContext Context;
        public TestEntityRepository(CoreContext context) : base(context)
        {
            Context = context;
        }
    }

    public interface ITestEntityRepository : Interfaces.Repositories.IEntityRepository<TestEntityModel>
    {
    }
}
