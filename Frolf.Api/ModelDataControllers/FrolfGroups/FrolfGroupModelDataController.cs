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
    public class FrolfGroupModelDataController : IFrolfGroupModelDataController
    {
        private readonly IReadWriteEntityMapper<FrolfGroupModel, FrolfGroup> frolfGroupMapper;
        private readonly IQueryService<FrolfGroup> frolfGroupQueryService;

        public FrolfGroupModelDataController(
            IReadWriteEntityMapper<FrolfGroupModel, FrolfGroup> frolfGroupMapping,
            IQueryService<FrolfGroup> frolfGroupService
            )
        {
            frolfGroupMapper         = frolfGroupMapping;
            frolfGroupQueryService   = frolfGroupService;
        }

        public void Delete(FrolfGroupModel modelToDelete)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FrolfGroupModel> GetAll()
        {
            var currUserGuid = UserExtensions.GetCurrentUserGuid();

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

        public FrolfGroupModel GetById(Guid id)
        {
            var entity = FindFrolfGroup(id);
            var model  = new FrolfGroupModel();

            frolfGroupMapper.MapToApiModel(model, entity);

            return model;
        }

        public void Insert(FrolfGroupModel newModel)
        {
            throw new NotImplementedException();
        }

        public void Update(FrolfGroupModel modelToUpdate)
        {
            throw new NotImplementedException();
        }

        private FrolfGroup FindFrolfGroup(Guid entityKey)
        {
            var entity = frolfGroupQueryService
                .GetAll()
                .SingleOrDefault(u => u.EntityKey == entityKey);

            if (entity == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return entity;
        }
    }
}