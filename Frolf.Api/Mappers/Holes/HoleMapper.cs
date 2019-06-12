using Domain.Entities;
using Frolf.Api.Models.Holes;

namespace Frolf.Api.Mappers.Holes
{
    public class HoleMapper : IReadWriteEntityMapper<HoleModel, Hole>
    {
        public void MapToApiModel(HoleModel apiModel, Hole entity)
        {
            apiModel.IdKey      = entity.EntityKey;
            apiModel.Order      = entity.Order;
            apiModel.Par        = entity.Par;
            apiModel.CourseId   = entity.CourseId;
        }

        public void MapToEntity(HoleModel apiModel, Hole entity)
        {
            entity.EntityKey        = apiModel.IdKey;
            entity.CourseId         = apiModel.CourseId;
            entity.Order            = apiModel.Order;
            entity.Par              = apiModel.Par;
        }
    }
}