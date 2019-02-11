using Frolf.Api.Models.FrolfGroups;
using System;

namespace Frolf.Api.ModelDataControllers.Contracts
{
    public interface IFrolfGroupInviteModelDataController
        : IModelDataController<FrolfGroupInviteModel, FrolfGroupInviteFilterModel>
    {
        void Accept(Guid id);
        void Send(InviteCreationModel friendCode);
    }
}