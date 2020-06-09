using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DbTest.Interfaces.Repositories;
using DbTest.RLS;
using DbTest.Model.Placeholder;
using DbTest.Model;

namespace DbTest.SecureRepository.PlaceHolder
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
