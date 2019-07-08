using Domain.Entities;
using Domain.Query.Contracts;
using Microsoft.Owin.Security.OAuth;
using Security.Contracts;
using Security.OAuth;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
            var rsa         = (IRsaEncryptionManager)serviceLocator.GetService(typeof(IRsaEncryptionManager));
            var rsaInfo     = (IRsaKeyInfo)serviceLocator.GetService(typeof(IRsaKeyInfo));

            // decrypt login attempt
            var encrytpedLoginName  = Convert.FromBase64String(context.UserName);
            var loginNameBytes      = rsa.Decrypt(rsaInfo.GetPrivateKeyXml(), encrytpedLoginName);
            var loginName           = Encoding.UTF8.GetString(loginNameBytes);

            try
            {
                var foundUser = appUserRepo.GetAll().SingleOrDefault(
                    u => u.LoginName == loginName);

                // decrypt password attempt before comparing
                var encrytpedPassword = Convert.FromBase64String(context.Password);
                var passwordBytes     = rsa.Decrypt(rsaInfo.GetPrivateKeyXml(), encrytpedPassword);
                var password          = Encoding.UTF8.GetString(passwordBytes);

                var hasher         = (IHashManager)serviceLocator.GetService(typeof(IHashManager));
                var hashedPassword = hasher.Hash(password + foundUser.Salt);

                if (foundUser.Password != hashedPassword) throw new Exception();

                return foundUser;
            }
            finally
            {
                UserExtensions.SetCurrentUser(null);
            }
        }
    }
}