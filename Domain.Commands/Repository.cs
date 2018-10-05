using Domain.Commands.Contracts;
using Domain.Entities.Contracts;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Commands
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly IEntityDbContext entityContext;

        public Repository(IEntityDbContext context)
        {
            entityContext = context;
        }

        public virtual TEntity Add(TEntity newEntity)
        {
            // todo: some kind of validation

            if (newEntity is IGuidEntity guidEntity)
            {
                guidEntity.EntityKey = Guid.NewGuid();
            }

            return entityContext.Add(newEntity);
        }

        public virtual void Update(TEntity updatedEntity)
        {
            // todo: some kind of validation
        }

        public virtual void Remove(TEntity entityToRemove)
        {
            // todo: some kind of validation
            entityContext.Remove(entityToRemove);
        }

        public virtual TEntity GetByKey(Expression<Func<TEntity, bool>> keyExpression)
        {
            return entityContext
                .GetCollection<TEntity>()
                .SingleOrDefault(keyExpression.Compile());
        }
    }
}
