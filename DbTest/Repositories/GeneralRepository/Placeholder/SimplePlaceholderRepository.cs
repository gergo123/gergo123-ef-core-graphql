using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DbTest.Model.Placeholder;
using DbTest.Model;

namespace DbTest.Repositories
{
    public class SimplePlaceholderRepository : Interfaces.Repositories.Repository<SimplePlaceHolderEntity>, ISimplePlaceholderRepository
    {
        private Model.CoreContext Context;
        public SimplePlaceholderRepository(CoreContext context) : base(context)
        {
        }
    }

    public interface ISimplePlaceholderRepository : Interfaces.Repositories.IEntityRepository<SimplePlaceHolderEntity>
    {
    }
}