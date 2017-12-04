using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NeuroTradeAPI
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            string includeProperties = "");
        TEntity GetByID(Guid id);
        void Insert(TEntity entity);
        void Delete(Guid id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
    }
}