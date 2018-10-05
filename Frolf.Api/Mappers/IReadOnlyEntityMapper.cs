namespace Frolf.Api.Mappers
{
    /// <summary>
    /// Provides mapping to ApiDataModel
    /// </summary>
    /// <typeparam name="TApiModel">Model to map to</typeparam>
    /// <typeparam name="TEntity">Entity to map from</typeparam>
    public interface IReadOnlyEntityMapper<in TApiModel, in TEntity>
        where TApiModel : class
        where TEntity : class
    {
        void MapToApiModel(TApiModel apiModel, TEntity entity);
    }
}