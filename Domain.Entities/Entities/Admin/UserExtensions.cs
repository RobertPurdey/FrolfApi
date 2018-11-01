using Domain.Entities.Contracts;
using System;
using System.Security.Claims;
using System.Threading;
using System.Web;

namespace Domain.Entities
{
    public static class UserExtensions
    {
        public static IAppUser GetCurrentUser()
        {
            IAppUser user = null;

            if (HttpContext.Current != null)
            {
                user = HttpContext.Current.User as IAppUser;
            }

            return user;
        }

        public static Guid GetCurrentUserGuid()
        {
            var claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
            var userGuidClaim   = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);

            return Guid.Parse(userGuidClaim.Value);
        }

        public static void SetCurrentUser(IAppUser user)
        {
            Thread.CurrentPrincipal = user;

            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = user;
            }
        }

        public static void ImpersonateAdmin(Guid userId)
        {
            SetCurrentUser(new AppUser
            {
                EntityKey = userId
            });
        }
    }
}
