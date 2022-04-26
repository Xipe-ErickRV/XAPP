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

namespace Xapp.API.XipeCoinsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class XipeCoinsController : ControllerBase
    {
        private readonly DbService _db;

        public XipeCoinsController(DbService db)
        {
            _db = db;
        }

        [HttpPost("post")]
        public async Task<IActionResult> Post(User user)
        {
            user.CreateEntity();

            await _db.Users.AddAsync(user);
            await _db.Wallets.AddAsync(user.WalletlUser);
            await _db.Perfiles.AddAsync(user.PerfilUser);
            await _db.SaveChangesAsync();



            return Ok();
        }


        [HttpPost("PostConDTO")]
        public async Task<IActionResult> PostConDTO(UserInput dto)
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
        [HttpPost("perfil")]
        public async Task<IActionResult> Perfil(int userId, Wallet wallet)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            user.WalletlUser = wallet;

            await _db.Wallets.AddAsync(wallet);
            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("GetTest")]
        public async Task<IActionResult> GetTest()
        {
            var test = await _db.Users.ToListAsync();
            return Ok(test);
        }


        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _db.Users
                .Include(x => x.PerfilUser)
                .Include(x => x.WalletlUser)
                .FirstOrDefaultAsync(x => x.UserId == id);
            return Ok(user);
        }


        [HttpDelete("ById")]
        public async Task<IActionResult> ById(int id)
        {
            var user = await _db.Users
                .Include(x => x.PerfilUser)
                .Include(x => x.WalletlUser)
                .FirstOrDefaultAsync(x => x.UserId == id);

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("GetWallets")]
        public async Task<IActionResult> GetWallets( )
        {
            var list = await _db.Wallets.ToListAsync();
            return Ok(list);
        }

        [HttpGet("GetXipeCoins")]
        public async Task<IActionResult> GetXipeCoins(int id)
        {

            var balance = await _db.Wallets
                .Include(x => x.UserId)
                .Include(x => x.Balance)
                .FirstOrDefaultAsync(x => x.UserId == id);

            return Ok(balance);
        }


    }
}
