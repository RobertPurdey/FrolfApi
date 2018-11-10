using Frolf.Api.ModelDataControllers.Contracts;
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
            IFrolfGroupInviteModelDataController frolfGroupInviteDataController)
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

        [HttpPost]
        [Route("{id:guid}/accept")]
        public void Accept(Guid id)
        {
            frolfGroupInviteModelDataController.Accept(id);
        }
    }
}