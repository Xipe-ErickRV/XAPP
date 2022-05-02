using Microsoft.AspNetCore.Mvc;

namespace Xapp.Web.Services
{
    public class FeedService : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
