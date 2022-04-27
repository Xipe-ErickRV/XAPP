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
        public async Task<IActionResult> addEvent(EventInput dto)
        {
            var evento = new Event();
            {

            }
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
