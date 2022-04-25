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
        
        [HttpGet("getUser")]
        public async Task<IActionResult> GetUser(string email)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(x => x.Email == email);
            if (user == null) return BadRequest();

            return Ok(user);
        }
        [HttpGet("getPerfil")]
        public async Task<IActionResult> GetPerfil(string email)
        {
            var user = await _db.Users
                .Include(x => x.PerfilUser)
                .FirstOrDefaultAsync(x => x.Email == email);

            var perfil = user.PerfilUser;
            if (perfil != null) return BadRequest();
            
            return Ok(perfil);
        }
        [HttpGet("getSkills")]
        public async Task<IActionResult> GetSkills(string email)
        {
            var user = await _db.Users
                .Include(x => x.PerfilUser)
                .ThenInclude(x => x.Skills)
                .FirstOrDefaultAsync(m => m.Email == email);

            if (user == null) return BadRequest();
            
            return Ok(user.PerfilUser.Skills);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _db.Users
                .Include(m => m.PerfilUser)
                .FirstOrDefaultAsync(m => m.Email == email && m.Password == password);
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
        public async Task<IActionResult> AddUser(UserInput dto)
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
        [HttpPost("newSkill")]
        public async Task<IActionResult> NewSkill(string email, SkillInput dto)
        {
            var user = await _db.Users
                .Include(x => x.PerfilUser)
                .ThenInclude(x => x.Skills)
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user == null) return BadRequest();

            var skill = new Skill()
            {
                User = user.UserId,
                Nombre = dto.Nombre,
                Nivel = dto.Nivel,
                Descripcion = dto.Descripcion
            };
            skill.CreateEntity();
            user.PerfilUser.Skills.Add(skill);

            await _db.Skills.AddAsync(skill);
            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpPatch("passwordChange")]
        public async Task<IActionResult> PasswordChange(string email, string old, string newP, string confirmP)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user == null) return NotFound();

            if (user.Password != old) return BadRequest();

            if (confirmP != newP) return BadRequest();

            user.Password = newP;
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpPatch("patchUsuario")]
        public async Task<IActionResult> PatchUsuario(string email, PatchUser dto)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user == null) return BadRequest();

            user.Username = dto.Username;
            //user.Password = dto.Password;
            user.Email = dto.Email;

            await _db.SaveChangesAsync();
            return Ok();
        }
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
        
        //INCOMPLETE?
        [HttpDelete("userDelete")]
        public async Task<IActionResult> UserDelete(string email)
        {
            var user = await _db.Users
                .Include(x => x.PerfilUser)
                .Include(x=>x.WalletlUser)
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user == null) return BadRequest();

            user.Delete();
            user.PerfilUser.Delete();
            user.WalletlUser.Delete();
            //Execute every Delete() in child entities like PTO?

            await _db.SaveChangesAsync();
            return Ok(user);
        }
        [HttpDelete("skillDelete")]
        public async Task<IActionResult> SkillDelete(int ID, string email)
        {
            var user = await _db.Users
                .Include(x=> x.PerfilUser)
                .ThenInclude(x=> x.Skills)
                .FirstOrDefaultAsync(x => x.Email == email);

            //validación bla bla
            // validación ...

            var skill = user.PerfilUser.Skills
                .Find(x => x.User == user.UserId && x.Id == ID);
            if (skill == null) return BadRequest();

            skill.Delete();
            await _db.SaveChangesAsync();
            return Ok(skill);
        }
    }
}
