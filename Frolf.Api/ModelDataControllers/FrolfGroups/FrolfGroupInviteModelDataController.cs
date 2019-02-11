using Application.Command.FrolfGroupInvites.Commands;
using Application.Query.Services.FrolfGroups;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Query.Contracts;
using Frolf.Api.Mappers;
using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.Models.FrolfGroups;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Frolf.Api.ModelDataControllers.FrolfGroups
{
    public class FrolfGroupInviteModelDataController :
        ModelDataController<FrolfGroupInviteModel, FrolfGroupInviteFilterModel>,
        IFrolfGroupInviteModelDataController
    {
        private readonly IReadWriteEntityMapper<FrolfGroupInviteModel, FrolfGroupInvite> frolfGroupInviteMapper;
        private readonly IQueryService<FrolfGroupInvite> frolfGroupInviteQueryService;
        private readonly IQueryService<AppUser> appUserQueryService;
        private readonly IQueryService<FrolfGroup> frolfGroupQueryService;
        private readonly ICommandExecutor commandExecutor;

        public FrolfGroupInviteModelDataController(
            IReadWriteEntityMapper<FrolfGroupInviteModel, FrolfGroupInvite> frolfGroupInviteMapping,
            IQueryService<FrolfGroupInvite> frolfGroupInviteService,
            IQueryService<AppUser> appUserService,
            IQueryService<FrolfGroup> frolfGroupService,
            ICommandExecutor cmdExecutor)
        {
            frolfGroupInviteMapper         = frolfGroupInviteMapping;
            frolfGroupInviteQueryService   = frolfGroupInviteService;
            appUserQueryService            = appUserService;
            frolfGroupQueryService         = frolfGroupService;
            commandExecutor                = cmdExecutor;
        }

        public override void Delete(FrolfGroupInviteModel modelToDelete)
        {
            var inviteEntity = FindEntity(modelToDelete.IdKey, frolfGroupInviteQueryService);

            commandExecutor.Execute( new DeleteInviteCommand(inviteEntity) );   
        }

        public override IEnumerable<FrolfGroupInviteModel> GetAll()
        {
            var currUserGuid = UserExtensions.GetCurrentUserId();

            foreach ( var entity in frolfGroupInviteQueryService.GetAll() )
            {
                var isInviteForCurrentUser = entity.InviteeId == currUserGuid;

                if ( isInviteForCurrentUser )
                {
                    var model = new FrolfGroupInviteModel();
                    frolfGroupInviteMapper.MapToApiModel(model, entity);

                    yield return model;
                }
            }       
        }
    
        public override IEnumerable<FrolfGroupInviteModel> GetWithFilter(FrolfGroupInviteFilterModel filter)
        {
            var queryArg         = ConvertToQueryArg(filter);
            var filteredInvites  = frolfGroupInviteQueryService.GetWithQueryArg(queryArg);

            foreach ( var entity in filteredInvites )
            {
                var model = new FrolfGroupInviteModel();
                frolfGroupInviteMapper.MapToApiModel(model, entity);

                yield return model;
            }  
        }

        public override FrolfGroupInviteModel GetById(Guid id)
        {
            var entity = FindEntity(id, frolfGroupInviteQueryService);
            var model  = new FrolfGroupInviteModel();

            frolfGroupInviteMapper.MapToApiModel(model, entity);

            return model;
        }

        public override void Insert(FrolfGroupInviteModel newModel)
        {
            throw new NotImplementedException();
        }

        public override void Update(FrolfGroupInviteModel modelToUpdate)
        {
            throw new NotImplementedException();
        }

        public void Accept(Guid id)
        {
            var inviteEntity = FindEntity(id, frolfGroupInviteQueryService);

            commandExecutor.Execute( new AcceptInviteCommand(inviteEntity) );           
        }

        public void Send(InviteCreationModel creationModel)
        {
            var invitee = appUserQueryService
                .GetAll()
                .Where(u => u.FriendCode == creationModel.FriendCode)
                .SingleOrDefault();

            if ( invitee == null )
            {
                // Vague on purpose as to not give info away as to why
                throw new Exception("Send invite failed.");
            }

            var newInvite = new FrolfGroupInvite
            {
                InviteeId    = invitee.EntityKey,
                InviterId    = UserExtensions.GetCurrentUserId(),
                FrolfGroupId = creationModel.FrolfGroupId
            };

            commandExecutor.Execute( new AddInviteCommand(newInvite) );
        }

        private static FrolfGroupInviteQueryArg ConvertToQueryArg(FrolfGroupInviteFilterModel filter)
        {
            return new FrolfGroupInviteQueryArg
            {
                CurrentUserGuid = UserExtensions.GetCurrentUserId()
            };
        }
    }
}