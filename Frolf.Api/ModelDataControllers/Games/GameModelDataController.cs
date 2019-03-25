using Application.Command.HoleScores;
using Application.Query.Services.Games;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Query.Contracts;
using Frolf.Api.Mappers;
using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.Models.Games;
using Frolf.Api.Models.HoleScores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Frolf.Api.ModelDataControllers.Games
{
    public class GameModelDataController :
        ModelDataController<GameModel, GameFilterModel>,
        IGameModelDataController
    {
        private readonly IReadOnlyEntityMapper<GameModel, Game> gameMapper;
        private readonly IQueryService<Game> gameQueryService;
        private readonly ICommandExecutor commandExecutor;

        public GameModelDataController(
            IReadOnlyEntityMapper<GameModel, Game> gameMapping,
            IQueryService<Game> gameService,
            ICommandExecutor cmdExecutor)
        {
            gameMapper       = gameMapping;
            gameQueryService = gameService;
            commandExecutor  = cmdExecutor;
        }

        public override void Delete(GameModel modelToDelete)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<GameModel> GetAll()
        {
            foreach (var entity in gameQueryService.GetAll())
            {
                var model = new GameModel();
                gameMapper.MapToApiModel(model, entity);

                yield return model;
            }
        }

        public override IEnumerable<GameModel> GetWithFilter(GameFilterModel filter)
        {
            throw new NotImplementedException();
        }

        public override GameModel GetById(Guid id)
        {
            var entity  = FindEntity(id, gameQueryService);
            var model   = new GameModel();

            gameMapper.MapToApiModel(model, entity);

            return model;
        }

        public override void Insert(GameModel newModel)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameModel modelToUpdate)
        {
            throw new NotImplementedException();
        }

        public void SaveHoleScoreSet(HoleScoreSetUpdateModel updateRequest)
        {
            var gameId          = updateRequest.GameId;
            var newHoleScores   = updateRequest.HoleScoreUpdates;
            var game            = FindEntity(gameId, gameQueryService);
            
            // Deny early 
            if ( game.CreatedBy != UserExtensions.GetCurrentUserId() )
            {
                throw new Exception("Cannot score game that wasn't created by you.");
            }

            var updatedHoleScores = game.HoleScores
                .Where(hs => newHoleScores.ContainsKey(hs.EntityKey));

            foreach ( var holeScore in updatedHoleScores )
            {
                holeScore.Score = newHoleScores[holeScore.EntityKey];
            }

            var saveCmd = new BatchUpdateHoleScoreCommand(gameId, updatedHoleScores);
            commandExecutor.Execute(saveCmd);
        }

        private static GameQueryArg ConvertToQueryArg(GameFilterModel filter)
        {
            return new GameQueryArg
            {
                // todo: mappings filter => query arg
            };
        }
    }
}