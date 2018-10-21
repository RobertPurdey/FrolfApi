using Domain.Entities;
using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.Models.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Frolf.Api.Controllers
{
    [RoutePrefix("api/appusers")]
    public class AppUserController : EditControllerBase<AppUserModel>
    {
        private readonly IAppUserModelDataController appUserModelDataController;

        public AppUserController(
            IAppUserModelDataController appUserDataController)
        {
            appUserModelDataController = appUserDataController;
        }

        [HttpGet]
        [Route("{id:guid}")]
        public override Task<AppUserModel> GetById([FromUri] Guid id)
        {
            var foundAppUser = appUserModelDataController.GetById(id);

            return Task.FromResult(foundAppUser);
        }

        [HttpGet]
        [Route("friends")]
        public Task<IEnumerable<AppUserModel>> GetAppUsers()
        {
            var allUsers = appUserModelDataController.GetAll();

            return Task.FromResult(allUsers);
        }

        protected override Task<AppUserModel> Create([FromBody] AppUserModel newItem)
        {
            throw new NotImplementedException();
        }

        protected override Task Remove([FromUri] Guid id)
        {
            throw new NotImplementedException();
        }

        protected override Task Update([FromBody] AppUserModel newDetails)
        {
            throw new NotImplementedException();
        }
    }
}