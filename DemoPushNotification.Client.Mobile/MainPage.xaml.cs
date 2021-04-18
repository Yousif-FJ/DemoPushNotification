using DemoPushNotification.Clients.Common;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DemoPushNotification.Client.Mobile
{
    public partial class MainPage : ContentPage
    {
        private const string Url = "http://10.0.2.2:62200/Apphub";
        private SignalRClientService signalRService;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            await InitializeSignalR();

            base.OnAppearing();
        }

        private async Task InitializeSignalR()
        {
            try
            {
                signalRService = new SignalRClientService(Url);
                await signalRService.StartConectionAndListenAsync();
            }
            catch (Exception e)
            {
                await DisplayAlert("error", e.Message, "Ok");
                return;
            }

            signalRService.NotificationPushed += NotificationReceived;
        }

        private async void NotificationReceived(object sender, EventArgs e)
        {
            await DisplayAlert("Notification", "You Received Notification", "Ok");
        }


        private async void OnClick_btn_SendNotification(object sender, EventArgs args)
        {
            try
            {
                await signalRService.SendNotificationAsync();
            }
            catch (Exception e)
            {
                await DisplayAlert("error", e.Message, "Ok");
                return;
            }
        }
    }
}

