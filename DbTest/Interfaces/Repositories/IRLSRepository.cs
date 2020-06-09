using DbTest.Interfaces.RLS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DbTest.Interfaces.Repositories
{
    public interface IRLSRepository<Entity, ACLEntity> : IDisposable
        where Entity : class
        where ACLEntity : class, IACLEntity
    {
        IQueryable<Entity> GetAll();
        IQueryable<Entity> Find(Expression<Func<Entity, bool>> query);
        Entity GetById(int id);
        Entity GetById(long id);
        void Add(Entity item);
        void Delete(Entity item);
        void Update(Entity item);
        void SaveChanges();
        void AddAcls(ACLEntity[] aCLEntities);
    }
}
