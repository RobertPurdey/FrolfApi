using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace Frolf.Api.Routing
{
    public class DirectRouteProvider : DefaultDirectRouteProvider
    {
        protected override IReadOnlyList<IDirectRouteFactory> GetActionRouteFactories(
            HttpActionDescriptor actionDescriptor)
        {
            // inherit route attributes decorated on base class controller's actions
            return actionDescriptor.GetCustomAttributes<IDirectRouteFactory>(true);
        }
    }
}