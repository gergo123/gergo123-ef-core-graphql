using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DbTest.Stepper.Model.RLS;
using DbTest.Model.RLS;
using DbTest.Model;

namespace DbTest.Repositories.RLS
{
    public class SecurityObjectRepository : Interfaces.Repositories.Repository<SecurityObject>, ISecurityObjectRepository
    {
        private readonly CoreContext context;

        public SecurityObjectRepository(CoreContext context) : base(context)
        {
            this.context = context;
        }

        public void RemoveIdentityFromGroup(SecurityGroupSecurityIdentity groupConenction)
        {
            context.Entry(groupConenction.Group).State = EntityState.Detached;
            context.Entry(groupConenction.Identity).State = EntityState.Detached;
            context.Entry(groupConenction).State = EntityState.Deleted;
        }

        public void AddIdentityToGroup(SecurityGroupSecurityIdentity conn)
        {
            context.SecurityGroupSecurityIdentities.Add(conn);
        }

        public SecurityGroup GetGroup(long id)
        {
            return context.SecurityObjects.OfType<SecurityGroup>()
                .Include(x => x.GroupMembers).Where(x => x.Id == id).Single();
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
        SecurityGroup GetGroup(long id);
        void RemoveIdentityFromGroup(SecurityGroupSecurityIdentity groupConenction);
        void AddIdentityToGroup(SecurityGroupSecurityIdentity conn);
    }
}
