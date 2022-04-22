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

        [HttpGet("getPerfil")]
        public async Task<IActionResult> GetPerfil(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(m => m.Email == email);
            var perfil = user.PerfilUser;
            if (perfil != null)
            {
                return Ok(perfil);
            }
            else
            {

                return Ok();
            }
        }

        //TEST PENDING
        [HttpGet("getSkills")]
        public async Task<IActionResult> GetSkills(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(m => m.Email == email);
            var skills = user.PerfilUser.Skills; 
            if (user != null)
            {
                return Ok(skills);
            }
            else
            {

                return Ok();
            }
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _db.Users.Include(m => m.PerfilUser).FirstOrDefaultAsync(m => m.Email == email && m.Password == password);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest();
            } 
        }

        [HttpPost("addUser")]
        public async Task<IActionResult> addUser(UserInput dto)
        {
            var user = new User()
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password,
                PerfilUser = new Perfil
                {
                    Nombre = dto.Nombre,
                    Apellido = dto.Apellido,
                    Area = dto.Area,
                    Bio = dto.Bio,
                    FechaCumple = dto.FechaCumple
                },
                WalletlUser = new Wallet(),
                Comments = new List<Comment>(),
                Posts = new List<Post>(),
                PTOs = new List<PTO>(),
                Roles = new List<Rol>()
            };
            user.CreateEntity();
            user.PerfilUser.CreateEntity();
            user.WalletlUser.CreateEntity();

            await _db.Users.AddAsync(user);
            await _db.Perfiles.AddAsync(user.PerfilUser);
            await _db.Wallets.AddAsync(user.WalletlUser);
            await _db.SaveChangesAsync();

            return Ok();
        }

        //SERVICES??
        [HttpPatch("patchPerfil")]
        public async Task<IActionResult> PatchPerfil(string email, ProfileUpdate dto)
        {
            var user = await _db.Users
                .Include(x=> x.PerfilUser)
                .FirstOrDefaultAsync(m => m.Email == email);
            if (user != null)
            {
                user.PerfilUser.MetodoEdit(dto);
                await _db.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        //TEST PENDING
        [HttpDelete("skillDelete")]
        public async Task<IActionResult> SkillDelete(int ID, string email)
        {
            var user = await _db.Users
                .Include(x=> x.PerfilUser)
                .ThenInclude(x=> x.Skills)
                .FirstOrDefaultAsync(x => x.Email == email);

            //validación bla bla
            // validación ...


            var skill = user.PerfilUser.Skills.Find(m => m.Id == ID);
            skill.Delete();
            await _db.SaveChangesAsync();
            return Ok(skill);
        }
    }
}
