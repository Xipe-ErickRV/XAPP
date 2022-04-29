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

        [HttpPatch("TransferXipeCoins")] //Checao:)
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
                };
                return BadRequest(output);
            }

            if ((sender.Balance <= 0) || (sender.Balance < dto.Amount))
            {
                var output = new ApiResponse
                {
                    StatusCode = 400,
                    Message = "El emisor no tiene saldo suficinente",
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

            var lista = await _db.Users
                .Include(x => x.WalletlUser)
                .ThenInclude(x => x.Transfers)
                .FirstOrDefaultAsync(x => x.UserId == id);

            //var wallet = await _db.Wallets.FirstOrDefaultAsync(m => m.UserId == id);
            //var list1 = await _db.Transfers.Where(m => m.WalletId == wallet.Id).Select(x => new Transfer
            //{
            //    Id = x.Id,
            //    Amount = x.Amount,
            //    Concept = x.Concept,
            //    Receiver = x.Receiver,
            //    Sender = x.Sender
            //}).ToListAsync();

            //var list2 = await _db.Transfers.Include(m => m.Wallet).Where(m => m.Wallet.UserId == id).Select(x => new Transfer
            //{
            //    Id = x.Id,
            //    Amount = x.Amount,
            //    Concept = x.Concept,
            //    Receiver = x.Receiver,
            //    Sender = x.Sender
            //}).ToListAsync();

            if (lista == null) 
            {
                var output = new ApiResponse
                {
                    StatusCode = 400,
                    Message = "El Usuario no tiene movimientos registrados",
                };
                return BadRequest(output);
            };

            List<Transfer> ab = lista.WalletlUser.Transfers;

            return Ok(ab);
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
