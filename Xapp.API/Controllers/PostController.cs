using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xapp.API.Data;
using Xapp.Domain.DTOs;
using Xapp.Domain.Entities;

namespace Xapp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly DbService _db;

        public PostController(DbService db)
        {
            _db = db;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(int id,PostInput dto)
        {
            var post = new Post()
            {
                Title = dto.Title,
                Content = dto.Content,
                Multimedia = dto.Multimedia,
                Tag = dto.Tag,
                Likes = 0,
                UserId = id,
                Comments = new List<Comment>()

                
            };


            var User = await _db.Users.FirstOrDefaultAsync(x => x.UserId == id);

            post.User = User;

            post.CreateEntity();

            await _db.Posts.AddAsync(post);
            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _db.Posts
                .FirstOrDefaultAsync(x => x.Id == id);

            if (post == null)
            {
                var outputError = new ApiResponse<string>
                {
                    StatusCode = 400,
                    Message = "Error",
                    Result = "No existe el Post"
                };
                return BadRequest(outputError);
            }

            post.Delete();
            await _db.SaveChangesAsync();

            return Ok();
        }
        [HttpGet("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var post = await _db.Posts
                .Include(x => x.Comments)
                .ThenInclude(x => x.User)
                .ThenInclude(x => x.PerfilUser)
                .Include(x => x.User)
                .ThenInclude(x => x.PerfilUser)
                .FirstOrDefaultAsync(x => x.Id == id);

            var outpost = new PostOutput();
            
            outpost.Title = post.Title;
            outpost.Content = post.Content;
            outpost.Multimedia = post.Multimedia;
            outpost.Tag = post.Tag;
            outpost.UserId = post.UserId;
            outpost.URLProfile = post.User.PerfilUser.UrlFoto;
            outpost.Likes = post.Likes;
            outpost.UserName = post.User.Username;
            outpost.Comments = new List<CommentOutput>();

            foreach (var comment in post.Comments)
            {
                var outcomment = new CommentOutput();

                outcomment.UserName = comment.User.Username;
                outcomment.Content = comment.Content;
                outcomment.UserId = comment.UserId;
                outcomment.PostId = comment.PostId;
                outpost.Likes = comment.Likes;
                outpost.URLProfile = comment.User.PerfilUser.UrlFoto;

                outpost.Comments.Add(outcomment);
            }

            if (outpost == null)
            {
                var outputError = new ApiResponse<string>
                {
                    StatusCode = 400,
                    Message = "Error",
                    Result = "No existe el Post"
                };
                return BadRequest(outputError);

            }

            var output = new ApiResponse<PostOutput>
            {
                StatusCode = 200,
                Message = "Ok",
                Result = outpost
            };
            return Ok(output);

        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _db.Posts
                .Include(x => x.Comments)
                .ThenInclude(x => x.User)
                .ThenInclude(x => x.PerfilUser)
                .Include(x => x.User)
                .ThenInclude(x => x.PerfilUser)
                .ToListAsync();

            var plist = new PostList();
            var list= new List<PostOutput>();
            foreach (var post in posts)
            {
                var outpost = new PostOutput();
                outpost.Title = post.Title;
                outpost.Content = post.Content;
                outpost.Multimedia = post.Multimedia;
                outpost.Tag = post.Tag;
                outpost.UserId = post.UserId;
                outpost.URLProfile = post.User.PerfilUser.UrlFoto;
                outpost.Likes = post.Likes;
                outpost.UserName = post.User.Username;
                outpost.Comments = new List<CommentOutput>();

                foreach (var comment in post.Comments)
                {
                    var outcomment = new CommentOutput();

                    outcomment.Content = comment.Content;
                    outcomment.UserName = comment.User.Username;
                    outcomment.UserId = comment.UserId;
                    outcomment.PostId = comment.PostId;
                    outpost.Likes = comment.Likes;
                    outpost.URLProfile = comment.User.PerfilUser.UrlFoto;

                    outpost.Comments.Add(outcomment);
                }

                list.Add(outpost);
            }
            plist.Posts = list;

            var output = new ApiResponse<PostList>
            {
                StatusCode = 200,
                Message = "OK",
                Result = plist
            };

            return Ok(output);
        }

        [HttpPatch("Update")]
        public async Task<IActionResult> Update(int id, PostInput npost)
        {
            var post = await _db.Posts
                .FirstOrDefaultAsync(x => x.Id == id);

            if(post == null)
            {
                var outputError = new ApiResponse<string>
                {
                    StatusCode = 400,
                    Message = "Error",
                    Result = "No existe el Post"
                };
                return BadRequest(outputError);
            }

            post.Title = npost.Title;
            post.Content = npost.Content;
            post.Multimedia = npost.Multimedia;

            post.EditEntity();

            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch("Like")]
        public async Task<IActionResult> Like(int id)
        {
            var post = await _db.Posts
                .FirstOrDefaultAsync(x => x.Id == id);

            post.Likes = ++post.Likes;

            post.EditEntity();

            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
