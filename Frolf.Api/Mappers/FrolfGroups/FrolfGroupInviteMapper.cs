using Domain.Entities;
using Frolf.Api.Models.FrolfGroups;

namespace Frolf.Api.Mappers.FrolfGroups
{
    public class FrolfGroupInviteMapper
        : IReadWriteEntityMapper<FrolfGroupInviteModel, FrolfGroupInvite>
    {
        public FrolfGroupInviteMapper()
        {

        }

        public void MapToApiModel(FrolfGroupInviteModel apiModel, FrolfGroupInvite entity)
        {
            apiModel.IdKey   = entity.EntityKey;
        }

        public void MapToEntity(FrolfGroupInviteModel apiModel, FrolfGroupInvite entity)
        {
            entity.EntityKey   = apiModel.IdKey;
        }
    }
}