using Domain.Commands.Contracts;
using Frolf.Api.OAuth;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
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
        static readonly CorsPolicy PreflightPolicy = new CorsPolicy
        {
            AllowAnyHeader       = true,
            AllowAnyMethod       = true,
            AllowAnyOrigin       = true,
            SupportsCredentials  = true,
            PreflightMaxAge      = 600
        };

        public static void Configure(IAppBuilder app, HttpConfiguration config)
        {
            var keyProvider = (ISecurityKeyProvider)config.DependencyResolver.GetService(typeof(ISecurityKeyProvider));

            app.UseCors(GetCorsOptions());

            SetupAuthorizationServer(app, config, keyProvider);
            SetupBearerAuthentication(app, config, keyProvider);
        }

        private static void SetupAuthorizationServer(
            IAppBuilder app,
            HttpConfiguration config,
            ISecurityKeyProvider keyProvider)
        {
            var resolver = config.DependencyResolver;

            var OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                // todo: allow insecure for initial testing then change to https to deploy. better way for it to happen?
                AllowInsecureHttp           = true,
                TokenEndpointPath           = new PathString("/oauth2/token"),
                AccessTokenExpireTimeSpan   = TimeSpan.FromMinutes(1), // todo: switch to 1 hour?
                AccessTokenFormat           = new JwtTokenFormat(keyProvider),
                Provider                    = new AppUserAuthorizationServerProvider(config.DependencyResolver),
                RefreshTokenProvider        = new AppUserRefreshTokenProvider(
                    (ICommandExecutor)resolver.GetService(typeof(ICommandExecutor)), 
                    (ICommandLocator)resolver.GetService(typeof(ICommandLocator)),
                    (IEncryptionManager)resolver.GetService(typeof(IEncryptionManager)) )
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
        }

        private static void SetupBearerAuthentication(
            IAppBuilder app,
            HttpConfiguration config,
            ISecurityKeyProvider keyProvider)
        {
            var jwtBearerOptions = new JwtBearerAuthenticationOptions
            {
                AuthenticationMode        = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters
                {
                    LifetimeValidator = (before, expires, token, parameters)
                        => before.Value <= DateTime.UtcNow
                        && expires.Value >= DateTime.UtcNow,

                    ValidateIssuer             = false,
                    ValidateAudience           = false,
                    ValidateIssuerSigningKey   = true,
                    IssuerSigningKey           = keyProvider.GetSigningKey()
                }
            };

            app.UseJwtBearerAuthentication(jwtBearerOptions);
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