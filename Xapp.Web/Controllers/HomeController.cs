using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
            //consumir API
            var obj = new PerfilService();
            var output = await obj.LogInAsync(dto);

            if (output.StatusCode == 200) //si se pudo
            {
                var user = (User)output.Result; //esto mandarlo al feed , creo
                string page = $"/Home/Profile_html?email={user.Email}";
                return Redirect(page);
            }
            else //no se pudo
            {
                var message = output.Message; //mostrar esta alerta con sweet alert
                return Redirect("/");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Profile_html(string email)
        {
            var obj = new PerfilService();
            var output = await obj.GetPerfil(email);

            if (output.StatusCode == 200) //si se pudo
            {
                var perfil = (Perfil)output.Result; //esto mandarlo al feed , creo
                return View(perfil);
            }
            else //no se pudo
            {
                var message = output.Message; //mostrar esta alerta con sweet alert
                return View();
            }
        }

        public IActionResult Profile()
        {
            return View();
        }

        public async Task<IActionResult> ProfileModoVista(string email)
        {
            var obj = new PerfilService();
            var output = await obj.GetPerfil(email);

            if (output.StatusCode == 200)
            {
                var perfil = (Perfil)output.Result; 
                return View(perfil);
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
