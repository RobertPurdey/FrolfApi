using Domain.Commands.Contracts;
using Domain.Entities;
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

        public Repository(
            IEntityDbContext entityContext)
        {
            this.entityContext = entityContext;
        }

        public virtual TEntity Add(TEntity newEntity)
        {
            if (newEntity is IGuidEntity guidEntity)
            {
                guidEntity.EntityKey = Guid.NewGuid();
            }

            if (newEntity is IOwnable ownableEntity)
            {
                ownableEntity.CreatedBy   = UserExtensions.GetCurrentUserId();
                ownableEntity.CreatedDate = DateTime.UtcNow;
            }

            return entityContext.Add(newEntity);
        }

        public virtual void Update(TEntity updatedEntity)
        {

        }

        public virtual void Remove(TEntity entityToRemove)
        {
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
