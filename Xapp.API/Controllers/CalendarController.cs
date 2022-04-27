using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xapp.API.Data;

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Xapp.Domain.DTOs;
using Xapp.Domain.Entities;

namespace Xapp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly DbService _db;

        public CalendarController(DbService db)
        {
            _db = db;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CalendarController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("addEvent")]
        public async Task<IActionResult> AddEvent(EventInput dto)
        {
            var user = await _db.Users
                .Include(x => x.PerfilUser)
                .ThenInclude(x => x.Eventos)
                .FirstOrDefaultAsync(x => x.UserId == dto.UserId);

            if (user == null)
            {
                var outputError = new ApiResponse<string>
                {
                    StatusCode = 400,
                    Message = "Error",
                    Result = "No existe el usuario"
                };
                return BadRequest(outputError);
            }

            if (!ModelState.IsValid)
            {
                var outputError = new ApiResponse<string>
                {
                    StatusCode = 400,
                    Message = "Error",
                    Result = "Llena los campos correctamente"
                };
                return BadRequest(outputError);
            }

            var evento = new Event()
            {
                Title = dto.Title,
                Description = dto.Description,
                DateTime = dto.Date,
                IsPublic = dto.IsPublic,
                IsPublish = true,
                UserId = dto.UserId,
                User = user
            };
            evento.CreateEntity();
            user.PerfilUser.Eventos.Add(evento);
            await _db.Eventos.AddAsync(evento);
            await _db.SaveChangesAsync();

            var output = new ApiResponse<string>
            {
                StatusCode = 200,
                Message = "Éxito",
                Result = "Evento creado"
            };
            return Ok(output);
        }

        // PUT api/<CalendarController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CalendarController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
