using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.Models.Games;
using Frolf.Api.Models.HoleScores;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Frolf.Api.Controllers
{
    [RoutePrefix("api/games")]
    public class GameController : EditControllerBase<GameModel, GameFilterModel>
    {
        private readonly IGameModelDataController gameDataController;

        public GameController(
            IGameModelDataController gameDataController)
        {
            this.gameDataController = gameDataController;
        }

        public override Task<IEnumerable<GameModel>> GetAll()
        {
            var foundGames = gameDataController.GetAll();

            return Task.FromResult(foundGames);
        }

        public override Task<GameModel> GetById([FromUri] Guid id)
        {
            var foundFrolfGroup = gameDataController.GetById(id);

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

        [HttpPost]
        [Route("holeScores")]
        public Task UpdateGameHoles([FromBody] HoleScoreSetUpdateModel updateModel)
        {
            gameDataController.SaveHoleScoreSet(updateModel);

            return Task.FromResult(1);
        }
    }
}