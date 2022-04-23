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
                Likes = dto.Likes,
                
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
                .FirstOrDefaultAsync(x => x.Id == id);
            return Ok(post);

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

    }
}
