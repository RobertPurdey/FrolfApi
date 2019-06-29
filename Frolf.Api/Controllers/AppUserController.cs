using Domain.Entities;
using Frolf.Api.Encryption;
using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.ModelDataControllers.Users;
using Frolf.Api.Models.Encryption;
using Frolf.Api.Models.Users;
using Security.Encryption;
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
            IModelEncryptor modelEncryptor,
            IAppUserPublicKeyRetriever userRsaKeyRetriever,
            IAppUserModelDataController appUserDataController)
            : base(modelEncryptor, userRsaKeyRetriever)
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
        public Task<EncryptModel> GetCurrentUserInfo()
        {
            var foundAppUser   = appUserModelDataController.GetById(UserExtensions.GetCurrentUserId());
            var encryptedModel = EncryptModel(foundAppUser);

            return Task.FromResult(encryptedModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("create/account")]
        public Task CreateAccount([FromBody] EncryptModel newUserRequest)
        {
            var userRequest = DecryptModel<AppUserCreationModel>(newUserRequest);
            appUserModelDataController.CreateAccount(userRequest);

            return Task.FromResult(1);
        }

        [HttpPost]
        [Route("setPublicKey")]
        public Task SetNewPublicKey([FromBody] PublicKeyModel publicKeyModel)
        {
            appUserModelDataController.SetPublicKey(publicKeyModel);

            return Task.FromResult(1);
        }

        [HttpPost]
        [Route("update/account")]
        public Task UpdateAccount([FromBody] AppUserUpdateModel updateUserRequest)
        {
            appUserModelDataController.UpdateAccount(updateUserRequest);

            return Task.FromResult(1);
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