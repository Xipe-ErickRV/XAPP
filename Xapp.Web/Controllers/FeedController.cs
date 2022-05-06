using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Collections.Generic;
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

        [HttpGet]
        public async Task<IActionResult> Post(int id)
        {
            var obj = new FeedService();
            var output = await obj.GetPost(id);

            if (output.StatusCode == 200)
            {
                var resultOutput = (PostOutput)output.Result;
                return View(resultOutput);
            }
            else
            {
                var message = output.Message;
                return View();
            }
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

        [HttpPost]
        public async Task<IActionResult> CreatePost(PostInput dto)
        {
            var user_id = new PerfilService();
            var user = await user_id.GetPerfil(User.Identity.Name);
            var obj = new FeedService();
            var output = await obj.CreatePost(user.Result.Id, dto);

            if (output.StatusCode == 200)
            {
                var resultOutput = output.Result;
                string page = $"/Feed/index";
                return Redirect(page);
            }
            else
            {
                var message = output.Message;
                string page = $"/Feed/index";
                return Redirect(page);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CommentInput dto)
        {
            var user_id = new PerfilService();
            var user = await user_id.GetPerfil(User.Identity.Name);
            var obj = new FeedService();
            var output = await obj.CreateComment(user.Result.Id, dto);

            if (output.StatusCode == 200)
            {
                var resultOutput = output.Result;
                string page = $"/Feed/Post?id={dto.PostId}";
                return Redirect(page);
            }
            else
            {
                var message = output.Message;
                string page = $"/Feed/Post?id={dto.PostId}";
                return Redirect(page);
            }
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
