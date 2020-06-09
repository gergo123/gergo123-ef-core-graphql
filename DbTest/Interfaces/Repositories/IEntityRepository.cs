using System;
using System.Linq;
using System.Linq.Expressions;

namespace DbTest.Interfaces.Repositories
{
    public interface IEntityRepository<Entity> : IDisposable where Entity : class
    {
        IQueryable<Entity> GetAll();
        IQueryable<Entity> Find(Expression<Func<Entity, bool>> query);
        Entity GetById(int id);
        Entity GetById(long id);
        void Add(Entity item);
        void Delete(Entity item);
        void Update(Entity item);
        void SaveChanges();
    }
}