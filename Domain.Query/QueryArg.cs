using Domain.Query.Contracts;
using System;
using System.Linq.Expressions;

namespace Domain.Query
{
    public abstract class QueryArg<TEntity> : IQueryArg<TEntity>
        where TEntity : class
    {
        protected Expression<Func<TEntity, bool>> True => ExpressionBuilder.True<TEntity>();
        protected Expression<Func<TEntity, bool>> False => ExpressionBuilder.False<TEntity>();

        public abstract Expression<Func<TEntity, bool>> FilterBy { get; }
    }
}
