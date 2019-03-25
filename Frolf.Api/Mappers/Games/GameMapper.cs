using Domain.Entities;
using Frolf.Api.Models.Games;
using System.Linq;

namespace Frolf.Api.Mappers.Games
{
    public class GameMapper : IReadWriteEntityMapper<GameModel, Game>
    {
        public GameMapper()
        {

        }

        public void MapToApiModel(GameModel apiModel, Game entity)
        {
            apiModel.IdKey    = entity.EntityKey;
            apiModel.GroupId  = entity.FrolfGroupId;
            apiModel.CourseId = entity.CourseId;
            apiModel.Name     = entity.Name;
            apiModel.HoleIds  = entity.Course.Holes.Select(h => h.EntityKey);
        }

        public void MapToEntity(GameModel apiModel, Game entity)
        {
            entity.EntityKey = apiModel.IdKey;

        }
    }
}