﻿using Microsoft.AspNetCore.Http;
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

        [HttpGet("GetXipeCoins")]// Checao
        public async Task<IActionResult> GetXipeCoins(int id)
        {

            var balance = await _db.Users 
                .Include(x => x.WalletlUser)
                .FirstOrDefaultAsync(x => x.UserId == id);

            return Ok(balance.WalletlUser.Balance); 
        }

        [HttpPatch("TransferXipeCoins")] //Sin completar
        public async Task<IActionResult> TranferXipeCoins(decimal amount, int idReceiver, int idSender, string concepto)
        {
            var receiver = await _db.Transfers
                .FirstOrDefaultAsync(x => x.Receiver == idReceiver);

            var sender = await _db.Transfers
                .FirstOrDefaultAsync(x => x.Sender == idSender);

            var amountTransfer = await _db.Transfers
                .FirstOrDefaultAsync(x => x.Amount == amount);

            var conceptoTransfer = await _db.Transfers
                .FirstOrDefaultAsync(x => x.Concept == concepto);

            if (receiver == null || sender == null) return BadRequest();
            if (receiver == sender) return BadRequest();
            //revisar caso en caso que las monedas no sean suficientes
            else if (amountTransfer == null) return BadRequest();
            else
            {
                receiver.Amount = receiver.Amount + amount;
                sender.Amount = sender.Amount - amount;
                //Transfer.Concept = conceptoTransfer; 
            }

            await _db.SaveChangesAsync();
            return Ok(sender.Amount);
        }

        [HttpGet("GetTransfers")]
        public async Task<IActionResult> GetTransfers(int id)
        {

            var list = await _db.Transfers
                .Include(x => x.Sender)
                .Include(x => x.Receiver)
                .Include(x => x.Amount)
                .Include(x => x.Concept)
                .FirstOrDefaultAsync(x => x.Sender == id || x.Receiver == id);

            return Ok(list);
        }

        [HttpGet("GetEarnings")]
        public async Task<IActionResult> GetEarnings(int id)
        {
            var earnings = await _db.Transfers
                .Include(x => x.Amount)
                .FirstOrDefaultAsync(x => x.Receiver == id);

            // Falta hacer que sumen las ganancias, que no sea una lista

            return Ok(earnings);
        }

    }
}