namespace Domain.Commands.Contracts
{
    public interface IEntityValidator<in TEntity>
        where TEntity : class
    {
        bool IsValid(TEntity entity);
        string Error { get; }
    }
}
