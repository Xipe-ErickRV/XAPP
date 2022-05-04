using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xapp.Domain.DTOs;
using Xapp.Web.Services;

namespace Xapp.Web.Controllers
{
    public class XipeCoinsController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var obj = new XipeCoinsService();
            var output = await obj.GetProfile(6);

            if (output.StatusCode == 200) 
            {
                var resultOutput = (WalletUser)output.Result;
                return View(resultOutput);
            }
            else
            {
                var message = output.Message;
                return View();
            }
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

