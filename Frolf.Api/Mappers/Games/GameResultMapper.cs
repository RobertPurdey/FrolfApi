using Domain.Entities;
using Frolf.Api.Models.Games;
using System.Collections.Generic;
using System.Linq;

namespace Frolf.Api.Mappers.Games
{
    public class GameResultMapper : IReadOnlyEntityMapper<GameResultModel, Game>
    {
        private readonly IReadOnlyEntityMapper<PlayerGameResultModel, Round> playerResultMapper;

        public GameResultMapper(IReadOnlyEntityMapper<PlayerGameResultModel, Round> playerResultMapper)
        {
            this.playerResultMapper = playerResultMapper;
        }

        public void MapToApiModel(GameResultModel apiModel, Game entity)
        {
            apiModel.CourseName         = entity.Course.Name;
            apiModel.CoursePar          = entity.Course.Holes.Sum(h => h.Par);
            apiModel.HoleCount          = entity.Course.Holes.Count();
            apiModel.HolePars           = entity.Course.Holes.ToDictionary(h => h.Order, h => h.Par);
            apiModel.PlayerResults      = playerResultMapper.MapToModels(entity.Rounds);
        }
    }
}