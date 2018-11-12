using Domain.Entities;
using Domain.Query.Contracts;
using Microsoft.Owin.Security.OAuth;
using Security.OAuth;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http.Dependencies;

namespace Frolf.Api.OAuth
{
    public class AppUserAuthorizationServerProvider : AuthorizationServerProviderBase
    {
        private readonly IDependencyResolver serviceLocator;

        public AppUserAuthorizationServerProvider(IDependencyResolver resolver)
        {
            serviceLocator = resolver;
        }

        protected override bool IsValidClient(OAuthValidateClientAuthenticationContext context)
        {
            // client_id is not used
            return true;
        }

        protected override bool IsValidUser(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var appUser = FindAppUser(context);

            return appUser != null;
        }

        protected override void OnClaimsIdentityCreated(
            OAuthGrantResourceOwnerCredentialsContext context,
            ClaimsIdentity identity)
        {
            base.OnClaimsIdentityCreated(context, identity);

            var user = FindAppUser(context);

            if (user == null)
            {
                SetInvalidUserError(context);
                return;
            }

            identity.AddClaim(new Claim(ClaimTypes.Name, user.LoginName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.EntityKey.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Role, "appuser"));
        }

        private AppUser FindAppUser(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var appUserRepo = (IQueryService<AppUser>)serviceLocator.GetService(typeof(IQueryService<AppUser>));

            try
            {
                var foundUser = appUserRepo.GetAll().SingleOrDefault(
                    u => u.LoginName == context.UserName
                      && u.Password  == context.Password);

                return foundUser;
            }
            finally
            {
                UserExtensions.SetCurrentUser(null);
            }
        }
    }
}