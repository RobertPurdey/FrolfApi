using System.Linq;

namespace Domain.Entities.Contracts
{
    /// <summary>
    /// Defines database context operations
    /// </summary>
    public interface IEntityDbContext
    {
        TEntity Add<TEntity>(TEntity entityToAdd) where TEntity : class;
        void Remove<TEntity>(TEntity entityToRemove) where TEntity : class;
        IQueryable<TEntity> GetCollection<TEntity>() where TEntity : class;
        void CommitChanges();
    }
}
