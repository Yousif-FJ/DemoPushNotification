using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace DemoPushNotification.Clients.Common
{
    public class SignalRClientService
    {
        private readonly HubConnection connection;
        public event EventHandler NotificationPushed;

        public SignalRClientService()
        {
            connection = new HubConnectionBuilder()
               .WithUrl("http://localhost:53353/AppHub")
               .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
        }

        public async Task StartConectionAndListen()
        {
            await connection.StartAsync();
            connection.On<EventArgs>("ShowNotification", OnInvokeNotification);
        }


        public async Task SendNotificationAsync()
        {
            await connection.InvokeAsync("AlertAllClients");
        }

        private void OnInvokeNotification(EventArgs e = null)
        {
            e = EventArgs.Empty;
            NotificationPushed?.Invoke(this, e);
        }
    }
}
