using Application.Command.FrolfGroups.Commands;
using Application.Command.Games;
using Application.Query.Services.FrolfGroups;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Query.Contracts;
using Frolf.Api.Composers.Games;
using Frolf.Api.Mappers;
using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.Models.FrolfGroups;
using Frolf.Api.Models.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Frolf.Api.ModelDataControllers.FrolfGroups
{
    public class FrolfGroupModelDataController :
        ModelDataController<FrolfGroupModel, FrolfGroupFilterModel>,
        IFrolfGroupModelDataController
    {
        private readonly IReadWriteEntityMapper<FrolfGroupModel, FrolfGroup> frolfGroupMapper;
        private readonly IReadWriteEntityMapper<PlayerModel, Player> playerMapper;
        private readonly IReadWriteEntityMapper<GameModel, Game> gameMapper;

        private readonly IQueryService<FrolfGroup> frolfGroupQueryService;
        private readonly IQueryService<AppUser> appUserQueryService;

        private readonly ICommandExecutor commandExecutor;
        private readonly IGameComposer gameComposer;

        public FrolfGroupModelDataController(
            IReadWriteEntityMapper<FrolfGroupModel, FrolfGroup> frolfGroupMapping,
            IReadWriteEntityMapper<PlayerModel, Player> playerMapping,
            IReadWriteEntityMapper<GameModel, Game> gameMapping,
            IQueryService<FrolfGroup> frolfGroupService,
            IQueryService<AppUser> appUserService,
            ICommandExecutor cmdExecutor,
            IGameComposer gameComp) // todo: move this to gamecontroller most likely
        {
            frolfGroupMapper         = frolfGroupMapping;
            playerMapper             = playerMapping;
            gameMapper               = gameMapping;
            frolfGroupQueryService   = frolfGroupService;
            appUserQueryService      = appUserService;
            commandExecutor          = cmdExecutor;
            gameComposer             = gameComp;
        }

        public override void Delete(FrolfGroupModel modelToDelete)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<FrolfGroupModel> GetAll()
        {
            var currUserGuid = UserExtensions.GetCurrentUserId();

            foreach ( var entity in frolfGroupQueryService.GetAll() )
            {
                var isCurrUserInGroup = 
                    entity.Members.Any(gm => gm.AppUserId == currUserGuid);

                if ( isCurrUserInGroup )
                {
                    var model = new FrolfGroupModel();
                    frolfGroupMapper.MapToApiModel(model, entity);

                    yield return model;
                }
            }       
        }

        public override IEnumerable<FrolfGroupModel> GetWithFilter(FrolfGroupFilterModel filter)
        {
            throw new NotImplementedException();
        }

        public override FrolfGroupModel GetById(Guid id)
        {
            var entity = FindEntity(id, frolfGroupQueryService);
            var model  = new FrolfGroupModel();

            frolfGroupMapper.MapToApiModel(model, entity);

            return model;
        }

        public override void Insert(FrolfGroupModel newModel)
        {
            var entity = new FrolfGroup();

            frolfGroupMapper.MapToEntity(newModel, entity);

            // Add current user to the group
            entity.Members.Add( CreateInitialGroupMember() );

            var addCommand  = new AddFrolfGroupCommand { newEntity = entity };
            commandExecutor.Execute(addCommand);

            newModel.IdKey = addCommand.newEntity.EntityKey;
        }

        public override void Update(FrolfGroupModel modelToUpdate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PlayerModel> GetGroupMembers(Guid groupId)
        {
            var group = FindEntity(groupId, frolfGroupQueryService);

            var isCurrUserInGroup = group.Members
                .Any(p => p.AppUserId == UserExtensions.GetCurrentUserId() );

            if ( !isCurrUserInGroup )
            {
                ThrowHttpResponseException(
                    "You can't access groups you don't belong in.",
                    HttpStatusCode.Unauthorized);
            }

            foreach ( var player in group.Members )
            {
                var model = new PlayerModel();
                playerMapper.MapToApiModel(model, player);

                yield return model;
            }
        }

        public GameModel CreateGame(GameCreationModel model)
        {
            var game       = gameComposer.NewGame(model);
            var addCommand = new AddGameCommand { NewGame = game };

            commandExecutor.Execute(addCommand);

            var newGameModel = new GameModel();
            gameMapper.MapToApiModel(newGameModel, game);

            return newGameModel;
        }

        private Player CreateInitialGroupMember()
        {
            var currUser     = UserExtensions.GetCurrentUser();
            var currAppUser  = FindEntity(currUser.EntityKey, appUserQueryService);

            return new Player
            {
                EntityKey = Guid.NewGuid(),
                AppUserId = currAppUser.EntityKey,
                GroupRole = GroupRole.Administrator,
                Handle    = currAppUser.Handle
            };
        }

        private static FrolfGroupQueryArg ConvertToQueryArg(FrolfGroupFilterModel filter)
        {
            return new FrolfGroupQueryArg
            {
                // todo: frolf group mappings
            };
        }
    }
}