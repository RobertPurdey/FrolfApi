using Domain.Entities;
using Frolf.Api.Models.HoleScores;

namespace Frolf.Api.Mappers.HoleScores
{
    public class HoleScoreMapper : IReadWriteEntityMapper<HoleScoreModel, HoleScore>
    {
        public HoleScoreMapper()
        {

        }

        public void MapToApiModel(HoleScoreModel apiModel, HoleScore entity)
        {
            apiModel.IdKey            = entity.EntityKey;
            apiModel.HoleId           = entity.HoleId;
            apiModel.PlayerId         = entity.PlayerId;
            apiModel.RoundId          = entity.RoundId;

            apiModel.PlayerHandle     = entity.Player.Handle;
            apiModel.Strokes          = entity.Strokes;
            apiModel.Score            = entity.Strokes - entity.Hole.Par;
            apiModel.HolePar          = entity.Hole.Par;
        }

        public void MapToEntity(HoleScoreModel apiModel, HoleScore entity)
        {
            entity.EntityKey = apiModel.IdKey;
        }
    }
}