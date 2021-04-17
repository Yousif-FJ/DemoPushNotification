using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoPushNotification.Web.Hub
{
    public interface ISignalRClient 
    {
        void ShowNotification();
    }
}
