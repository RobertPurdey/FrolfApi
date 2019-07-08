using Frolf.Api.Encryption;
using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.ModelDataControllers.Users;
using Frolf.Api.Models;
using Frolf.Api.Models.Encryption;
using Frolf.Api.Models.FrolfGroups;
using Frolf.Api.Models.Games;
using Frolf.Api.Models.Players;
using Security.Contracts;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Frolf.Api.Controllers
{
    [RoutePrefix("api/frolfgroups")]
    public class FrolfGroupController : EditControllerBase<FrolfGroupModel, FrolfGroupFilterModel>
    {
        private readonly IFrolfGroupModelDataController frolfGroupModelDataController;

        public FrolfGroupController(
            IModelEncryptor modelEncryptor,
            IRsaKeyInfo serverKeyInfo,
            IAppUserPublicKeyRetriever userRsaKeyRetriever,
            IFrolfGroupModelDataController frolfGroupDataController)
            : base(modelEncryptor, serverKeyInfo, userRsaKeyRetriever)
        {
            frolfGroupModelDataController = frolfGroupDataController;
        }

        public override Task<EncryptModel> GetAll()
        {
            var foundFrolfGroups    = frolfGroupModelDataController.GetAll();
            var encryptFrolfGroups  = EncryptModel(foundFrolfGroups);
            
            return Task.FromResult(encryptFrolfGroups);
        }

        public override Task<EncryptModel> GetById(EncryptModel id)
        {
            var idModel             = DecryptModel<IdModel>(id);
            var foundFrolfGroup     = frolfGroupModelDataController.GetById(idModel.IdKey);
            var encryptFrolfGroup   = EncryptModel(foundFrolfGroup);

            return Task.FromResult(encryptFrolfGroup);
        }

        protected override Task<EncryptModel> Create([FromBody] EncryptModel newEntity)
        {
            var newFrolfGroup = DecryptModel<FrolfGroupModel>(newEntity);
            frolfGroupModelDataController.Insert(newFrolfGroup);
            var encryptFrolfGroup = EncryptModel(newFrolfGroup);

            return Task.FromResult(encryptFrolfGroup);
        }

        protected override Task Remove(EncryptModel id)
        {
            throw new NotImplementedException();
        }

        protected override Task Update([FromBody] EncryptModel newDetails)
        {
            var frolfGroup = DecryptModel<FrolfGroupModel>(newDetails);
            frolfGroupModelDataController.Update(frolfGroup);

            return Task.FromResult(1);
        }

        public override Task<EncryptModel> GetWithFilter([FromBody] EncryptModel filter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("groupmembers")]
        public Task<EncryptModel> GetFrolfGroupMembers([FromBody] EncryptModel id)
        {
            var idModel         = DecryptModel<IdModel>(id);
            var groupMembers    = frolfGroupModelDataController.GetGroupMembers(idModel.IdKey);
            var encryptMembers  = EncryptModel(groupMembers); 

            return Task.FromResult(encryptMembers);
        }

        [HttpPost]
        [Route("creategame")]
        public Task<EncryptModel> CreateGame([FromBody] EncryptModel model)
        {
            var gameCreation    = DecryptModel<GameCreationModel>(model);
            var createdGame     = frolfGroupModelDataController.CreateGame(gameCreation);
            var encryptGame     = EncryptModel(createdGame);

            return Task.FromResult(encryptGame);
        }

        [HttpPost]
        [Route("leave")]
        public Task LeaveGroup([FromBody] EncryptModel id)
        {
            var idModel = DecryptModel<IdModel>(id);
            frolfGroupModelDataController.LeaveGroup(idModel.IdKey);

            return Task.FromResult(1);
        }

        [HttpPost]
        [Route("removePlayer")]
        public Task RemovePlayer(EncryptModel removePlayerModel)
        {
            var removePlayer = DecryptModel<RemovePlayerModel>(removePlayerModel);
            frolfGroupModelDataController.RemovePlayer(removePlayer);

            return Task.FromResult(1);
        }
    }
}