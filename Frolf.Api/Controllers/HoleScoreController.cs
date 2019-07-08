using Frolf.Api.Encryption;
using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.ModelDataControllers.Users;
using Frolf.Api.Models;
using Frolf.Api.Models.Encryption;
using Frolf.Api.Models.HoleScores;
using Security.Contracts;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Frolf.Api.Controllers
{
    [RoutePrefix("api/holescores")]
    public class HoleScoreController : EditControllerBase<HoleScoreModel, HoleScoreFilterModel>
    {
        private readonly IHoleScoreModelDataController holeScoreDataController;

        public HoleScoreController(
            IModelEncryptor modelEncryptor,
            IRsaKeyInfo serverKeyInfo,
            IAppUserPublicKeyRetriever userRsaKeyRetriever,
            IHoleScoreModelDataController holeScoreDataController)
            : base(modelEncryptor, serverKeyInfo,  userRsaKeyRetriever)
        {
            this.holeScoreDataController = holeScoreDataController;
        }

        public override Task<EncryptModel> GetAll()
        {
            var foundHoleScores     = holeScoreDataController.GetAll();
            var encryptHoleScores   = EncryptModel(foundHoleScores);

            return Task.FromResult(encryptHoleScores);
        }

        public override Task<EncryptModel> GetById([FromBody] EncryptModel id)
        {
            var idModel          = DecryptModel<IdModel>(id);
            var foundHoleScore   = holeScoreDataController.GetById(idModel.IdKey);
            var encryptHoleScore = EncryptModel(foundHoleScore);

            return Task.FromResult(encryptHoleScore);
        }

        public override Task<EncryptModel> GetWithFilter([FromBody] EncryptModel filter)
        {
            var holeScoreFilter   = DecryptModel<HoleScoreFilterModel>(filter);
            var foundHoleScores   = holeScoreDataController.GetWithFilter(holeScoreFilter);
            var encryptHoleScores = EncryptModel(foundHoleScores);

            return Task.FromResult(encryptHoleScores);
        }

        protected override Task<EncryptModel> Create(EncryptModel newEntity)
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
    }
}