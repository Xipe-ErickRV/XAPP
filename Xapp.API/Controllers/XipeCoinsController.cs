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
        public async Task<IActionResult> GetWallets()
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
                var output = new ApiResponse<string>
                {
                    StatusCode = 400,
                    Message = "No se encontró el receptor",
                    Result = "No se encontró el receptor "
                };
                return BadRequest(output);
            }

            var sender = await _db.Wallets
                .Include(x => x.Transfers)
                .FirstOrDefaultAsync(x => x.UserId == dto.IdSender);
            if (sender == null)
            {
                var output = new ApiResponse<string>
                {
                    StatusCode = 400,
                    Message = "No se encontró el emisor",
                    Result = "No se encontró el emisor"
                };
                return BadRequest(output);
            }

            if ((sender.Balance <= 0) || (sender.Balance < dto.Amount))
            {
                var output = new ApiResponse<string>
                {
                    StatusCode = 400,
                    Message = "El emisor no tiene saldo suficiente",
                    Result = "El emisor no tiene saldo suficiente"
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

            receiver.Transfers.Add(transferReceiver); 
            sender.Transfers.Add(transferSender);

            await _db.Transfers.AddAsync(transferReceiver);
            await _db.Transfers.AddAsync(transferSender);
            await _db.SaveChangesAsync();


            var outputOk = new ApiResponse<string>
            {
                StatusCode = 200,
                Message = "Se transfirió correctamente",
                Result = "Transferencia realizada"
            };
            return Ok(outputOk);
        }


        [HttpGet("GetTransfers")]
        public async Task<IActionResult> GetTransfers(int id)
        {

            var lista = await _db.Transfers
                .FirstOrDefaultAsync(x => x.Id == id);


            var outtransfer = new TransferOutput();

            outtransfer.IdSender = lista.Sender;
            outtransfer.IdReceiver = lista.Receiver;
            outtransfer.Amount = lista.Amount;
            outtransfer.AmountConcept = lista.Concept;
            outtransfer.DateTime = lista.CreationDate;


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
                var output = new ApiResponse<string>
                {
                    StatusCode = 400,
                    Message = "El Usuario no tiene movimientos registrados",
                };
                return BadRequest(output);
            };

            //List<Transfer> ab = lista.WalletlUser.Transfers;

            //for (int i = 0; i < ab.Count; i++)
            //{
            //    Console.WriteLine(ab[i].Receiver);
            //    Console.WriteLine(ab[i].Sender);
            //    Console.WriteLine(ab[i].Concept);
            //    Console.WriteLine(ab[i].Amount);
            //    Console.WriteLine(ab[i].WalletId);
            //}

            return Ok(outtransfer);
        }

        [HttpGet("GetEarnings")]
        public async Task<IActionResult> GetEarnings(int id)
        {
            var earnings = await _db.Wallets
                .Include(x => x.Transfers)
                .FirstOrDefaultAsync(x => x.UserId == id);

            // Falta hacer que sumen las ganancias, que no sea una lista

            if (earnings == null)
            {
                var output = new ApiResponse<string>
                {
                    StatusCode = 400,
                    Message = "El Usuario no tiene Wallet registrada",
                    Result = "El usuario no tiene Wallet  registrada"
                };
                return BadRequest(output);
            }

            var transfers = earnings.Transfers;

            if (transfers == null)
            {
                var output = new ApiResponse<string>
                {
                    StatusCode = 400,
                    Message = "El Usuario no tiene movimientos registrados",
                    Result= "El usuario no tiene movimientos registrados"
                };
                return BadRequest(output);
            }

            List<Transfer> XPearn = new List<Transfer>();

            for (int i = 0; i < transfers.Count; i++)
            {
                if (transfers[i].Receiver == earnings.UserId)
                {
                    XPearn.Add(earnings.Transfers[i]);
                }
            }

            if (XPearn == null)
            {
                var output = new ApiResponse<string>
                {
                    StatusCode = 400,
                    Message = "El Usuario no tiene movimientos registrados donde reciba XipeCoins",
                    Result = "El usuario no ha recibido XipeCoins"
                };
                return BadRequest(output);
            }

            return Ok(XPearn);
        }
        
        [HttpGet("GetProfile")]
        public async Task<IActionResult> GetProfile (int id)
        {
            var balance = await _db.Users
                .Include (x => x.PerfilUser)
                .Include (x => x.WalletlUser)              
                .FirstOrDefaultAsync(x => x.UserId == id);

            if (balance == null)
            {
                var output1 = new ApiResponse <string>
                {
                    StatusCode = 400,
                    Message = "No se encontró al usuario",
                    Result = ""
                };
                return BadRequest(output1);
            }

            var userOutput = new WalletUser();
            userOutput.Balance = balance.WalletlUser.Balance;
            userOutput.Nombre = balance.PerfilUser.Nombre;
            userOutput.Apellido = balance.PerfilUser.Apellido;
            userOutput.UrlProfile = balance.PerfilUser.UrlFoto;

            var outputOk = new ApiResponse<WalletUser>
            {
                StatusCode = 200,
                Message = "OK",
                Result = userOutput
            };

            return Ok(outputOk);
        }

    }
}
