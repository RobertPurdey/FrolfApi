using Application.Command.FrolfGroups.Commands;
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
using System.Net;
using System.Web.Http;

namespace Frolf.Api.ModelDataControllers.FrolfGroups
{
    public class FrolfGroupModelDataController :
        ModelDataController<FrolfGroupModel, FrolfGroupFilterModel>,
        IFrolfGroupModelDataController
    {
        private readonly IReadWriteEntityMapper<FrolfGroupModel, FrolfGroup> frolfGroupMapper;
        private readonly IQueryService<FrolfGroup> frolfGroupQueryService;
        private readonly IQueryService<AppUser> appUserQueryService;
        private readonly ICommandExecutor commandExecutor;

        public FrolfGroupModelDataController(
            IReadWriteEntityMapper<FrolfGroupModel, FrolfGroup> frolfGroupMapping,
            IQueryService<FrolfGroup> frolfGroupService,
            IQueryService<AppUser> appUserService,
            ICommandExecutor cmdExecutor)
        {
            frolfGroupMapper         = frolfGroupMapping;
            frolfGroupQueryService   = frolfGroupService;
            appUserQueryService      = appUserService;
            commandExecutor          = cmdExecutor;
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

            commandExecutor.Execute( new AddFrolfGroupCommand{ newEntity = entity } );
        }

        public override void Update(FrolfGroupModel modelToUpdate)
        {
            throw new NotImplementedException();
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