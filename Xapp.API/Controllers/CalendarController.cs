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
        public async Task<IActionResult> GetAsync()
        {
            var events = await _db.Eventos
                .Where(x => x.IsPublic && x.IsActive).ToListAsync();

            if (events == null)
            {
                var outputError = new ApiResponse<string>
                {
                    StatusCode = 400,
                    Message = "Error",
                    Result = "No existe el usuario"
                };
                return BadRequest(outputError);
            }

            var listOfEvent = events.Select(X => X.Output()).ToList();
            var output = new ApiResponse<List<EventInput>>
            {
                StatusCode = 200,
                Message = "",
                Result = listOfEvent
            };
            return Ok(output);
        }

        [HttpGet("GetEventsByUser")]
        public async Task<IActionResult> Get(int userId)
        {
            var events = await _db.Eventos
                .Where(x => x.UserId == userId).ToListAsync();

            if (events == null)
            {
                var outputError = new ApiResponse<string>
                {
                    StatusCode = 400,
                    Message = "Error",
                    Result = "No existe el usuario"
                };
                return BadRequest(outputError);
            }

            var listOfEvent = events.Select(X => X.Output()).ToList();
            var output = new ApiResponse<List<EventInput>>
            {
                StatusCode = 200,
                Message = "",
                Result = listOfEvent
            };
            return Ok(output);
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

        [HttpPatch("EditEvent")]
        public async Task<IActionResult> EditEvent(EditEvent dto, int userId, int eventId)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(x => x.UserId == userId);
            if (user == null)
                return BadRequest();

            var evento = await _db.Eventos
               .FirstOrDefaultAsync(x => x.Id == eventId);
            if (evento == null)
                return BadRequest();

            evento.Title = dto.Title;
            evento.Description = dto.Description;
            evento.DateTime = dto.DateTime;

            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("DeleteEvent")]
        public async Task<IActionResult> DeleteEvent(int userId, int eventId)
        {
            var user = await _db.Users
                .Include(x => x.PerfilUser)
                .ThenInclude(x => x.Eventos)
                .FirstOrDefaultAsync(x => x.UserId == userId);

            var events = user.PerfilUser.Eventos
                .Find(x => x.UserId == user.UserId && x.Id == eventId);
            if (events == null)
                return BadRequest();

            events.Delete();
            await _db.SaveChangesAsync();
            return Ok(events);
        }

    }
}
