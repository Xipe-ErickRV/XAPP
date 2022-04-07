using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xapp.API.Data;

namespace Xapp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        private readonly DbService _db;

        public ExampleController(DbService db)
        {
            _db = db;
        }

        //[HttpGet("Test")]
        //public async Task<IActionResult> ExtractExel()
        //{
        //    var test = await _db.Countries.Include(m => m.Categories).Where(m => m.Name == "Mexico").ToListAsync();
        //    return Ok();
        //}
    }
}
