using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Db.Stepper.Model.Workflow;
using Microsoft.EntityFrameworkCore;
using Test.Db.Model;

namespace Test.Db.Repositories.SecureRepository.Stepper
{
    public class BasicTaskAclRepository : Interfaces.Repositories.Repository<BasicTaskAcl>, IBasicTaskAclRepository
    {
        private Model.CoreContext Context;
        public BasicTaskAclRepository(CoreContext context) : base(context)
        {
            {
            }
        }
    }

    public interface IBasicTaskAclRepository : Interfaces.Repositories.IEntityRepository<BasicTaskAcl>
    {
    }
}