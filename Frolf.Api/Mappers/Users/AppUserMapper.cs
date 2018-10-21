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
            apiModel.IdKey         = entity.EntityKey;
            apiModel.NickName      = entity.LoginName;
            apiModel.Password      = entity.Password;
            apiModel.Email         = entity.Email;
        }

        public void MapToEntity(AppUserModel apiModel, AppUser entity)
        {
            entity.EntityKey    = apiModel.IdKey;
            entity.LoginName    = apiModel.NickName;
            entity.Password     = apiModel.Password;
            entity.Email        = apiModel.Email;
        }
    }
}