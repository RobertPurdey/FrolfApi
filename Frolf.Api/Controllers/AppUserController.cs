using Domain.Entities;
using Frolf.Api.ModelDataControllers.Contracts;
using Frolf.Api.Models.Users;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Frolf.Api.Controllers
{
    [RoutePrefix("api/appusers")]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserModelDataController appUserModelDataController;

        public AppUserController(
            IAppUserModelDataController appUserDataController)
        {
            appUserModelDataController = appUserDataController;
        }

        [HttpGet]
        [Route("{id:guid}")]
        public Task<AppUserModel> GetById([FromUri] Guid id)
        {
            var foundAppUser = appUserModelDataController.GetById(id);

            return Task.FromResult(foundAppUser);
        }
    }
}