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
using System.Web.Http;

namespace Frolf.Api.ModelDataControllers.Users
{
    public interface IAppUserPublicKeyRetriever
    {
        string GetXmlRsaPublicKey();
    }

    public class AppUserModelDataController : IAppUserModelDataController, IAppUserPublicKeyRetriever
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

        public string GetXmlRsaPublicKey()
        {
            var currentUser = FindAppUser(UserExtensions.GetCurrentUserId());

            return currentUser.XmlPublicKey;
        }

        public void SetPublicKey(PublicKeyModel keyModel)
        {
            var user        = FindAppUser( UserExtensions.GetCurrentUserId() );
            user.XmlPublicKey = keyModel.XmlRsaPublicKey;

            commandExecutor.Execute(new UpdateAppUserCommand { User = user });
        }

        public AppUser CreateAccount(AppUserCreationModel creationRequest)
        {
            var newUser         = appUserComposer.NewAppUser(creationRequest);
            var newUserModel    = new AppUserModel();

            commandExecutor.Execute( new AddAppUserCommand { NewUser = newUser } );
            appUserMapper.MapToApiModel(newUserModel, newUser);

            return newUser;
        }

        public void UpdateAccount(AppUserUpdateModel updateModel)
        {
            var entity = FindAppUser(updateModel.IdKey);

            entity.LoginName    = updateModel.LoginName;
            entity.Handle       = updateModel.Handle;

            commandExecutor.Execute( new UpdateAppUserCommand { User = entity } );
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