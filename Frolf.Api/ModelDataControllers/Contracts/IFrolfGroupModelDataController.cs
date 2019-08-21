using Frolf.Api.Models.FrolfGroups;
using Frolf.Api.Models.Games;
using Frolf.Api.Models.Players;
using System;
using System.Collections.Generic;

namespace Frolf.Api.ModelDataControllers.Contracts
{
    public interface IFrolfGroupModelDataController
        : IModelDataController<FrolfGroupModel, FrolfGroupFilterModel>
    {
        IEnumerable<PlayerModel> GetGroupMembers(Guid groupId);
        void LeaveGroup(Guid id);
        void RemovePlayer(RemovePlayerModel removePlayer);
    }
}