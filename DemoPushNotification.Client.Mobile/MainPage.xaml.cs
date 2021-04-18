using DemoPushNotification.Clients.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DemoPushNotification.Client.Mobile
{
    public partial class MainPage : ContentPage
    {
        private SignalRClientService signalRService;
        private bool enableButton;
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
                signalRService = new SignalRClientService("http://10.0.2.2:62200/Apphub");
                await signalRService.StartConectionAndListenAsync();
            }
            catch (Exception e)
            {
                await DisplayAlert("error", e.Message,"Ok");
                return;
            }

            EnableButton(this, EventArgs.Empty);

            signalRService.NotificationPushed += NotificationReceived;

            signalRService.ConnectionClosed += EnableButton;
            signalRService.ConnectionClosed += DisableButton;
        }

        private async void NotificationReceived(object sender, EventArgs e)
        {
            await DisplayAlert("Notification","You Received Notification" , "Ok");
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

        private void EnableButton(object sender, EventArgs e)
        {
            enableButton = true;
        }

        private void DisableButton(object sender, EventArgs e)
        {
            enableButton = false;
        }
    }
}

