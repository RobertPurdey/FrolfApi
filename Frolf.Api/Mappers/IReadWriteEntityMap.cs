namespace Frolf.Api.Mappers
{
    /// <summary>
    /// Provides mapping to/from ApiDataModel and Entity
    /// </summary>
    /// <typeparam name="TApiModel">Model to map to/from</typeparam>
    /// <typeparam name="TEntity">Entity to map to/from</typeparam>
    public interface IReadWriteEntityMapper<in TApiModel, in TEntity>
        : IReadOnlyEntityMapper<TApiModel, TEntity>
        where TApiModel : class
        where TEntity : class
    {
        void MapToEntity(TApiModel apiModel, TEntity entity);
    }
}