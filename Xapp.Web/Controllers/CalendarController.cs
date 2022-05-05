using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Threading.Tasks;
using Xapp.Domain.DTOs;
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

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetEvent()
        {
            var obj = new CalendarService();
            var output = await obj.GetEvents();

            if (output.StatusCode == 200)
            {
                var resultOutput = (EventList)output.Result;
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
