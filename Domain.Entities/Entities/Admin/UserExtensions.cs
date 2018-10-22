using Domain.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Domain.Entities
{
    public static class UserExtensions
    {
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
