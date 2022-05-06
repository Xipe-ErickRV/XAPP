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

    public class AuthController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly JWTMiddlewareService _session;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthController(ILogger<HomeController> logger, JWTMiddlewareService session, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _session = session;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var output = new LoginInput();
            return View(output);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginInput dto)
        {
            var obj = new PerfilService();
            var output = await obj.LogInAsync(dto);

            if (output.StatusCode == 200)
            {
                var user = (User)output.Result;
                _session.AttachAccountToContext(user.Token);
                string page = $"/Feed/index";
                return Redirect(page);
            }
            else
            {
                var message = output.Message;
                return Redirect("/");
            }
        }

        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
