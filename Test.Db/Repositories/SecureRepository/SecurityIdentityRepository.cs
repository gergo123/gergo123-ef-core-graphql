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
    public class SecurityIdentityRepository : Interfaces.Repositories.Repository<Model.RLS.SecurityIdentity>, ISecurityIdentityRepository
    {
        private CoreContext Context { get; }

        public SecurityIdentityRepository(CoreContext context) : base(context)
        {
            Context = context;
        }

        public void DeleteConnection(SecurityGroupSecurityIdentity groupConenction)
        {
            Context.Entry(groupConenction.Group).State = EntityState.Detached;
            Context.Entry(groupConenction.Identity).State = EntityState.Detached;
            Context.Entry(groupConenction).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public void AddConn(SecurityGroupSecurityIdentity conn)
        {
            Context.SecurityGroupSecurityIdentities.Add(conn);
        }

        public SecurityGroup GetGroup(long id)
        {
            return Context.SecurityObjects.OfType<SecurityGroup>().Where(x => x.Id == id).Single();
        }

        public SecurityIdentity GetIdentity(long id)
        {
            return Context.SecurityObjects.OfType<SecurityIdentity>()
                .Include(x => x.GroupMemberShips).Where(x => x.Id == id).Single();
        }
    }

    public interface ISecurityIdentityRepository : Interfaces.Repositories.IEntityRepository<Model.RLS.SecurityIdentity>
    {
        SecurityGroup GetGroup(long id);
        SecurityIdentity GetIdentity(long id);
        void DeleteConnection(SecurityGroupSecurityIdentity groupConenction);
        void AddConn(SecurityGroupSecurityIdentity conn);
    }
}
