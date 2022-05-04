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

        [HttpPatch("EditPTO")]
        public async Task<IActionResult> EditPTO(EditPTO dto, int userId, int ptoId)
        {
            var user = await _db.Perfiles
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
                return BadRequest();

            var pto = await _db.PTOs
                .FirstOrDefaultAsync(x => x.Id == ptoId);
            if (pto == null)
                return BadRequest();

            pto.IsPTO = dto.IsPTO;
            pto.IsVacation = dto.IsVacation;
            pto.Description = dto.Description;
            pto.StartDate = dto.StartDate;
            pto.EndDate = dto.EndDate;

            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("DeletePTO")]
        public async Task<IActionResult> DeletePTO(int userId, int ptoId)
        {
            var user = await _db.Perfiles
                .Include(x => x.User)
                .ThenInclude(x => x.PTOs)
                .FirstOrDefaultAsync(x => x.Id == userId);

            var ptos = user.User.PTOs
                .Find(x => x.UserId == user.Id && x.Id == ptoId);
            if (ptos == null)
                return BadRequest();

            ptos.Delete();
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
