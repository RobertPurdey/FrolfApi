using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Security.Resources;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Security.OAuth
{
    public abstract class AuthorizationServerProviderBase : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if ( !IsValidClient(context) )
            {
                context.SetError("invalid_client", MessageResource.InvalidOWinClientError);

                return Task.FromResult<object>(null);
            }

            context.Validated();

            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if ( !IsValidUser(context) )
            {
                SetInvalidUserError(context);

                return Task.FromResult<object>(null);
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            OnClaimsIdentityCreated(context, identity);

            var authTicket = new AuthenticationTicket(identity, null);
            context.Validated(authTicket);

            return Task.FromResult<object>(null);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            if ( context.Ticket == null )
            {
                context.SetError("invalid_grant", MessageResource.MissingPreviousTicketForRefreshToken);

                return Task.FromResult<object>(null);
            }

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                         "audience", "self"
                    }
                });


            var newTicket = new AuthenticationTicket(context.Ticket.Identity, props);

            OnNewTokenCreatedWithRefreshToken(newTicket);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

        protected abstract bool IsValidClient(OAuthValidateClientAuthenticationContext context);
        protected abstract bool IsValidUser(OAuthGrantResourceOwnerCredentialsContext context);

        protected virtual void OnNewTokenCreatedWithRefreshToken(AuthenticationTicket newTicket)
        {
        }

        protected virtual void OnClaimsIdentityCreated(
            OAuthGrantResourceOwnerCredentialsContext context,
            ClaimsIdentity identity)
        {
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
        }

        protected static void SetInvalidUserError(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.SetError("invalid_grant", MessageResource.InvalidOWinCredentialError);
        }
    }
}
