using Frolf.Api.Models.FrolfGroups;
using Frolf.Api.Models.Games;
using System;
using System.Collections.Generic;

namespace Frolf.Api.ModelDataControllers.Contracts
{
    public interface IFrolfGroupModelDataController
        : IModelDataController<FrolfGroupModel, FrolfGroupFilterModel>
    {
        IEnumerable<PlayerModel> GetGroupMembers(Guid groupId);
        GameModel CreateGame(GameCreationModel groupId);
        void LeaveGroup(Guid id);
        void RemovePlayer(Guid id, Guid playerId);
    }
}