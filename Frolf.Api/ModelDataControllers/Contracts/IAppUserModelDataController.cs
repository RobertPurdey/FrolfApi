using Domain.Entities;
using Frolf.Api.Models.Users;
using System;

namespace Frolf.Api.ModelDataControllers.Contracts
{
    public interface IAppUserModelDataController : IModelDataController<AppUserModel, AppUserFilterModel>
    {
        AppUser CreateAccount(AppUserCreationModel newUser);
        void UpdateAccount(AppUserUpdateModel updateUser);
        void SetPublicKey(PublicKeyModel keyModel);
        AppUserModel GetExtendedUser(Guid id);
    }
}