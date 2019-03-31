using Frolf.Api.Models.Games;
using Frolf.Api.Models.HoleScores;
using System;
using System.Collections.Generic;

namespace Frolf.Api.ModelDataControllers.Contracts
{
    public interface IGameModelDataController
        : IModelDataController<GameModel, GameFilterModel>
    {
        void SaveHoleScoreSet(HoleScoreSetUpdateModel updateRequest);
        IEnumerable<PlayerGameResultModel> GetPlayerResults(Guid gameId);
    }
}