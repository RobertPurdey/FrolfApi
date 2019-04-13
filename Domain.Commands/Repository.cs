using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Domain.Commands
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly IEnumerable<IEntityValidator<TEntity>> entityValidators;
        private readonly IEntityDbContext entityContext;

        public Repository(
            IEntityDbContext entityContext,
            IEnumerable<IEntityValidator<TEntity>> entityValidators)
        {
            this.entityContext    = entityContext;
            this.entityValidators = entityValidators;
        }

        public virtual TEntity Add(TEntity newEntity)
        {
            ValidateEntity(newEntity);

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
            ValidateEntity(updatedEntity);
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

        private void ValidateEntity(TEntity entity)
        {
            foreach ( var validator in entityValidators )
            {
                if ( validator.IsValid(entity) )
                {
                    continue;
                }

                var sb = new StringBuilder();

                sb.AppendLine("Entity is invalid:");
                sb.AppendLine(validator.Error);

                throw new Exception(sb.ToString());
            }
        }
    }
}
