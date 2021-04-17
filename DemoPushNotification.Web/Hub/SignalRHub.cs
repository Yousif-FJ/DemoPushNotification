using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoPushNotification.Web.Hub
{
    public class AppHub : Hub<ISignalRClient>
    {
        public void AlertAllClients()
        {
            Clients.All.ShowNotification();
        }
    }
}
