using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Domain.Entities;
using Domain.Query.Contracts;
using Frolf.Api.OAuth;
using Frolf.Api.Resources;

namespace Frolf.Api.ControllerAttributes
{
    public class AppUserLoginAuthorizationFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var success = true;

            if ( actionContext.Request.GetOwinContext().IsNormalUser() )
            {
                success = LoginAsNormalUser(actionContext);
            }

            if ( !success )
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    // Provide vague information on login failure
                    Content = new StringContent(MessageResource.UknownTokenUserError)
                };

                return;
            }

            base.OnAuthorization(actionContext);
        }

        private static bool LoginAsNormalUser(HttpActionContext actionContext)
        {
            var owinContext = actionContext.Request.GetOwinContext();
            var userId      = owinContext.GetUserId();

            if (!userId.HasValue)
            {
                return false;
            }

            var queryService = (IQueryService<AppUser>)actionContext
                .Request
                .GetDependencyScope()
                .GetService(typeof(IQueryService<AppUser>));

            var user = queryService
                .GetAll()
                .SingleOrDefault(u => u.EntityKey == userId.Value);

            if (user == null)
            {
                return false;
            }

            user.SetAsCurrentUser();

            return true;
        }
    }
}