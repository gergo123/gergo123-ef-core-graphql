using Test.Db.Interfaces.RLS;
using Test.Db.RLS;
using Test.Db.Utils;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Test.Db.Interfaces.Repositories
{
    public abstract class RLSRepositoryBase<Entity, AclEntity> : IRLSRepository<Entity, AclEntity>
        where Entity : class, ISecurityEntity
        where AclEntity : class, IACLEntity
    {
        private DbContext _context;
        private DbSet<Entity> _dbSet;
        private DbSet<AclEntity> _aclDbSet;
        private IUserSecurityObjectsHandler SecurityObjects;

        public RLSRepositoryBase(DbContext context, IUserSecurityObjectsHandler securityObjects)
        {
            this._context = context;
            this._dbSet = context.Set<Entity>();
            this._aclDbSet = context.Set<AclEntity>();
            SecurityObjects = securityObjects;
        }

        public IQueryable<Entity> GetAll()
        {
            var query = from acl in _aclDbSet
                        where SecurityObjects.SecurityObjects.Contains(acl.SecurityObjectID) && (acl.Permission & PermissionEnum.Read) != 0
                        join s in _dbSet on acl.EntityID equals s.Id
                        select s;
            return query.Distinct();
        }

        public IQueryable<Entity> Find(Expression<Func<Entity, bool>> query)
        {
            var queryr = from acl in _aclDbSet
                         where SecurityObjects.SecurityObjects.Contains(acl.SecurityObjectID) && (acl.Permission & PermissionEnum.Read) != 0
                         join s in _dbSet on acl.EntityID equals s.Id
                         select s;

            return queryr.Distinct().Where(query);
        }

        public Entity GetById(int id)
        {
            var query = from acl in _aclDbSet
                        where SecurityObjects.SecurityObjects.Contains(acl.SecurityObjectID) && (acl.Permission & PermissionEnum.Read) != 0 && acl.EntityID == id
                        join s in _dbSet on acl.EntityID equals s.Id
                        select s;
            return query.Distinct().SingleOrDefault();
        }

        public void Add(Entity item)
        {
            _dbSet.Add(item);
        }

        public void Delete(Entity item)
        {
            var acls = _aclDbSet.Where(x => SecurityObjects.SecurityObjects.Contains(x.SecurityObjectID) &&
                (x.Permission & PermissionEnum.Delete) != 0 &&
                x.EntityID == item.Id);
            if (acls.Any())
            {
                _dbSet.Attach(item);
                _dbSet.Remove(item);
            }
            else
            {
                throw new Exception("User has no right to delete this entity!");
            }
        }

        public void Update(Entity item)
        {
            var acls = _aclDbSet.Where(x => SecurityObjects.SecurityObjects.Contains(x.SecurityObjectID) &&
                (x.Permission & PermissionEnum.Update) != 0 &&
                x.EntityID == item.Id);
            if (acls.Any())
            {
                _context.Entry(item).State = EntityState.Modified;
            }
            else
            {
                throw new Exception("User has no right to update this entity!");
            }
        }

        public void SaveChanges()
        {
            try
            {
                //var errors = _context.GetValidationErrors();
                //if (errors.Count() > 0 && errors.Any(x => !x.IsValid))
                //{
                //    throw new DbValidationException(errors);
                //}
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }

        }

        public void AddAcls(AclEntity[] aCLEntities)
        {
            _aclDbSet.AddRange(aCLEntities);
        }

        public Entity GetById(long id)
        {
            var query = from acl in _aclDbSet
                        where SecurityObjects.SecurityObjects.Contains(acl.SecurityObjectID) && (acl.Permission & PermissionEnum.Read) != 0 && acl.EntityID == id
                        join s in _dbSet on acl.EntityID equals s.Id
                        select s;
            return query.Distinct().SingleOrDefault();
        }

        public void Dispose()
        {
            this._context.Dispose();
        }
    }
}
