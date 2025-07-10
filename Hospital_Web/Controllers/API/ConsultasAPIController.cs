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
    public class ConsultasAPIController : ControllerBase
    {
        private readonly Hospital_WebContext _context;

        public ConsultasAPIController(Hospital_WebContext context)
        {
            _context = context;
        }

        // GET: api/ConsultasAPI
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Consulta>>> GetConsulta()
        {
            return await _context.Consulta.ToListAsync();
        }

        // GET: api/ConsultasAPI/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Consulta>> GetConsulta(int id)
        {
            var consulta = await _context.Consulta.FindAsync(id);

            if (consulta == null)
            {
                return NotFound();
            }

            return consulta;
        }

        // PUT: api/ConsultasAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutConsulta(int id, Consulta consulta)
        {
            if (id != consulta.Episodio)
            {
                return BadRequest();
            }

            _context.Entry(consulta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultaExists(id))
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

        // POST: api/ConsultasAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Consulta>> PostConsulta(Consulta consulta)
        {
            _context.Consulta.Add(consulta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConsulta", new { id = consulta.Episodio }, consulta);
        }

        // DELETE: api/ConsultasAPI/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConsulta(int id)
        {
            var consulta = await _context.Consulta.FindAsync(id);
            if (consulta == null)
            {
                return NotFound();
            }

            _context.Consulta.Remove(consulta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConsultaExists(int id)
        {
            return _context.Consulta.Any(e => e.Episodio == id);
        }
    }
}
