using Test.Db.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Db.RLS;
using Microsoft.EntityFrameworkCore;
using Test.Db.Model.Placeholder;
using Test.Db.Model;

namespace Test.Db.SecureRepository.PlaceHolder
{
    public class PlaceholderSecureRepository : RLSRepositoryBase<PlaceholderEntity, PlaceholderEntityACL>, IPlaceholderSecureRepository
    {
        public IPlaceholderACLRepository ACL;
        public PlaceholderSecureRepository(CoreContext context, IUserSecurityObjectsHandler securityObjects, IPlaceholderACLRepository aclRepo) :
            base(context, securityObjects)
        {
            ACL = aclRepo;
        }
    }

    public interface IPlaceholderSecureRepository : IRLSRepository<PlaceholderEntity, PlaceholderEntityACL>
    {
    }
}
