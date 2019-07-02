using Frolf.Api.Encryption;
using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.ModelDataControllers.Users;
using Frolf.Api.Models;
using Frolf.Api.Models.Encryption;
using Frolf.Api.Models.FrolfGroups;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Frolf.Api.Controllers
{
    [RoutePrefix("api/frolfgroupinvites")]
    public class FrolfGroupInviteController : EditControllerBase<FrolfGroupInviteModel, FrolfGroupInviteFilterModel>
    {
        private readonly IFrolfGroupInviteModelDataController frolfGroupInviteModelDataController;

        public FrolfGroupInviteController(
            IModelEncryptor modelEncryptor,
            IAppUserPublicKeyRetriever userRsaKeyRetriever,
            IFrolfGroupInviteModelDataController frolfGroupInviteDataController)
            : base(modelEncryptor, userRsaKeyRetriever)
        {
            frolfGroupInviteModelDataController = frolfGroupInviteDataController;
        }

        public override Task<EncryptModel> GetAll()
        {
            var foundFrolfGroupInvites    = frolfGroupInviteModelDataController.GetAll();
            var encryptFrolfGroupInvites  = EncryptModel(foundFrolfGroupInvites);

            return Task.FromResult(encryptFrolfGroupInvites);
        }

        public override Task<EncryptModel> GetWithFilter([FromBody] EncryptModel filter)
        {
            var frolfGroupInviteFilter = DecryptModel<FrolfGroupInviteFilterModel>(filter);
            var foundInvites           = frolfGroupInviteModelDataController.GetWithFilter(frolfGroupInviteFilter);
            var encryptInvites         = EncryptModel(foundInvites);

            return Task.FromResult(encryptInvites);
        }

        public override Task<EncryptModel> GetById([FromBody] EncryptModel id)
        {
            var idModel                 = DecryptModel<IdModel>(id);
            var foundFrolfGroupInvite   = frolfGroupInviteModelDataController.GetById(idModel.IdKey);
            var encryptFrolfGroupInvite = EncryptModel(foundFrolfGroupInvite);

            return Task.FromResult(encryptFrolfGroupInvite);
        }

        protected override Task<EncryptModel> Create(EncryptModel newEntity)
        {
            var newFrolfGroupInvite = DecryptModel<FrolfGroupInviteModel>(newEntity);
            frolfGroupInviteModelDataController.Insert(newFrolfGroupInvite);
            var encryptFrolfGroupInvite = EncryptModel(newFrolfGroupInvite);

            return Task.FromResult(encryptFrolfGroupInvite);
        }

        protected override Task Remove(EncryptModel id)
        {
            var idModel = DecryptModel<IdModel>(id);
            frolfGroupInviteModelDataController.Delete(new FrolfGroupInviteModel { IdKey = idModel.IdKey });

            return Task.FromResult(1);
        }

        protected override Task Update(EncryptModel newDetails)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("accept")]
        public void Accept([FromBody] EncryptModel id)
        {
            var idModel = DecryptModel<IdModel>(id);
            frolfGroupInviteModelDataController.Accept(idModel.IdKey);
        }

        [HttpPost]
        [Route("send")]
        public void Send(EncryptModel creationModel)
        {
            var inviteCreation = DecryptModel<InviteCreationModel>(creationModel);
            frolfGroupInviteModelDataController.Send(inviteCreation);
        }
    }
}