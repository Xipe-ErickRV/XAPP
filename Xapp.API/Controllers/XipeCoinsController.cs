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

        [HttpGet("GetXipeCoins")]// Checao
        public async Task<IActionResult> GetXipeCoins(int id)
        {

            var balance = await _db.Users 
                .Include(x => x.WalletlUser)
                .FirstOrDefaultAsync(x => x.UserId == id);

            return Ok(balance.WalletlUser.Balance); 
        }

        [HttpPatch("TransferXipeCoins")] //Sin completar¿?
        public async Task<IActionResult> TranferXipeCoins(TransferInput dto)
        {
            var receiver = await _db.Wallets
                .Include(x => x.Transfers)
                .FirstOrDefaultAsync(x => x.UserId == dto.IdReceiver);
            if (receiver == null)
            {
                var output = new ApiResponse 
                {
                    StatusCode = 400,
                    Message = "No se encontró el receptor",
                };
                return BadRequest(output);
            }

            var sender = await _db.Wallets
                .Include(x => x.Transfers)
                .FirstOrDefaultAsync(x => x.UserId == dto.IdSender);
            if (sender == null)
            {
                var output = new ApiResponse 
                {
                    StatusCode = 400,
                    Message = "No se encontró el emisor",
                    //Result = ""
                };
                return BadRequest(output);
            }

            if ((sender.Balance <= 0) || (sender.Balance < dto.Amount))
            {
                var output = new ApiResponse
                {
                    StatusCode = 400,
                    Message = "El emisor no tiene saldo suficinente",
                   // Result = ""
                };
                return BadRequest(output);
            }

            receiver.Sum(dto.Amount);
            sender.Sub(dto.Amount);


            var transferReceiver = new Transfer
            {
                Amount = dto.Amount,
                Concept = dto.AmountConcept,
                Receiver = dto.IdReceiver,
                Sender = dto.IdSender,
                WalletId = receiver.Id,
                Wallet = receiver
            };
            var transferSender = new Transfer
            {
                Amount = dto.Amount,
                Concept = dto.AmountConcept,
                Receiver = dto.IdReceiver,
                Sender = dto.IdSender,
                WalletId = sender.Id,
                Wallet = sender
            };

            transferReceiver.CreateEntity();
            transferSender.CreateEntity();

            receiver.Transfers.Add(transferReceiver); //Aquí peta, marca nulo
            sender.Transfers.Add(transferSender);

            await _db.Transfers.AddAsync(transferReceiver);
            await _db.Transfers.AddAsync(transferSender);
            await _db.SaveChangesAsync();


            var outputOk = new ApiResponse
            {
                StatusCode = 200,
                Message = "Se transfirió correctamente",
            };
            return Ok(outputOk);
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
