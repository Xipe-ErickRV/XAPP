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
    public class PerfilController : ControllerBase
    {
        private readonly DbService _db;

        public PerfilController(DbService db)
        {
            _db = db;
        }
        

        [HttpPost("login")]

        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _db.Users.FirstOrDefaultAsync(m => m.Email == email && m.Password == password);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                
                return Ok();
            } 
        }

        [HttpGet("getPerfil")]
        public async Task<IActionResult> GetPerfil(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(m => m.Email == email);
            var perfil = await _db.Perfiles.FirstOrDefaultAsync(m => m.Id == user.UserId);
            if (perfil != null)
            {
                return Ok(perfil);
            }
            else
            {

                return Ok();
            }
        }

        [HttpPatch("patchPerfil")]
        public async Task<IActionResult> PatchPerfil(string email, ProfileUpdate dto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(m => m.Email == email);
            var perfil = await _db.Perfiles.FirstOrDefaultAsync(m => m.Id == user.UserId);
            if (perfil != null)
            {
                return Ok(); //this is NOT ok()
            }
            else
            {
                return Ok();
            }
        }
    }
}
