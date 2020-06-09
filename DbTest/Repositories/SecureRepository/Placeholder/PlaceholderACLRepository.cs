using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbTest.Model.Placeholder;
using DbTest.Model;

namespace DbTest.SecureRepository.PlaceHolder
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
