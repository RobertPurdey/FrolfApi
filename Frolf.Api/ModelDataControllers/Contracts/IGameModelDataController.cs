using Frolf.Api.Models.Games;
using Frolf.Api.Models.HoleScores;
using System;

namespace Frolf.Api.ModelDataControllers.Contracts
{
    public interface IGameModelDataController
        : IModelDataController<GameModel, GameFilterModel>
    {
        void SaveHoleScoreSet(HoleScoreSetUpdateModel updateRequest);
        GameResultModel GetGameResults(Guid gameId);
    }
}