using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Threading.Tasks;
using Xapp.Domain.DTOs;
using Xapp.Domain.Entities;
using Xapp.Web.Models;
using Xapp.Web.Services;

namespace Xapp.Web.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        private readonly ILogger<CalendarController> _logger;
        private readonly JWTMiddlewareService _session;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CalendarController(ILogger<CalendarController> logger, JWTMiddlewareService session, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _session = session;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var obj = new CalendarService();
            var output = await obj.GetEvents();

            if (output.StatusCode == 200)
            {
                var eventos = (EventList)output.Result;
                return View(eventos);
            }
            else
            {
                var message = output.Message;
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventInput dto)
        {
            var userId = new PerfilService();
            var user = await userId.GetPerfil(User.Identity.Name);
            var obj = new CalendarService();
            var output = await obj.AddEvent(user.Result.Id, dto);

            if (output.StatusCode == 200)
            {
                var resultOutput = output.Result;
                string page = $"Calendar/Index";
                return Redirect(page);
            }
            else
            {
                var messege = output.Message;
                string page = $"Calendar/Index";
                return Redirect(page);
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
