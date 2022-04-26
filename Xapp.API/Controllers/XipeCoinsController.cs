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

        [HttpPatch("TransferXipeCoins")]
        public async Task<IActionResult> TranferXipeCoins(decimal amount, int idReceiver, int idSender)
        {
            var receiver = await _db.Transfers
                .FirstOrDefaultAsync(x => x.Receiver == idReceiver);

            var sender = await _db.Transfers
                .FirstOrDefaultAsync(x => x.Sender == idSender);

            var amountTransfer = await _db.Transfers.FirstOrDefaultAsync(x => x.Amount == amount);

            if (receiver == null) return BadRequest();
            else if (sender == null) return BadRequest();
            else if (amountTransfer == null) return BadRequest();
            else
            {
               // receiver.Amount = receiver.Amount + amountTransfer;
               // sender.Amount = sender.Amount - amountTransfer;
            }

            await _db.SaveChangesAsync();

            return Ok();
        }


}
}
