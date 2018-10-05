using System.Web.Http;
using Frolf.Api.App_Start;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Frolf.Api.Startup))]
namespace Frolf.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            IocConfig.Configure(config);
            WebApiConfig.Configure(config);
            OAuthConfig.Configure(app, config);

            // todo: redo this in similar fashion to above classes
            //ConfigureAuth(app);

            app.UseWebApi(config);
        }
    }
}
