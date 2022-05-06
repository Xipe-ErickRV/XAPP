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

        [HttpGet("GetEvents")]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _db.Eventos
                .Where(x => x.IsPublic && x.IsActive).ToListAsync();

            var elist = new EventList();
            var list = new List<EventInput>();
            foreach (var e in events)
            {
                var outpost = new EventInput();
                outpost.UserId = e.UserId;
                outpost.EventId = e.Id;
                outpost.Title = e.Title;
                outpost.Description = e.Description;
                outpost.IsPublic = e.IsPublic;
                outpost.Date = e.DateTime;

                list.Insert(0 , outpost);
            }

            elist.Events = list;

            if (events == null)
            {
                var outputError = new ApiResponse<string>
                {
                    StatusCode = 400,
                    Message = "Error",
                    Result = "No existe el evento"
                };
                return BadRequest(outputError);
            }

            //var listOfEvent = events.Select(X => X.Output()).ToList();
            var output = new ApiResponse<EventList>
            {
                StatusCode = 200,
                Message = "OK",
                Result = elist
            };
            return Ok(output);
        }

        [HttpGet("GetEventsByUser")]
        public async Task<IActionResult> Get(int userId)
        {
            /*var events = await _db.Eventos
                .Where(x => x.UserId == userId).ToListAsync();*/
            var events = await _db.Eventos
                .FirstOrDefaultAsync(x => x.UserId == userId);

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

            var outevent = new EventInput();
            outevent.Title = events.Title;
            outevent.Description = events.Description;
            outevent.Date = events.DateTime;
            outevent.IsPublic = events.IsPublic;


            //var listOfEvent = events.Select(X => X.Output()).ToList();
            /*var output = new ApiResponse<List<EventInput>>
            var output = new ApiResponse<EventInput>
            {
                StatusCode = 200,
                Message = "OK",
                Result = listOfEvent
            };*/
            return Ok(outevent);
        }

        [HttpPost("AddEvent")]
        public async Task<IActionResult> AddEvent(int userId, EventInput dto)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(x => x.UserId == userId);

            /*if (user == null)
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
            }*/

            var evento = new Event()
            {
                Title = dto.Title,
                Description = dto.Description,
                DateTime = dto.Date,
                IsPublic = dto.IsPublic,
                IsPublish = true
            };
            evento.User = user;
            evento.CreateEntity();
            //user.PerfilUser.Eventos.Add(evento);
            await _db.Eventos.AddAsync(evento);
            await _db.SaveChangesAsync();

            var output = new ApiResponse<Event>
            {
                StatusCode = 200,
                Message = "Éxito",
                Result = evento
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
