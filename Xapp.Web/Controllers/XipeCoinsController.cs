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
            var output = await obj.GetProfile(12);

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

        [HttpGet]
        public async Task<IActionResult> account()
        {
            var obj = new XipeCoinsService();
            var output = await obj.GetProfile(12);

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
        public async Task<IActionResult> convenios()
        {
            var obj = new XipeCoinsService();
            var output = await obj.GetProfile(12);

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

        /*[HttpPost]
        public async Task<IActionResult> transaction(TransferInput dto)
        {
            /*var obj = new XipeCoinsService();
            var output = await obj.PostTransfer(12, dto);

            if (output.StatusCode == 200)
            {
                var resultOutput = output.Result;
                return View(resultOutput);
            }
            else
            {
                var message = output.Message;
                return View();
            }

        }*/

        public IActionResult transaction()
        {
            return View();
        }
        
    



    }
}
