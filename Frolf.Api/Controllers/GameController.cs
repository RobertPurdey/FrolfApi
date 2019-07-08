using Frolf.Api.Encryption;
using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.ModelDataControllers.Users;
using Frolf.Api.Models;
using Frolf.Api.Models.Encryption;
using Frolf.Api.Models.Games;
using Frolf.Api.Models.HoleScores;
using Security.Contracts;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Frolf.Api.Controllers
{
    [RoutePrefix("api/games")]
    public class GameController : EditControllerBase<GameModel, GameFilter>
    {
        private readonly IGameModelDataController gameDataController;

        public GameController(
            IModelEncryptor modelEncryptor,
            IRsaKeyInfo serverKeyInfo,
            IAppUserPublicKeyRetriever userRsaKeyRetriever,
            IGameModelDataController gameDataController)
            : base(modelEncryptor, serverKeyInfo, userRsaKeyRetriever)
        {
            this.gameDataController = gameDataController;
        }

        public override Task<EncryptModel> GetAll()
        {
            var foundGames    = gameDataController.GetAll();
            var encryptGames  = EncryptModel(foundGames); 

            return Task.FromResult(encryptGames);
        }

        public override Task<EncryptModel> GetById([FromBody] EncryptModel id)
        {
            var idModel         = DecryptModel<IdModel>(id);
            var foundGameModel  = gameDataController.GetById(idModel.IdKey);
            var encryptGame     = EncryptModel(foundGameModel);

            return Task.FromResult(encryptGame);
        }

        protected override Task<EncryptModel> Create( EncryptModel newEntity)
        {
            throw new NotImplementedException();
        }

        protected override Task Remove(EncryptModel id)
        {
            throw new NotImplementedException();
        }

        protected override Task Update(EncryptModel newDetails)
        {
            throw new NotImplementedException();
        }

        public override Task<EncryptModel> GetWithFilter([FromBody] EncryptModel filter)
        {
            var gameFilter   = DecryptModel<GameFilter>(filter);
            var results      = gameDataController.GetWithFilter(gameFilter);
            var encryptGames = EncryptModel(results);

            return Task.FromResult(encryptGames);
        }

        [HttpPost]
        [Route("holeScores")]
        public Task UpdateGameHoles([FromBody] EncryptModel updateModel)
        {
            var holeScoreSetUpdate = DecryptModel<HoleScoreSetUpdateModel>(updateModel);
            gameDataController.SaveHoleScoreSet(holeScoreSetUpdate);

            return Task.FromResult(1);
        }

        [HttpPost]
        [Route("results")]
        public Task<EncryptModel> GetGameResults([FromBody] EncryptModel id)
        {
            var idModel             = DecryptModel<IdModel>(id);
            var gameResults         = gameDataController.GetGameResults(idModel.IdKey);
            var encryptGameResult   = EncryptModel(gameResults);

            return Task.FromResult(encryptGameResult);
        }

        [HttpPost]
        [Route("spectate")]
        public Task<EncryptModel> CanSpectateGame([FromBody] EncryptModel id)
        {
            var idModel         = DecryptModelFromServer<IdModel>(id);
            var result          = gameDataController.CanSpectateGame(idModel.IdKey);
            var encryptedResult = InternalEncryptModel(new BoolModel { Value = result });

            return Task.FromResult(encryptedResult);
        }

        [HttpPost]
        [Route("announce")]
        public Task<EncryptModel> CanAnnounceGame([FromBody] EncryptModel id)
        {
            var idModel         = DecryptModelFromServer<IdModel>(id);
            var result          = gameDataController.CanAnnounceGame(idModel.IdKey);
            var encryptedResult = InternalEncryptModel(new BoolModel { Value = result });

            return Task.FromResult(encryptedResult);
        }

        [HttpPatch]
        [Route("complete")]
        public Task CompleteGame([FromBody] EncryptModel id)
        {
            var idModel = DecryptModel<IdModel>(id);
            gameDataController.CompleteGame(idModel.IdKey);

            return Task.FromResult(1);
        }
    }
}