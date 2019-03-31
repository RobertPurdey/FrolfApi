using Domain.Entities;
using Frolf.Api.Models.Games;
using System.Linq;

namespace Frolf.Api.Mappers.Games
{
    public class PlayerGameResultMapper : IReadOnlyEntityMapper<PlayerGameResultModel, Round>
    {
        public PlayerGameResultMapper()
        {

        }

        public void MapToApiModel(PlayerGameResultModel apiModel, Round entity)
        {
            apiModel.PlayerName     = entity.Player.Handle;
            apiModel.Strokes        = entity.HoleScores.Sum(hs => hs.Score);
            apiModel.TotalScore     = entity.HoleScores.Sum(hs => hs.Score - hs.Hole.Par);
            apiModel.Scores         = entity.HoleScores.ToDictionary(hs => hs.Hole.Order, hs => hs.Score);
        }
    }
}