using Domain.Entities.Contracts;
using System;
using System.Linq.Expressions;

namespace Domain.Query
{
    public abstract class GuidEntityQueryArg<TEntity> : QueryArg<TEntity>
        where TEntity : class, IGuidEntity
    {
        public Guid? Id { get; set; }

        public sealed override Expression<Func<TEntity, bool>> FilterBy => 
            HasId.And(ConstructFilter());

        protected virtual Expression<Func<TEntity, bool>> ConstructFilter()
        {
            return True;
        }

        protected virtual Expression<Func<TEntity, bool>> HasId =>
            Id.HasValue
                ? (entity => entity.EntityKey.Equals(Id.Value))
                : True;
    }
}
