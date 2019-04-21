using Application.Command.AppUsers.Commands;
using Domain.Commands.Contracts;
using Domain.Entities;
using Domain.Query.Contracts;
using Frolf.Api.Composers.Users;
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
        private readonly IUserComposer appUserComposer;
        private readonly ICommandExecutor commandExecutor;

        public AppUserModelDataController(
            IReadWriteEntityMapper<AppUserModel, AppUser> appUserMapping,
            IQueryService<AppUser> appUserService,
            IUserComposer userComposer,
            ICommandExecutor cmdExecutor
            )
        {
            appUserMapper                 = appUserMapping;
            appUserQueryService           = appUserService;
            appUserComposer               = userComposer;
            commandExecutor               = cmdExecutor;
        }

        public void Delete(AppUserModel modelToDelete)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AppUserModel> GetAll()
        {
            foreach (var entity in appUserQueryService.GetAll())
            {
                var model = new AppUserModel();
                appUserMapper.MapToApiModel(model, entity);

                yield return model;
            }       
        }

        public IEnumerable<AppUserModel> GetWithFilter(AppUserFilterModel filter)
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

        public AppUser CreateAccount(AppUserCreationModel creationRequest)
        {
            var newUser         = appUserComposer.NewAppUser(creationRequest);
            var newUserModel    = new AppUserModel();

            commandExecutor.Execute( new AddAppUserCommand { NewUser = newUser } );
            appUserMapper.MapToApiModel(newUserModel, newUser);

            return newUser;
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