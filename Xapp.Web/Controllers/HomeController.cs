using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Authorize]
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly JWTMiddlewareService _session;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(ILogger<HomeController> logger, JWTMiddlewareService session, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _session = session;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            var test = User.Identity;
            var test2 = User.Identity.IsAuthenticated;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var obj = new PerfilService();
            var output = await obj.GetPerfil(User.Identity.Name);

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
            var obj = new PerfilService();
            var output = await obj.PatchPerfil(User.Identity.Name, dto);

            if (output.StatusCode == 200)
            {
                var perfil = output.Result;
                string page = $"/Home/ProfileModoVista";
                return Redirect(page);
            }
            else
            {
                var message = output.Message; 
                return View();
            }
           
        }

        [HttpGet]
        public async Task<IActionResult> ProfileModoVista()
        {
            var obj = new PerfilService();
            var output = await obj.GetPerfil(User.Identity.Name);

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
