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
        public async Task<IActionResult> Create(CommentInput dto)
        {
            var comment = new Comment()
            {
                Content = dto.Content,
                PostId = dto.PostId,
                UserId = dto.UserId,
                
                
            };
            comment.CreateEntity();

            var post = await _db.Posts.FirstOrDefaultAsync(x => x.Id == comment.Id);
            if (post == null)
            {
                var output = new ApiResponse<string>
                {
                    StatusCode = 400,
                    Message = "Error",
                    Result = "No existe el post"
                };
                return BadRequest(output);
            }

            post.Comments.Add(comment);

            await _db.Comments.AddAsync(comment);
            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _db.Comments
                .FirstOrDefaultAsync(x => x.Id == id);

            if(comment == null)
            {
                var output = new ApiResponse<string>
                {
                    StatusCode = 400,
                    Message = "Error",
                    Result = "No existe el comentario"
                };
                return BadRequest(output);
            }
            _db.Comments.Remove(comment);
            await _db.SaveChangesAsync();

            return Ok();
        }
        [HttpGet("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var comment = await _db.Comments
                .FirstOrDefaultAsync(x => x.Id == id);

            if(comment == null)
            {
                var output = new ApiResponse<string>
                {
                    StatusCode = 400,
                    Message = "Error",
                    Result = "No existe el comentario"
                };
                return BadRequest(output);
            }
            return Ok(comment);

        }
        [HttpPatch("Update")]
        public async Task<IActionResult> Update(int id, CommentInput ncomment)
        {
            var comment = await _db.Comments
                .FirstOrDefaultAsync(x => x.Id == id);

            if (comment == null)
            {
                var output = new ApiResponse<string>
                {
                    StatusCode = 400,
                    Message = "Error",
                    Result = "No existe el comentario"
                };
                return BadRequest(output);
            }

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
