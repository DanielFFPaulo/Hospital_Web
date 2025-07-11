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
    public class InternamentosAPIController : ControllerBase
    {
        private readonly Hospital_WebContext _context;

        public InternamentosAPIController(Hospital_WebContext context)
        {
            _context = context;
        }

        // GET: api/InternamentosAPI
        [HttpGet]
        [Authorize(Roles = "Admin, Medico")]
        public ActionResult<IEnumerable<Internamento>> GetInternamento()
        {
            return Unauthorized("Ninguem tem permissão para pedir por todos os registos de uma tabela da base de dados");
        }

        // GET: api/InternamentosAPI/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Medico")]
        public async Task<ActionResult<Internamento>> GetInternamento(int id)
        {
            var internamento = await _context.Internamento.FindAsync(id);

            if (internamento == null)
            {
                return NotFound();
            }

            return internamento;
        }

        // PUT: api/InternamentosAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Medico")]
        public async Task<IActionResult> PutInternamento(int id, Internamento internamento)
        {
            if (id != internamento.ID || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Entry(internamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InternamentoExists(id))
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

        // POST: api/InternamentosAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin, Medico")]
        public async Task<ActionResult<Internamento>> PostInternamento(Internamento internamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Internamento.Add(internamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInternamento", new { id = internamento.ID }, internamento);
        }

        // DELETE: api/InternamentosAPI/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteInternamento(int id)
        {

            var internamento = await _context.Internamento.FindAsync(id);
            if (internamento == null)
            {
                return NotFound();
            }

            _context.Internamento.Remove(internamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InternamentoExists(int id)
        {
            return _context.Internamento.Any(e => e.ID == id);
        }
    }
}
