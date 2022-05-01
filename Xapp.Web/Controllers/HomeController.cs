using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xapp.Domain.DTOs;
using Xapp.Domain.DTOs.Perfil;
using Xapp.Domain.Entities;
using Xapp.Web.Models;
using Xapp.Web.Services;

namespace Xapp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var output = new LoginInput();
            return View(output);
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginInput dto)
        {
            var obj = new PerfilService();
            var output = await obj.LogInAsync(dto);

            if (output.StatusCode == 200)
            {
                var user = (User)output.Result; 
                string page = $"/Home/ProfileModoVista?email={user.Email}"; 
                return Redirect(page);
            }
            else
            {
                var message = output.Message; 
                return Redirect("/");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Profile(string email)
        {
            var obj = new PerfilService();
            var output = await obj.GetPerfil(email);

            if (output.StatusCode == 200) 
            {
                var perfil = (Perfil)output.Result;
                var outout = perfil.Output();
                return View(outout);
            }
            else 
            {
                var message = output.Message; 
                return View();
            }
        }


        [HttpPost]
        public async Task<IActionResult> Profile(ProfileOutput dto)
        {
            var email = dto.Email;
            var obj = new PerfilService();
            var output = await obj.PatchPerfil(email, dto);

            if (output.StatusCode == 200)
            {
                var perfil = output.Result;
                string page = $"/Home/ProfileModoVista?email={email}";
                return Redirect(page);
            }
            else
            {
                var message = output.Message; 
                return View();
            }
           
        }

        [HttpGet]
        public async Task<IActionResult> ProfileModoVista(string email)
        {
            var obj = new PerfilService();
            var output = await obj.GetPerfil(email);

            if (output.StatusCode == 200)
            {
                var perfil = (Perfil)output.Result;
                var resultOutput = perfil.Output();
                return View(resultOutput);
            }
            else 
            {
                var message = output.Message; 
                return View();
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
