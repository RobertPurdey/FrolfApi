using System.Collections.Generic;

namespace Frolf.Api.Mappers
{
    public static class MapperExtensions
    {
        /// <summary>
        /// Maps entities to their respective model.
        /// </summary>
        /// <typeparam name="M">Model type to be mapped to</typeparam>
        /// <typeparam name="E">Entity type to map</typeparam>
        /// <param name="mapper">Mapper to map entity to model</param>
        /// <param name="entites">Entities being mapped</param>
        /// <returns>Mapped entities</returns>
        public static IEnumerable<M> MapToModels<M, E>(this IReadOnlyEntityMapper<M, E> mapper, IEnumerable<E> entites)
            where E : class
            where M : class, new()
        {
            var results = new List<E>();

            foreach (var entity in entites)
            {
                var model = new M();
                mapper.MapToApiModel(model, entity);

                yield return model;
            }
        }
    }
}