using Test.Db.Model;
using Test.Db.Stepper.Model.Workflow;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Db.Repositories.GeneralRepository.Stepper
{
    public class BasicTaskRepository : Interfaces.Repositories.Repository<BasicTask>, IBasicTaskRepository
    {
        private readonly CoreContext Context;
        public BasicTaskRepository(CoreContext context) : base(context)
        {
            Context = context;
        }
    }

    public interface IBasicTaskRepository : Interfaces.Repositories.IEntityRepository<BasicTask>
    {
    }
}
