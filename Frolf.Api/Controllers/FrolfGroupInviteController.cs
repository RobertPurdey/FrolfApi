using Frolf.Api.Encryption;
using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.ModelDataControllers.Users;
using Frolf.Api.Models.FrolfGroups;
using System;
using System.Collections.Generic;
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

        public override Task<IEnumerable<FrolfGroupInviteModel>> GetAll()
        {
            var foundFrolfGroupInvites = frolfGroupInviteModelDataController.GetAll();

            return Task.FromResult(foundFrolfGroupInvites);
        }

        public override Task<IEnumerable<FrolfGroupInviteModel>> GetWithFilter([FromBody] FrolfGroupInviteFilterModel filter)
        {
            var foundInvites = frolfGroupInviteModelDataController.GetWithFilter(filter);

            return Task.FromResult(foundInvites);
        }

        public override Task<FrolfGroupInviteModel> GetById([FromUri] Guid id)
        {
            var foundFrolfGroupInvite = frolfGroupInviteModelDataController.GetById(id);

            return Task.FromResult(foundFrolfGroupInvite);
        }

        protected override Task<FrolfGroupInviteModel> Create([FromBody] FrolfGroupInviteModel newEntity)
        {
            frolfGroupInviteModelDataController.Insert(newEntity);

            return Task.FromResult(newEntity);
        }

        protected override Task Remove([FromUri] Guid id)
        {
            frolfGroupInviteModelDataController.Delete(new FrolfGroupInviteModel { IdKey = id });

            return Task.FromResult(1);
        }

        protected override Task Update([FromBody] FrolfGroupInviteModel newDetails)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{id:guid}/accept")]
        public void Accept([FromUri] Guid id)
        {
            frolfGroupInviteModelDataController.Accept(id);
        }

        [HttpPost]
        [Route("send")]
        public void Send(InviteCreationModel creationModel)
        {
            frolfGroupInviteModelDataController.Send(creationModel);
        }
    }
}