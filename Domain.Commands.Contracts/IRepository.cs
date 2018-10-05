using System;
using System.Linq.Expressions;

namespace Domain.Commands.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity newEntity);
        void Update(TEntity updatedEntity);
        void Remove(TEntity entityToRemove);
        TEntity GetByKey(Expression<Func<TEntity, bool>> keyExpression);
    }
}
