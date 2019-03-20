using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.Models.HoleScores;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Frolf.Api.Controllers
{
    [RoutePrefix("api/holescores")]
    public class HoleScoreController : EditControllerBase<HoleScoreModel, HoleScoreFilterModel>
    {
        private readonly IHoleScoreModelDataController holeScoreDataController;

        public HoleScoreController(
            IHoleScoreModelDataController holeScoreDataController)
        {
            this.holeScoreDataController = holeScoreDataController;
        }

        public override Task<IEnumerable<HoleScoreModel>> GetAll()
        {
            var foundHoleScores = holeScoreDataController.GetAll();

            return Task.FromResult(foundHoleScores);
        }

        public override Task<HoleScoreModel> GetById([FromUri] Guid id)
        {
            var foundFrolfGroup = holeScoreDataController.GetById(id);

            return Task.FromResult(foundFrolfGroup);
        }

        public override Task<IEnumerable<HoleScoreModel>> GetWithFilter([FromBody] HoleScoreFilterModel filter)
        {
            var foundHoleScores = holeScoreDataController.GetWithFilter(filter);

            return Task.FromResult(foundHoleScores);
        }

        protected override Task<HoleScoreModel> Create([FromBody] HoleScoreModel newEntity)
        {
            throw new NotImplementedException();
        }

        protected override Task Remove([FromUri] Guid id)
        {
            throw new NotImplementedException();
        }

        protected override Task Update([FromBody] HoleScoreModel newDetails)
        {
            throw new NotImplementedException();
        }
    }
}