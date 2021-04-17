using DemoPushNotification.Clients.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoPushNotification.Clients.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SignalRClientService signalRService;
        public MainWindow()
        {
            signalRService = new SignalRClientService();
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
                await signalRService.StartConectionAndListenAsync();
            }
            catch (Exception e)
            {
                MessageBox.Show($"error :{e.Message} ");
                return;
            }

            EnableButton(this,EventArgs.Empty);

            signalRService.NotificationPushed += NotificationReceived;

            signalRService.ConnectionClosed += EnableButton;
            signalRService.ConnectionClosed += DisableButton;
        }

        private void NotificationReceived(object sender, EventArgs e)
        {
            MessageBox.Show("Notification received");
        }


        private async void OnClick_btn_SendNotification(object sender, RoutedEventArgs e)
        {
            await signalRService.SendNotificationAsync();
        }

        private void EnableButton(object sender, EventArgs e)
        {
            btn_SendNotification.IsEnabled = true;
        }

        private void DisableButton(object sender, EventArgs e)
        {
            btn_SendNotification.IsEnabled = false;
        }
    }
}
