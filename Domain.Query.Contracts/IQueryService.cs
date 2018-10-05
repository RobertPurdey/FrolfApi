using System.Linq;

namespace Domain.Query.Contracts
{
    public interface IQueryService<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetWithQueryArg(IQueryArg<TEntity> arg);
    }
}
