using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbTest.Model;
using DbTest.Stepper.Model.Workflow;
using Microsoft.EntityFrameworkCore;

namespace DbTest.Repositories.SecureRepository.Stepper
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