using Domain.Entities.Contracts;
using Domain.Query.Contracts;
using System.Linq;

namespace Domain.Query
{
    public class QueryService<TEntity> : IQueryService<TEntity>
        where TEntity : class
    {
        private readonly IEntityDbContext entityDbContext;

        public QueryService(IEntityDbContext context)
        {
            entityDbContext = context;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return GetQueryableData();
        }

        public virtual IQueryable<TEntity> GetWithQueryArg(IQueryArg<TEntity> arg)
        {
            return GetQueryableData()
                .Where(arg.FilterBy);
        }

        private IQueryable<TEntity> GetQueryableData()
        {
            return entityDbContext
                .GetCollection<TEntity>()
                .AsQueryable();
        }
    }
}
