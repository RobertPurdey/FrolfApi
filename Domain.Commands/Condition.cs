namespace Domain.Commands
{
    /// <summary>
    /// Determine that an entity meets a given condition.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class Condition<TEntity>
        where TEntity : class
    {
        public abstract bool Validate(TEntity entity);
    }
}
