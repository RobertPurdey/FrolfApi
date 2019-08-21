using Frolf.Api.Models.Games;
using Frolf.Api.Models.HoleScores;
using System;

namespace Frolf.Api.ModelDataControllers.Contracts
{
    public interface IGameModelDataController
        : IModelDataController<GameModel, GameFilter>
    {
        void SaveHoleScoreSet(HoleScoreSetUpdateModel updateRequest);
        GameModel CreateGame(GameCreationModel groupId);
        GameResultModel GetGameResults(Guid gameId);
        bool CanAnnounceGame(Guid gameId);
        bool CanSpectateGame(Guid gameId);
        void CompleteGame(Guid gameId);
    }
}