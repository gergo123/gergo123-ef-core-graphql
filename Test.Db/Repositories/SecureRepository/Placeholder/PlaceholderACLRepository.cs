using Test.Db.Model.Placeholder;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Db.Model;

namespace Test.Db.SecureRepository.PlaceHolder
{
    public class PlaceholderACLRepository : Interfaces.Repositories.Repository<PlaceholderEntityACL>, IPlaceholderACLRepository
    {
        private Model.CoreContext Context;
        public PlaceholderACLRepository(CoreContext context) : base(context)
        {
        }
    }

    public interface IPlaceholderACLRepository : Interfaces.Repositories.IEntityRepository<PlaceholderEntityACL>
    {
    }
}
