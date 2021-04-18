using System.Threading.Tasks;

namespace DemoPushNotification.Web.Hub
{
    public interface ISignalRClient
    {
        Task ShowNotification();
    }
}
