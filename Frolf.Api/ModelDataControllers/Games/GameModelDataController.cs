using Application.Command.FrolfGroups.Conditions;
using Application.Command.Games;
using Application.Command.Games.Commands;
using Application.Command.Games.Conditions;
using Application.Command.HoleScores;
using Application.Command.HoleScores.Commands;
using Application.Query.Services.Games;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Query.Contracts;
using Frolf.Api.Composers.Games;
using Frolf.Api.Mappers;
using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.Models.Games;
using Frolf.Api.Models.HoleScores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Frolf.Api.ModelDataControllers.Games
{
    public class GameModelDataController :
        ModelDataController<GameModel, GameFilter>,
        IGameModelDataController
    {
        private readonly IReadOnlyEntityMapper<GameModel, Game> gameMapper;
        private readonly IReadOnlyEntityMapper<GameResultModel, Game> gameResultMapper;
        private readonly IQueryService<Game> gameQueryService;
        private readonly IQueryService<AppUser> userQueryService;
        private readonly IGameComposer gameComposer;
        private readonly ICommandExecutor commandExecutor;

        public GameModelDataController(
            IReadOnlyEntityMapper<GameModel, Game> gameMapping,
            IReadOnlyEntityMapper<GameResultModel, Game> gameResultMapping,
            IQueryService<Game> gameService,
            IQueryService<AppUser> userService,
            IGameComposer gameComp,
            ICommandExecutor cmdExecutor)
        {
            gameMapper          = gameMapping;
            gameResultMapper    = gameResultMapping;
            gameQueryService    = gameService;
            userQueryService    = userService;
            gameComposer        = gameComp;
            commandExecutor     = cmdExecutor;
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

        public override IEnumerable<GameModel> GetWithFilter(GameFilter filter)
        {
            var queryArg = ConvertToQueryArg(filter);

            foreach (var entity in gameQueryService.GetWithQueryArg(queryArg))
            {
                var model = new GameModel();
                gameMapper.MapToApiModel(model, entity);

                yield return model;
            }
        }

        public override GameModel GetById(Guid id)
        {
            var entity  = FindEntity(id, gameQueryService);
            var model   = new GameModel();

            gameMapper.MapToApiModel(model, entity);

            return model;
        }

        public GameModel CreateGame(GameCreationModel model)
        {
            var game         = gameComposer.NewGame(model);
            var addCommand   = new AddGameCommand { NewGame = game };
            var newGameModel = new GameModel();

            commandExecutor.Execute(addCommand);
            gameMapper.MapToApiModel(newGameModel, game);

            return newGameModel;
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
                holeScore.Strokes = newHoleScores[holeScore.EntityKey];
            }

            var saveCmd = new BatchUpdateHoleScoreCommand(gameId, updatedHoleScores);
            commandExecutor.Execute(saveCmd);
        }

        public GameResultModel GetGameResults(Guid gameId)
        {            
            var game         = FindEntity(gameId, gameQueryService);
            var gameResult   = new GameResultModel();

           // IsCurrentUserInGroupVerify(game);

            gameResultMapper.MapToApiModel(gameResult, game);

            return gameResult;
        }

        private static GameQueryArg ConvertToQueryArg(GameFilter filter)
        {
            return new GameQueryArg
            { 
                State = filter.State
            };
        }

        public bool CanAnnounceGame(Guid gameId)
        {
            var currentUserId   = UserExtensions.GetCurrentUserId();
            var game            = FindEntity(gameId, gameQueryService);           
            var canAnnounce     = new IsGameCreatedByCurrentUser().Validate(game);

            return canAnnounce;
        }

        public bool CanSpectateGame(Guid gameId)
        {
            var currentUserId   = UserExtensions.GetCurrentUserId();
            var game            = FindEntity(gameId, gameQueryService);
            var user            = FindEntity(currentUserId, userQueryService);
            var canSpectate     = new IsUserAGroupMember(user.EntityKey).Validate(game.FrolfGroup);

            return canSpectate;
        }

        public void CompleteGame(Guid gameId)
        {
            var game = FindEntity(gameId, gameQueryService);

            // Deny early 
            if ( game.CreatedBy != UserExtensions.GetCurrentUserId() )
            {
                throw new Exception("Cannot complete game that wasn't created by you.");
            }

            commandExecutor.Execute(new CompleteGameCommand { Game = game });
        }

        private void IsCurrentUserInGroupVerify(Game game)
        {
            var isCurrUserInGroup = game.FrolfGroup.Members
                .Any(p => p.AppUserId == UserExtensions.GetCurrentUserId());

            if ( !isCurrUserInGroup )
            {
                ThrowHttpResponseException("No group access.", HttpStatusCode.Unauthorized);
            }
        }
    }
}