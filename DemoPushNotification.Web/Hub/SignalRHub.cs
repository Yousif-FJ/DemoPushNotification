using Microsoft.AspNetCore.SignalR;

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
