using Microsoft.AspNetCore.Mvc;

namespace Xapp.Web.Controllers
{
    public class XipeCoinsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult account()
        {
            return View();
        }
        public IActionResult convenios()
        {
            return View();
        }
        public IActionResult transaction()
        {
            return View();
        }
    }   

}

