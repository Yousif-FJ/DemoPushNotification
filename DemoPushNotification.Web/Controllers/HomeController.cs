using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using DemoPushNotification.Web.Hub;


namespace DemoPushNotification.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHubContext<AppHub, ISignalRClient> _hubContext;

        public HomeController(IHubContext<AppHub, ISignalRClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SendNotification()
        {
            _hubContext.Clients.All.ShowNotification();
            return RedirectToAction(nameof(Index));
        }
    }
}
