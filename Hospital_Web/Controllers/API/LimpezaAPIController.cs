using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital_Web.Data;
using Hospital_Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace Hospital_Web.Controllers.API
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class LimpezaAPIController : ControllerBase
    {
        private readonly Hospital_WebContext _context;

        public LimpezaAPIController(Hospital_WebContext context)
        {
            _context = context;
        }

        // GET: api/LimpezaAPI
        [HttpGet]
        [Authorize(Roles = "Admin, FuncionarioLimpeza")]
        public ActionResult<IEnumerable<LimpezaSala>> GetLimpezaSala()
        {
            return Unauthorized("Ninguem tem permissão para pedir por todos os registos de uma tabela da base de dados");
        }

        // GET: api/LimpezaAPI/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, FuncionarioLimpeza")]
        public async Task<ActionResult<LimpezaSala>> GetLimpezaSala(int id)
        {
            var limpezaSala = await _context.LimpezaSala.FindAsync(id);

            if (limpezaSala == null)
            {
                return NotFound();
            }

            return limpezaSala;
        }

        // PUT: api/LimpezaAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, FuncionarioLimpeza")]
        public async Task<IActionResult> PutLimpezaSala(int id, LimpezaSala limpezaSala)
        {
            if (id != limpezaSala.ID || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Entry(limpezaSala).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LimpezaSalaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LimpezaAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin, FuncionarioLimpeza")]
        public async Task<ActionResult<LimpezaSala>> PostLimpezaSala(LimpezaSala limpezaSala)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.LimpezaSala.Add(limpezaSala);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLimpezaSala", new { id = limpezaSala.ID }, limpezaSala);
        }

        // DELETE: api/LimpezaAPI/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, FuncionarioLimpeza")]
        public async Task<IActionResult> DeleteLimpezaSala(int id)
        {
            var limpezaSala = await _context.LimpezaSala.FindAsync(id);
            if (limpezaSala == null)
            {
                return NotFound();
            }

            _context.LimpezaSala.Remove(limpezaSala);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LimpezaSalaExists(int id)
        {
            return _context.LimpezaSala.Any(e => e.ID == id);
        }
    }
}
