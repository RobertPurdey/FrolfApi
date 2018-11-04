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

        // todo: map more for invite/decline invite
        public void MapToApiModel(FrolfGroupInviteModel apiModel, FrolfGroupInvite entity)
        {
            apiModel.IdKey         = entity.EntityKey;
            apiModel.GroupName     = entity.FrolfGroup.Name;
            apiModel.InviterHandle = entity.Inviter.Handle;
        }

        public void MapToEntity(FrolfGroupInviteModel apiModel, FrolfGroupInvite entity)
        {
            entity.EntityKey = apiModel.IdKey;
        }
    }
}