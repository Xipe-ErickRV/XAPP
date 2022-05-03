using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using Xapp.Domain.DTOs;
using Xapp.Domain.DTOs.Perfil;
using Xapp.Web.Models;
using Xapp.Web.Services;

namespace Xapp.Web.Controllers
{
    public class FeedController : Controller
    {
        private readonly ILogger<FeedController> _logger;
        private readonly JWTMiddlewareService _session;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FeedController(ILogger<FeedController> logger, JWTMiddlewareService session, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _session = session;
            _httpContextAccessor = httpContextAccessor;
        }


        public IActionResult Post()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var obj = new FeedService();
            var output = await obj.GetAllPosts();

            if (output.StatusCode == 200)
            {
                var resultOutput = (PostList)output.Result;
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
