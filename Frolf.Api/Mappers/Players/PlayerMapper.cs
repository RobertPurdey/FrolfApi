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
            apiModel.IdKey     = entity.EntityKey;
        }

        public void MapToEntity(PlayerModel apiModel, Player entity)
        {
            entity.EntityKey    = apiModel.IdKey;
        }
    }
}