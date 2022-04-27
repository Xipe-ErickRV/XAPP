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
        public async Task<IActionResult> Create(Post post)
        {

            post.CreateEntity();

            await _db.Posts.AddAsync(post);
            await _db.SaveChangesAsync();

            return Ok();

        }


        [HttpPost("Createdto")]
        public async Task<IActionResult> Createdto(PostInput dto)
        {
            var post = new Post()
            {
                Title = dto.Title,
                Content = dto.Content,
                Multimedia = dto.Multimedia,
                Tag = dto.Tag,
                UserId = dto.UserId,
                
            };
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
            _db.Posts.Remove(post);
            await _db.SaveChangesAsync();

            return Ok();
        }
        [HttpGet("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var post = await _db.Posts
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
            outpost.Comments = post.Comments;

            if(outpost == null)
            {
                var outputError = new ApiResponse<string>
                {
                    StatusCode = 400,
                    Message = "Error",
                    Result = "No existe el Post"
                };
                return BadRequest(outputError);

            }
            return Ok(outpost);

        }
        [HttpPatch("Update")]
        public async Task<IActionResult> Update(int id, PostInput npost)
        {
            var post = await _db.Posts
                .FirstOrDefaultAsync(x => x.Id == id);

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
