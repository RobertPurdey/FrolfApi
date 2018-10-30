using Domain.Entities;
using Frolf.Api.Models.FrolfGroups;

namespace Frolf.Api.Mappers.Players
{
    public class PlayerMapper : IReadWriteEntityMapper<PlayerModel, Player>
    {
        public PlayerMapper()
        {

        }

        public void MapToApiModel(PlayerModel apiModel, Player entity)
        {
            apiModel.IdKey        = entity.EntityKey;
            apiModel.AppUserId    = entity.AppUserId;
            apiModel.FrolfGroupId = entity.FrolfGroupId;
            apiModel.GroupRole    = entity.GroupRole;
            apiModel.Handle       = entity.Handle;
        }

        public void MapToEntity(PlayerModel apiModel, Player entity)
        {
            entity.EntityKey    = apiModel.IdKey;
            entity.AppUserId    = apiModel.AppUserId;
            entity.FrolfGroupId = apiModel.FrolfGroupId;
            entity.GroupRole    = apiModel.GroupRole;
            entity.Handle       = apiModel.Handle;
        }
    }
}