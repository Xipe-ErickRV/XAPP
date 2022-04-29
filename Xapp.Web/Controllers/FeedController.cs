using Microsoft.AspNetCore.Mvc;

namespace Xapp.Web.Controllers
{
    public class FeedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Post()
        {
            return View();
        }
    }
}
