using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.Models.FrolfGroups;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Frolf.Api.Controllers
{
    [RoutePrefix("api/frolfgroups")]
    public class FrolfGroupController : EditControllerBase<FrolfGroupModel>
    {
        private readonly IFrolfGroupModelDataController frolfGroupModelDataController;

        public FrolfGroupController(
            IFrolfGroupModelDataController frolfGroupDataController)
        {
            frolfGroupModelDataController = frolfGroupDataController;
        }

        [HttpGet]
        [Route("{id:guid}")]
        public override Task<FrolfGroupModel> GetById([FromUri] Guid id)
        {
            var foundFrolfGroup = frolfGroupModelDataController.GetById(id);

            return Task.FromResult(foundFrolfGroup);
        }

        protected override Task<FrolfGroupModel> Create([FromBody] FrolfGroupModel newEntity)
        {
            frolfGroupModelDataController.Insert(newEntity);

            return Task.FromResult(newEntity);
        }

        protected override Task Remove([FromUri] Guid id)
        {
            throw new NotImplementedException();
        }

        protected override Task Update([FromBody] FrolfGroupModel newDetails)
        {
            throw new NotImplementedException();
        }
    }
}