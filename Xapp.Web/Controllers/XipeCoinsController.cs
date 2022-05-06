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
            var user_id = new PerfilService();
            var user = await user_id.GetPerfil(User.Identity.Name);
            var obj = new XipeCoinsService();
            var output = await obj.GetProfile(user.Result.Id);

            if (output.StatusCode == 200)
            {
                var resultOutput = (AccountOutput)output.Result;
                return View(resultOutput);
            }
            else
            {
                var message = output.Message;
                return View();
            }
        }

        /*[HttpGet]
        public async Task<IActionResult> account()
        {
            var user_id = new PerfilService();
            var user = await user_id.GetPerfil(User.Identity.Name);
            var obj = new XipeCoinsService();
            var output = await obj.GetProfile(user.Result.Id);

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

        }*/

        [HttpGet]
        public async Task<IActionResult> account(AccountOutput dto)
        {
            var user_id = new PerfilService();
            var user = await user_id.GetPerfil(User.Identity.Name);
            var obj = new XipeCoinsService();
            var output = await obj.GetProfile(user.Result.Id);

            if (output.StatusCode == 200)
            {
                var resultAccOutput = (AccountOutput)output.Result;
                return View(resultAccOutput);
            }
            else
            {
                var message = output.Message;
                return View();
            }

        }
        public async Task<IActionResult> convenios()
        {
            var user_id = new PerfilService();
            var user = await user_id.GetPerfil(User.Identity.Name);
            var obj = new XipeCoinsService();
            var output = await obj.GetProfile(user.Result.Id);

            if (output.StatusCode == 200)
            {
                var resultOutput = (AccountOutput)output.Result;
                return View(resultOutput);
            }
            else
            {
                var message = output.Message;
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> transaction(TransferInput dto)
        {
            var obj = new XipeCoinsService();
            var output = await obj.PostTransfer(dto);

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

        }
        [HttpGet]
        public IActionResult transaction()
        {
            return View();
        }
        
    



    }
}
