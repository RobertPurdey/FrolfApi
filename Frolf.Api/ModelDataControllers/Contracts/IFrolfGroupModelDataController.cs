using Domain.Entities;
using Frolf.Api.Models.FrolfGroups;
using System;
using System.Collections.Generic;

namespace Frolf.Api.ModelDataControllers.Contracts
{
    public interface IFrolfGroupModelDataController
        : IModelDataController<FrolfGroupModel, FrolfGroupFilterModel>
    {
        IEnumerable<PlayerModel> GetGroupMembers(Guid groupId);
    }
}