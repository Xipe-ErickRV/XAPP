using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Xapp.Domain.DTOs;
using Xapp.Web.Services;

namespace Xapp.Web.Controllers
{
    public class FeedController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly JWTMiddlewareService _session;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public FeedController(ILogger<HomeController> logger, JWTMiddlewareService session, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _session = session;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var obj = new FeedService();
            var output = await obj.GetAllPosts();

            if (output.StatusCode == 200)
            {
                var post = (PostOutput)output.Result;
                return View(post);
            }
            else
            {
                var message = output.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPost(int id)
        {
            var obj = new FeedService();
            var output = await obj.GetPost(id);

            if (output.StatusCode == 200)
            {
                var post = (PostOutput)output.Result;
                return View(post);
            }
            else
            {
                var message = output.Message;
                return View();
            }
        }
    }
}
