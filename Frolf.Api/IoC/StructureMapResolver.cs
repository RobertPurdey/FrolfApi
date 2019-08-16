using StructureMap;
using System.Web.Http.Dependencies;

namespace Frolf.Api.IoC
{
    public class StructureMapResolver : StructureMapScope, IDependencyResolver
    {
        private readonly IContainer dependencyContainer;

        public StructureMapResolver(IContainer container)
            : base(container)
        {
            dependencyContainer = container;
        }

        public IDependencyScope BeginScope()
        {
            var childContainer    = dependencyContainer.GetNestedContainer();
            childContainer.Name   = "child container";
            var scope             = new StructureMapScope(childContainer);

            // Injecting IDependencyScope will give access to dependencies
            childContainer.Inject(typeof(IDependencyScope), scope);

            return scope;
        }
    }
}