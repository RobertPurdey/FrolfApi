using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.Models.Games;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Frolf.Api.Controllers
{
    [RoutePrefix("api/games")]
    public class GameController : EditControllerBase<GameModel, GameFilterModel>
    {
        private readonly IGameModelDataController groupGroupDataController;

        public GameController(
            IGameModelDataController groupGroupDataController)
        {
            this.groupGroupDataController = groupGroupDataController;
        }

        public override Task<IEnumerable<GameModel>> GetAll()
        {
            var foundGames = groupGroupDataController.GetAll();

            return Task.FromResult(foundGames);
        }

        public override Task<GameModel> GetById([FromUri] Guid id)
        {
            var foundFrolfGroup = groupGroupDataController.GetById(id);

            return Task.FromResult(foundFrolfGroup);
        }

        protected override Task<GameModel> Create([FromBody] GameModel newEntity)
        {
            throw new NotImplementedException();
        }

        protected override Task Remove([FromUri] Guid id)
        {
            throw new NotImplementedException();
        }

        protected override Task Update([FromBody] GameModel newDetails)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<GameModel>> GetWithFilter([FromBody] GameFilterModel filter)
        {
            throw new NotImplementedException();
        }
    }
}