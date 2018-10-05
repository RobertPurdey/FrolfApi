using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using System;
using System.Linq;
using System.Security.Claims;

namespace Frolf.Api.OAuth
{
    public static class OwinContextExtensions
    {
        public static Guid? GetUserId(this IOwinContext context)
        {
            var userIdString = context.Request.User.Identity.GetUserId();

            return string.IsNullOrEmpty(userIdString)
                ? default(Guid?) 
                : Guid.Parse(userIdString);
        }

        //public static Guid? GetClientId(this IOwinContext context)
        //{
        //    var identity = new ClaimsIdentity(context.Request.User.Identity);

        //    var clientIdClaim = identity.Claims.SingleOrDefault(
        //        c => c.Type == ClaimTypes.GroupSid);

        //    return clientIdClaim == null 
        //        ? default(Guid?)
        //        : Guid.Parse(clientIdClaim.Value);
        //}

        public static bool IsNormalUser(this IOwinContext context)
        {
            return context.Request.User.IsInRole("appuser");
        }
    }
}