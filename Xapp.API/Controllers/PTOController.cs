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
using Xapp.Domain.DTOs.Calendario.PTO;

namespace Xapp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PTOController : Controller
    {
        private readonly DbService _db;

        public PTOController(DbService db)
        {
            _db = db;
        }
        // GET: PTOController
        [HttpGet("Index")]
        public ActionResult Index()
        {
            return View();
        }

        // GET: PTOController/Details/5
        [HttpGet("Details")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PTOController/Create
        [HttpGet("Create")]
        public ActionResult Create()
        {
            return View();
        }

        //falta probarlo
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
                Description = dto.Description,
                StartDate = dto.StartTime,
                EndDate = dto.EndTime,
                //incompleto
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
        [HttpGet("Edit")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PTOController/Edit/5
        [HttpPost("EditPost")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PTOController/Delete/5
        [HttpGet("Delete")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PTOController/Delete/5
        [HttpPost("DeletePost")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
