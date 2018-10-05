using Frolf.Api.OAuth;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Security.Contracts;
using Security.Jwt;
using System;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http;

namespace Frolf.Api.App_Start
{
    public static class OAuthConfig
    {
        // same as CorsPolicy.AllowAll except for preflightage value
        static CorsPolicy PreflightPolicy = new CorsPolicy
        {
            AllowAnyHeader        = true,
            AllowAnyMethod        = true,
            AllowAnyOrigin        = true,
            SupportsCredentials   = true,
            PreflightMaxAge       = 600
        };

        public static void Configure(IAppBuilder app, HttpConfiguration config)
        {
            app.UseCors(GetCorsOptions());
            SetupAppUserAuthorization(app, config);
        }

        private static void SetupAppUserAuthorization(IAppBuilder app, HttpConfiguration config)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                // todo: allow insecure for initial testing
                AllowInsecureHttp           = true,
                TokenEndpointPath           = new PathString("/oauth2/token"),
                AccessTokenExpireTimeSpan   = TimeSpan.FromMinutes(30), // todo: switch to 1 hour?
                Provider                    = new AppUserAuthorizationServerProvider(config.DependencyResolver),
                AccessTokenFormat           = new CustomJwtFormat("robertpurdey")
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);

            // Consume JWT tokens
            //var key        = (ISecurityKeyProvider)config.DependencyResolver.GetService(typeof(ISecurityKeyProvider));
            var issuer     = "robertpurdey";
            var audience   = "self";
            var secret     = TextEncodings.Base64Url.Decode("IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw");

            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode           = AuthenticationMode.Active,
                    AllowedAudiences             = new[] { audience },
                    IssuerSecurityKeyProviders   = new IIssuerSecurityKeyProvider[]
                    {
                       new SymmetricKeyIssuerSecurityKeyProvider(issuer, secret)
                    }
                });
        }

        private static CorsOptions GetCorsOptions()
        {
            return new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = ctx => Task.FromResult(PreflightPolicy)
                }
            };
        }
    }
}