using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Db.Model;
using Test.Db.Model.RLS;

namespace Test.Db.Repositories.RLS
{
    public class SecurityObjectRepository : Interfaces.Repositories.Repository<Model.RLS.SecurityObject>, ISecurityObjectRepository
    {
        private readonly CoreContext context;

        public SecurityObjectRepository(CoreContext context) : base(context)
        {
            this.context = context;
        }

        public SecurityIdentity GetIdentity(long id)
        {
            return context.SecurityObjects.OfType<SecurityIdentity>()
                .Include(x => x.GroupMemberShips).Where(x => x.Id == id).Single();
        }
    }

    public interface ISecurityObjectRepository : Interfaces.Repositories.IEntityRepository<SecurityObject>
    {
        SecurityIdentity GetIdentity(long id);
    }
}
