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
    public class PTOController : ControllerBase
    {
        private readonly DbService _db;

        public PTOController(DbService db)
        {
            _db = db;
        }
        // GET: PTOController
        //No funciona
        //Cambios en la entidad PTO rompeiron algo en la bd
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var ptos = await _db.PTOs
                .Where(x => x.IsActive).ToListAsync();

            if (ptos == null)
            {
                var outputError = new ApiResponse<string>
                {
                    StatusCode = 400,
                    Message = "Error",
                    Result = "No existe el PTO"
                };
                return BadRequest(outputError);
            }

            var listOfPto = ptos.Select(X => X.Output()).ToList();
            var output = new ApiResponse<List<PTOInput>>
            {
                StatusCode = 200,
                Message = "",
                Result = listOfPto
            };
            return Ok(output);
        }

        [HttpGet("GetPTOByUser")]
        public async Task<IActionResult> Get(int userId)
        {
            var ptos = await _db.PTOs
                .Where(x => x.UserId == userId).ToListAsync();

            if (ptos == null)
            {
                var outputError = new ApiResponse<string>
                {
                    StatusCode = 400,
                    Message = "Error",
                    Result = "No existe el usuario"
                };
                return BadRequest(outputError);
            }

            var listOfPTO = ptos.Select(x => x.Output()).ToList();
            var output = new ApiResponse<List<PTOInput>>
            {
                StatusCode = 200,
                Message = "",
                Result = listOfPTO
            };
            return Ok(output);
        }
        
        [HttpPost("addPTO")]
        public async Task<IActionResult> AddPTO(PTOInput dto)
        {
            var user = await _db.Perfiles
                .Include(x => x.User)
                .ThenInclude(x => x.PTOs)
                .FirstOrDefaultAsync(x => x.Id == dto.UserId);

            if (user == null)
            {
                var outputError = new ApiResponse<string>
                {
                    StatusCode = 400,
                    Message = "Error",
                    Result = "No existe el perfil"
                };
            }

            if (!ModelState.IsValid)
            {
                var outputError = new ApiResponse<string>
                {
                    StatusCode = 400,
                    Message = "Error",
                    Result = "Llena los campos correctamente"
                };
                return BadRequest(outputError);
            }

            var pto = new PTO()
            {
                UserId = dto.UserId,
                IsPTO = dto.IsPTO,
                IsVacation = dto.IsVacation,
                Description = dto.Description,
                StartDate = dto.StartTime,
                EndDate = dto.EndTime,
            };
            pto.CreateEntity();
            user.User.PTOs.Add(pto);
            await _db.PTOs.AddAsync(pto);
            await _db.SaveChangesAsync();

            var output = new ApiResponse<string>
            {
                StatusCode = 200,
                Message = "Éxito",
                Result = "PTO Solicitado"
            };
            return Ok(output);
        }

        // GET: PTOController/Edit/5
        //[HttpGet("Edit")]
        //public ActionResult Edit(int id)
        //{
            //return View();
        //}

        // GET: PTOController/Delete/5
        //[HttpGet("Delete")]
        //public ActionResult Delete(int id)
        //{
            //return View();
        //}
    }
}
