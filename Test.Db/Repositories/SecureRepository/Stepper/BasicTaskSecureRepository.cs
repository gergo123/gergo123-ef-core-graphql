using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Db.Interfaces.Repositories;
using Test.Db.RLS;
using Microsoft.EntityFrameworkCore;
using Test.Db.Stepper.Model.Workflow;
using Test.Db.Model;

namespace Test.Db.Repositories.SecureRepository.Stepper
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
