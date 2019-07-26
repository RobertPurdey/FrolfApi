using Application.Command.FrolfGroups.Commands;
using Application.Command.Games;
using Application.Command.Games.Commands;
using Application.Command.Players.Commands;
using Application.Query.Services.FrolfGroups;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Query.Contracts;
using Frolf.Api.Composers.Games;
using Frolf.Api.Mappers;
using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.Models.FrolfGroups;
using Frolf.Api.Models.Games;
using Frolf.Api.Models.Players;
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
        private readonly IQueryService<Player> playerQueryService;

        private readonly ICommandExecutor commandExecutor;
        private readonly IGameComposer gameComposer;

        public FrolfGroupModelDataController(
            IReadWriteEntityMapper<FrolfGroupModel, FrolfGroup> frolfGroupMapping,
            IReadWriteEntityMapper<PlayerModel, Player> playerMapping,
            IReadWriteEntityMapper<GameModel, Game> gameMapping,
            IQueryService<FrolfGroup> frolfGroupService,
            IQueryService<AppUser> appUserService,
            IQueryService<Player> playerService,
            ICommandExecutor cmdExecutor,
            IGameComposer gameComp)
        {
            frolfGroupMapper         = frolfGroupMapping;
            playerMapper             = playerMapping;
            gameMapper               = gameMapping;
            frolfGroupQueryService   = frolfGroupService;
            appUserQueryService      = appUserService;
            playerQueryService       = playerService;
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

            IsCurrentUserInGroupVerify(entity);

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
            var entity = FindEntity(modelToUpdate.IdKey, frolfGroupQueryService);

            // only let the name be changed
            entity.Name = modelToUpdate.Name;

            commandExecutor.Execute( new UpdateFrolfGroupCommand { entity = entity } );
        }

        public IEnumerable<PlayerModel> GetGroupMembers(Guid groupId)
        {
            var group = FindEntity(groupId, frolfGroupQueryService);

            IsCurrentUserInGroupVerify(group);

            foreach ( var player in group.Members.OrderBy(m => m.Handle) )
            {
                var model = new PlayerModel();
                playerMapper.MapToApiModel(model, player);

                yield return model;
            }
        }

        public void IsCurrentUserInGroupVerify(FrolfGroup group)
        {
            var isCurrUserInGroup = group.Members
                .Any(p => p.AppUserId == UserExtensions.GetCurrentUserId());

            if ( !isCurrUserInGroup )
            {
                ThrowHttpResponseException("No group access.", HttpStatusCode.Unauthorized);
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

        public void LeaveGroup(Guid id)
        {
            var groupBeingLeft = FindEntity(id, frolfGroupQueryService);
            var userLeaving    = FindEntity(UserExtensions.GetCurrentUserId(), appUserQueryService);

            var playerId = userLeaving.Players
                .Where(
                    p => p.AppUserId    == userLeaving.EntityKey
                      && p.FrolfGroupId == id)
                .Select(p => p.EntityKey)
                .FirstOrDefault();

            var playerLeaving = FindEntity(playerId, playerQueryService);

            var command = new LeaveFrolfGroupCommand
            {
                FrolfGroup  = groupBeingLeft,
                Player      = playerLeaving
            };

            commandExecutor.Execute(command);
        }

        public void RemovePlayer(RemovePlayerModel removePlayer)
        {
            var groupToRemoveFrom   = FindEntity(removePlayer.FrolfGroupId, frolfGroupQueryService);
            var playerToRemove      = FindEntity(removePlayer.PlayerId, playerQueryService);

            var command = new RemovePlayerCommand
            {
                FrolfGroup  = groupToRemoveFrom,
                Player      = playerToRemove
            };

            commandExecutor.Execute(command);
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