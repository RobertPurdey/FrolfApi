using Domain.Entities;
using Frolf.Api.Models.FrolfGroups;

namespace Frolf.Api.Mappers.FrolfGroups
{
    public class FrolfGroupMapper : IReadWriteEntityMapper<FrolfGroupModel, FrolfGroup>
    {
        public FrolfGroupMapper()
        {

        }

        public void MapToApiModel(FrolfGroupModel apiModel, FrolfGroup entity)
        {
            apiModel.IdKey         = entity.EntityKey;
        }

        public void MapToEntity(FrolfGroupModel apiModel, FrolfGroup entity)
        {
            entity.EntityKey    = apiModel.IdKey;
        }
    }
}