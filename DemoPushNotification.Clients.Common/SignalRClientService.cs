using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace DemoPushNotification.Clients.Common
{
    public class SignalRClientService
    {
        private readonly HubConnection connection;

        public event EventHandler NotificationPushed;

        public SignalRClientService(string url)
        {

            connection = new HubConnectionBuilder()
               .WithUrl(url)
               .Build();


            connection.Closed += async (error) =>
            {
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
