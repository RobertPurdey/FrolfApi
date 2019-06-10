using Domain.Entities;
using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.Models.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Frolf.Api.Controllers
{
    [RoutePrefix("api/appusers")]
    public class AppUserController : EditControllerBase<AppUserModel, AppUserFilterModel>
    {
        private readonly IAppUserModelDataController appUserModelDataController;

        public AppUserController(
            IAppUserModelDataController appUserDataController)
        {
            appUserModelDataController = appUserDataController;
        }

        [HttpGet]
        [Route("{id:guid}")]
        public override Task<AppUserModel> GetById([FromUri] Guid id)
        {
            var foundAppUser = appUserModelDataController.GetById(id);

            return Task.FromResult(foundAppUser);
        }

        [HttpGet]
        [Route("info")]
        public Task<AppUserModel> GetCurrentUserInfo()
        {
            var foundAppUser = appUserModelDataController.GetById(UserExtensions.GetCurrentUserId());

            return Task.FromResult(foundAppUser);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("create/account")]
        public Task CreateAccount([FromBody] AppUserCreationModel newUserRequest)
        {
            appUserModelDataController.CreateAccount(newUserRequest);

            return Task.FromResult(1);
        }

        [HttpPost]
        [Route("update/account")]
        public Task UpdateAccount([FromBody] AppUserUpdateModel newUserRequest)
        {
            appUserModelDataController.UpdateAccount(newUserRequest);

            return Task.FromResult(1);
        }

        // todo: this is to be removed
        // game controller now has canAnnounce and canSpectate
        [HttpGet]
        [Route("allowedBroadcastAccess")]
        public Task<bool> AllowedBroadcastAccess()
        {
            // todo: more strict rules to come
            return Task.FromResult(true);
        }

        protected override Task<AppUserModel> Create([FromBody] AppUserModel newItem)
        {
            throw new NotImplementedException();
        }

        protected override Task Remove([FromUri] Guid id)
        {
            throw new NotImplementedException();
        }

        protected override Task Update([FromBody] AppUserModel newDetails)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<AppUserModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<AppUserModel>> GetWithFilter([FromBody] AppUserFilterModel filter)
        {
            throw new NotImplementedException();
        }
    }
}