using Domain.Entities;
using Frolf.Api.Encryption;
using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.ModelDataControllers.Users;
using Frolf.Api.Models;
using Frolf.Api.Models.Encryption;
using Frolf.Api.Models.Users;
using System;
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

        [HttpPost]
        [Route("getById")]
        public override Task<EncryptModel> GetById([FromBody] EncryptModel id)
        {
            var idModel      = DecryptModel<IdModel>(id);
            var foundAppUser = appUserModelDataController.GetById(idModel.IdKey);
            var encryptUser  = EncryptModel(foundAppUser);

            return Task.FromResult(encryptUser);
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
        public Task SetNewPublicKey([FromBody] EncryptModel publicKeyModel)
        {
            var newPublicKey = DecryptModel<PublicKeyModel>(publicKeyModel);
            appUserModelDataController.SetPublicKey(newPublicKey);

            return Task.FromResult(1);
        }

        [HttpPost]
        [Route("update/account")]
        public Task UpdateAccount([FromBody] EncryptModel updateUserRequest)
        {
            var userRequest = DecryptModel<AppUserUpdateModel>(updateUserRequest);
            appUserModelDataController.UpdateAccount(userRequest);

            return Task.FromResult(1);
        }

        protected override Task<EncryptModel> Create([FromBody] EncryptModel newItem)
        {
            throw new NotImplementedException();
        }

        protected override Task Remove([FromUri] EncryptModel id)
        {
            throw new NotImplementedException();
        }

        protected override Task Update([FromBody] EncryptModel newDetails)
        {
            throw new NotImplementedException();
        }

        public override Task<EncryptModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Task<EncryptModel> GetWithFilter([FromBody] EncryptModel filter)
        {
            throw new NotImplementedException();
        }
    }
}