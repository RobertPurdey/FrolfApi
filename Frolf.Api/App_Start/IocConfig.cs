using Frolf.Api.IoC;
using StructureMap;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace Frolf.Api.App_Start
{
    public static class IocConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            var container = new Container(new DefaultRegistry())
            {
                Name = "root container"
            };

            config.DependencyResolver = new StructureMapResolver(container);

            container.Inject(
                typeof(IDependencyScope),
                config.DependencyResolver);
        }
    }
}