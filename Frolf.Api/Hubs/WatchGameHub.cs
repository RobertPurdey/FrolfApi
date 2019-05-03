using Domain.Entities;
using Frolf.Api.ControllerAttributes;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Frolf.Api.Hubs
{
    //[Authorize]
    //[AppUserLoginAuthorizationFilter]
    [HubName("watchGame")]
    public class WatchGameHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public override Task OnConnected()
        {
            //var userId = UserExtensions.GetCurrentUserId();

            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            return base.OnDisconnected();
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }
    }
}