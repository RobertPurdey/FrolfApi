namespace Domain.Commands.Contracts
{
    public interface IWorkUnit
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        void Commit();
    }
}
