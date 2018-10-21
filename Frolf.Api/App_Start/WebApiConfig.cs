using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Frolf.Api.MessageHandlers;
using Frolf.Api.Routing;
using Microsoft.Owin.Security.OAuth;

namespace Frolf.Api
{
    public static class WebApiConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes(new DirectRouteProvider());

            config.Routes.MapHttpRoute(
                name:           "DefaultApi",
                routeTemplate:  "api/{controller}/{id}",
                defaults:       new { id = RouteParameter.Optional }
            );

            RegisterHandlers(config);
        }

        private static void RegisterHandlers(HttpConfiguration config)
        {
            config.Services.Replace(
                typeof(IExceptionHandler),
                new GeneralExceptionHandler());
        }
    }
}
