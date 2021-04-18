using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace DemoPushNotification.Clients.Common
{
    public class SignalRClientService
    {
        private readonly HubConnection connection;

        public event EventHandler NotificationPushed;
        public event EventHandler ConnectionClosed;
        public event EventHandler ReConnected;

        public SignalRClientService(string url)
        {
            connection = new HubConnectionBuilder()
               .WithUrl(url)
               .Build();


            connection.Reconnected += (error) =>
            {
                ReConnected?.Invoke(this, EventArgs.Empty);
                return Task.CompletedTask;
            };

            connection.Closed += async (error) =>
            {
                ConnectionClosed?.Invoke(this, EventArgs.Empty);

                await Task.Delay(3000);
                await connection.StartAsync();
            };
        }

        public async Task StartConectionAndListenAsync()
        {
            await connection.StartAsync();
            connection.On("ShowNotification", () => { OnInvokeNotification(); });
        }


        public async Task SendNotificationAsync()
        {
            await connection.InvokeAsync("AlertAllClients");
        }

        private void OnInvokeNotification()
        {
            NotificationPushed?.Invoke(this, EventArgs.Empty);
        }

    }
}
