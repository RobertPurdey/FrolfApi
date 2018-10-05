using Domain.Entities;
using Domain.Query.Contracts;
using Frolf.Api.Mappers;
using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace Frolf.Api.ModelDataControllers.Users
{
    public class AppUserModelDataController : IAppUserModelDataController
    {
        private readonly IReadWriteEntityMapper<AppUserModel, AppUser> appUserMapper;
        private readonly IQueryService<AppUser> appUserQueryService;

        public AppUserModelDataController(
            IReadWriteEntityMapper<AppUserModel, AppUser> appUserMapping,
            IQueryService<AppUser> appUserService
            )
        {
            appUserMapper                 = appUserMapping;
            appUserQueryService           = appUserService;
        }

        public void Delete(AppUserModel modelToDelete)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AppUserModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public AppUserModel GetById(Guid id)
        {
            var entity = FindAppUser(id);
            var model  = new AppUserModel();

            appUserMapper.MapToApiModel(model, entity);

            return model;
        }

        public void Insert(AppUserModel newModel)
        {
            throw new NotImplementedException();
        }

        public void Update(AppUserModel modelToUpdate)
        {
            throw new NotImplementedException();
        }

        private AppUser FindAppUser(Guid entityKey)
        {
            var entity = appUserQueryService
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