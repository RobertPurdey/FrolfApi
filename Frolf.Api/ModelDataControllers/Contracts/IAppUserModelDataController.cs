using Domain.Entities;
using Frolf.Api.Models.Users;

namespace Frolf.Api.ModelDataControllers.Contracts
{
    public interface IAppUserModelDataController : IModelDataController<AppUserModel, AppUserFilterModel>
    {
        AppUser CreateAccount(AppUserCreationModel newUser);
        void UpdateAccount(AppUserUpdateModel updateUser);
        void SetPublicKey(PublicKeyModel keyModel);
    }
}