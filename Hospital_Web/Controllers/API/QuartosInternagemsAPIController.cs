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
    public class QuartosInternagemsAPIController : ControllerBase
    {
        private readonly Hospital_WebContext _context;

        public QuartosInternagemsAPIController(Hospital_WebContext context)
        {
            _context = context;
        }

        // GET: api/QuartosInternagemsAPI
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<QuartosInternagem>> GetQuartosInternagem()
        {
            return Unauthorized("Ninguem tem permissão para pedir por todos os registos de uma tabela da base de dados");
        }

        // GET: api/QuartosInternagemsAPI/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<QuartosInternagem>> GetQuartosInternagem(int id)
        {
            var quartosInternagem = await _context.QuartosInternagem.FindAsync(id);

            if (quartosInternagem == null)
            {
                return NotFound();
            }

            return quartosInternagem;
        }

        // PUT: api/QuartosInternagemsAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutQuartosInternagem(int id, QuartosInternagem quartosInternagem)
        {
            if (id != quartosInternagem.ID || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Entry(quartosInternagem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuartosInternagemExists(id))
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

        // POST: api/QuartosInternagemsAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<QuartosInternagem>> PostQuartosInternagem(QuartosInternagem quartosInternagem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.QuartosInternagem.Add(quartosInternagem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuartosInternagem", new { id = quartosInternagem.ID }, quartosInternagem);
        }

        // DELETE: api/QuartosInternagemsAPI/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteQuartosInternagem(int id)
        {
            var quartosInternagem = await _context.QuartosInternagem.FindAsync(id);
            if (quartosInternagem == null)
            {
                return NotFound();
            }

            _context.QuartosInternagem.Remove(quartosInternagem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuartosInternagemExists(int id)
        {
            return _context.QuartosInternagem.Any(e => e.ID == id);
        }
    }
}
