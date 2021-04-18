using DemoPushNotification.Clients.Common;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace DemoPushNotification.Clients.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string Url = "http://localhost:62200/Apphub";
        private SignalRClientService signalRService;
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override async void OnContentRendered(EventArgs e)
        {
            await InitializeSignalR();

            base.OnContentRendered(e);
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
                MessageBox.Show($"error :{e.Message} ");
                return;
            }

            signalRService.NotificationPushed += NotificationReceived;
        }

        private void NotificationReceived(object sender, EventArgs e)
        {
            MessageBox.Show("Notification received");
        }


        private async void OnClick_btn_SendNotification(object sender, RoutedEventArgs e)
        {
            await signalRService.SendNotificationAsync();
        }
    }
}
