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
    public class CommentController : ControllerBase
    {
        private readonly DbService _db;

        public CommentController(DbService db)
        {
            _db = db;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Comment comment)
        {

            comment.CreateEntity();

            await _db.Comments.AddAsync(comment);
            await _db.SaveChangesAsync();

            return Ok();

        }


        [HttpPost("Createdto")]
        public async Task<IActionResult> Createdto(CommentInput dto)
        {
            var comment = new Comment()
            {
                Content = dto.Content,
                PostId = dto.PostId,
                UserId = dto.UserId,
                
                
            };
            comment.CreateEntity();

            await _db.Comments.AddAsync(comment);
            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _db.Comments
                .FirstOrDefaultAsync(x => x.Id == id);
            _db.Comments.Remove(comment);
            await _db.SaveChangesAsync();

            return Ok();
        }
        [HttpGet("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var comment = await _db.Comments
                .FirstOrDefaultAsync(x => x.Id == id);
            return Ok(comment);

        }
        [HttpPatch("Update")]
        public async Task<IActionResult> Update(int id, CommentInput ncomment)
        {
            var comment = await _db.Comments
                .FirstOrDefaultAsync(x => x.Id == id);

            comment.Content = ncomment.Content;

            comment.EditEntity();

            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch("Like")]
        public async Task<IActionResult> Like(int id)
        {
            var comment = await _db.Comments
                .FirstOrDefaultAsync(x => x.Id == id);

            comment.Likes = ++comment.Likes;

            comment.EditEntity();

            await _db.SaveChangesAsync();
            return Ok();
        }

    }
}
