using Frolf.Api.ControllerAttributes;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Frolf.Api.Controllers
{
    [Authorize]
    [AppUserLoginAuthorizationFilter]
    public class ControllerBase : ApiController
    {
        protected virtual void ValidateNullArgument(object argument, string message)
        {
            if (argument == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(message)
                });
            }
        }
    }
}