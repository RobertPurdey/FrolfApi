using System;
using System.Linq.Expressions;

namespace Domain.Query.Contracts
{
    public interface IQueryArg<TEntity>
        where TEntity : class
    {
        Expression<Func<TEntity, bool>> FilterBy { get; }
    }
}
