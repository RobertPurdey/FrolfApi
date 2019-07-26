using Application.Query.Services.HoleScores;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Query.Contracts;
using Frolf.Api.Mappers;
using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.Models.HoleScores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Frolf.Api.ModelDataControllers.HoleScores
{
    public class HoleScoreModelDataController :
        ModelDataController<HoleScoreModel, HoleScoreFilterModel>,
        IHoleScoreModelDataController
    {
        private readonly IReadOnlyEntityMapper<HoleScoreModel, HoleScore> holeScoreMapper;
        private readonly IQueryService<HoleScore> holeScoreQueryService;
        private readonly ICommandExecutor commandExecutor;

        public HoleScoreModelDataController(
            IReadOnlyEntityMapper<HoleScoreModel, HoleScore> holeScoreMapping,
            IQueryService<HoleScore> holeScoreService,
            ICommandExecutor cmdExecutor)
        {
            holeScoreMapper = holeScoreMapping;
            holeScoreQueryService = holeScoreService;
            commandExecutor = cmdExecutor;
        }

        public override void Delete(HoleScoreModel modelToDelete)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<HoleScoreModel> GetAll()
        {
            foreach (var entity in holeScoreQueryService.GetAll())
            {
                var model = new HoleScoreModel();
                holeScoreMapper.MapToApiModel(model, entity);

                yield return model;
            }
        }

        public override IEnumerable<HoleScoreModel> GetWithFilter(HoleScoreFilterModel filter)
        {
            var queryArg       = ConvertToQueryArg(filter);
            var filteredHoles  = holeScoreQueryService.GetWithQueryArg(queryArg);

            foreach (var entity in filteredHoles.OrderBy(hs => hs.Player.Handle))
            {
                var model = new HoleScoreModel();
                holeScoreMapper.MapToApiModel(model, entity);

                yield return model;
            }
        }

        public override HoleScoreModel GetById(Guid id)
        {
            var entity = FindEntity(id, holeScoreQueryService);
            var model = new HoleScoreModel();

            holeScoreMapper.MapToApiModel(model, entity);

            return model;
        }

        public override void Insert(HoleScoreModel newModel)
        {
            throw new NotImplementedException();
        }

        public override void Update(HoleScoreModel modelToUpdate)
        {
            throw new NotImplementedException();
        }

        private static HoleScoreQueryArg ConvertToQueryArg(HoleScoreFilterModel filter)
        {
            return new HoleScoreQueryArg
            {
                GameId            = filter.GameId,
                HoleNumber        = filter.HoleNumber
            };
        }
    }
}