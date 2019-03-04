using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.Models.FrolfGroups;
using Frolf.Api.Models.Games;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Frolf.Api.Controllers
{
    [RoutePrefix("api/frolfgroups")]
    public class FrolfGroupController : EditControllerBase<FrolfGroupModel, FrolfGroupFilterModel>
    {
        private readonly IFrolfGroupModelDataController frolfGroupModelDataController;

        public FrolfGroupController(
            IFrolfGroupModelDataController frolfGroupDataController)
        {
            frolfGroupModelDataController = frolfGroupDataController;
        }

        public override Task<IEnumerable<FrolfGroupModel>> GetAll()
        {
            var foundFrolfGroups = frolfGroupModelDataController.GetAll();

            return Task.FromResult(foundFrolfGroups);
        }

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

        public override Task<IEnumerable<FrolfGroupModel>> GetWithFilter([FromBody] FrolfGroupFilterModel filter)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("groupmembers/{id:guid}")]
        public Task<IEnumerable<PlayerModel>> GetFrolfGroupMembers([FromUri] Guid id)
        {
            var groupMembers = frolfGroupModelDataController.GetGroupMembers(id);
            
            return Task.FromResult(groupMembers);
        }

        [HttpPost]
        [Route("creategame")]
        public Task CreateGame([FromBody] GameCreationModel model)
        {
            frolfGroupModelDataController.CreateGame(model);

            return Task.FromResult(1);
        }
    }
}