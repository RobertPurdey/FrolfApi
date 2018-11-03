using Application.Query.Services.FrolfGroups;
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
    public class FrolfGroupInviteModelDataController : IFrolfGroupInviteModelDataController
    {
        private readonly IReadWriteEntityMapper<FrolfGroupInviteModel, FrolfGroupInvite> frolfGroupInviteMapper;
        private readonly IQueryService<FrolfGroupInvite> frolfGroupInviteQueryService;

        public FrolfGroupInviteModelDataController(
            IReadWriteEntityMapper<FrolfGroupInviteModel, FrolfGroupInvite> frolfGroupInviteMapping,
            IQueryService<FrolfGroupInvite> frolfGroupInviteService
            )
        {
            frolfGroupInviteMapper         = frolfGroupInviteMapping;
            frolfGroupInviteQueryService   = frolfGroupInviteService;
        }

        public void Delete(FrolfGroupInviteModel modelToDelete)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FrolfGroupInviteModel> GetAll()
        {
            var currUserGuid = UserExtensions.GetCurrentUserGuid();

            foreach ( var entity in frolfGroupInviteQueryService.GetAll() )
            {
                var isInviteForCurrentUser = entity.AppUserId == currUserGuid;

                if ( isInviteForCurrentUser )
                {
                    var model = new FrolfGroupInviteModel();
                    frolfGroupInviteMapper.MapToApiModel(model, entity);

                    yield return model;
                }
            }       
        }
    
        public IEnumerable<FrolfGroupInviteModel> GetWithFilter(FrolfGroupInviteFilterModel filter)
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

        public FrolfGroupInviteModel GetById(Guid id)
        {
            var entity = FindFrolfGroupInvite(id);
            var model  = new FrolfGroupInviteModel();

            frolfGroupInviteMapper.MapToApiModel(model, entity);

            return model;
        }

        public void Insert(FrolfGroupInviteModel newModel)
        {
            throw new NotImplementedException();
        }

        public void Update(FrolfGroupInviteModel modelToUpdate)
        {
            throw new NotImplementedException();
        }

        private FrolfGroupInvite FindFrolfGroupInvite(Guid entityKey)
        {
            var entity = frolfGroupInviteQueryService
                .GetAll()
                .SingleOrDefault(u => u.EntityKey == entityKey);

            if (entity == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return entity;
        }

        private static FrolfGroupInviteQueryArg ConvertToQueryArg(FrolfGroupInviteFilterModel filter)
        {
            return new FrolfGroupInviteQueryArg
            {
                CurrentUserGuid = UserExtensions.GetCurrentUserGuid(),
                InviteStatus    = filter.InviteStatus
            };
        }
    }
}