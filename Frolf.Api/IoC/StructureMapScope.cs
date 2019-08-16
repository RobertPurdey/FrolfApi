using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace Frolf.Api.IoC
{
    /**
     * 
     * Setup found at https://marisks.net/2012/08/19/configuring-structuremap-in-aspnet-webapi/
     */
    public class StructureMapScope : IDependencyScope
    {
        private readonly IContainer dependencyContainer;

        internal StructureMapScope(IContainer container)
        {
            dependencyContainer = container ?? throw new ArgumentNullException(nameof(container));
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null)
            {
                return null;
            }

            if (serviceType.IsAbstract || serviceType.IsInterface)
            {
                return dependencyContainer.TryGetInstance(serviceType);
            }

            return dependencyContainer.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return dependencyContainer
                .GetAllInstances(serviceType)
                .Cast<object>();
        }

        public void Dispose()
        {
            dependencyContainer.Dispose();
        }
    }
}