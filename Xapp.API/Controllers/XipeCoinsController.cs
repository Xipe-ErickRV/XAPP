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
