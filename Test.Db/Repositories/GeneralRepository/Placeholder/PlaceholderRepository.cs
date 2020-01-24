using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Test.Db.Model.Placeholder;
using Test.Db.Model;

namespace Test.Db.Repositories
{
    public class PlaceholderRepository : Interfaces.Repositories.Repository<PlaceholderEntity>, IPlaceholderRepository
    {
        private Model.CoreContext Context;
        public PlaceholderRepository(CoreContext context) : base(context)
        {
        }
    }

    public interface IPlaceholderRepository : Interfaces.Repositories.IEntityRepository<PlaceholderEntity>
    {
    }
}