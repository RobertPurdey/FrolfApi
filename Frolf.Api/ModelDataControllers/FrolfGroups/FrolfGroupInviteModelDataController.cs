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

namespace Frolf.Api.ModelDataControllers.FrolfGroups
{
    public class FrolfGroupInviteModelDataController :
        ModelDataController<FrolfGroupInviteModel, FrolfGroupInviteFilterModel>,
        IFrolfGroupInviteModelDataController
    {
        private readonly IReadWriteEntityMapper<FrolfGroupInviteModel, FrolfGroupInvite> frolfGroupInviteMapper;
        private readonly IQueryService<FrolfGroupInvite> frolfGroupInviteQueryService;
        private readonly ICommandExecutor commandExecutor;

        public FrolfGroupInviteModelDataController(
            IReadWriteEntityMapper<FrolfGroupInviteModel, FrolfGroupInvite> frolfGroupInviteMapping,
            IQueryService<FrolfGroupInvite> frolfGroupInviteService,
            ICommandExecutor cmdExecutor)
        {
            frolfGroupInviteMapper         = frolfGroupInviteMapping;
            frolfGroupInviteQueryService   = frolfGroupInviteService;
            commandExecutor                = cmdExecutor;
        }

        public override void Delete(FrolfGroupInviteModel modelToDelete)
        {
            throw new NotImplementedException();
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

        public void Decline(Guid id)
        {
            var inviteEntity = FindEntity(id, frolfGroupInviteQueryService);

            commandExecutor.Execute( new DeleteInviteCommand(inviteEntity) );   
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