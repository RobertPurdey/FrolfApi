using Domain.Entities;
using Frolf.Api.Models.Users;

namespace Frolf.Api.Mappers.Users
{
    public class AppUserMapper : IReadWriteEntityMapper<AppUserModel, AppUser>
    {
        public AppUserMapper()
        {

        }

        public void MapToApiModel(AppUserModel apiModel, AppUser entity)
        {
            apiModel.Id            = entity.EntityKey;
            apiModel.LoginName     = entity.LoginName;
            apiModel.Password      = entity.Password;
            apiModel.Email         = entity.Email;
        }

        public void MapToEntity(AppUserModel apiModel, AppUser entity)
        {
            entity.EntityKey    = apiModel.Id;
            entity.LoginName    = apiModel.LoginName;
            entity.Password     = apiModel.Password;
            entity.Email        = apiModel.Email;
        }
    }
}