using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbTest.Interfaces.Repositories;
using DbTest.RLS;
using Microsoft.EntityFrameworkCore;
using DbTest.Stepper.Model.Workflow;
using DbTest.Model;

namespace DbTest.Repositories.SecureRepository.Stepper
{
    public class BasicTaskSecureRepository : RLSRepositoryBase<BasicTask, BasicTaskAcl>, IBasicTaskSecureRepository
    {
        public IBasicTaskAclRepository ACL;
        public BasicTaskSecureRepository(CoreContext context, IUserSecurityObjectsHandler securityObjects, IBasicTaskAclRepository aclRepo) :
            base(context, securityObjects)
        {
            ACL = aclRepo;
        }
    }

    public interface IBasicTaskSecureRepository : IRLSRepository<BasicTask, BasicTaskAcl>
    {
    }
}
