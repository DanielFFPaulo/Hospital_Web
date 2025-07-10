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
    public class GabinetesAPIController : ControllerBase
    {
        private readonly Hospital_WebContext _context;

        public GabinetesAPIController(Hospital_WebContext context)
        {
            _context = context;
        }

        // GET: api/GabinetesAPI
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Gabinete>>> GetGabinete()
        {
            return await _context.Gabinete.ToListAsync();
        }

        // GET: api/GabinetesAPI/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Gabinete>> GetGabinete(int id)
        {
            var gabinete = await _context.Gabinete.FindAsync(id);

            if (gabinete == null)
            {
                return NotFound();
            }

            return gabinete;
        }

        // PUT: api/GabinetesAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutGabinete(int id, Gabinete gabinete)
        {
            if (id != gabinete.ID)
            {
                return BadRequest();
            }

            _context.Entry(gabinete).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GabineteExists(id))
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

        // POST: api/GabinetesAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Gabinete>> PostGabinete(Gabinete gabinete)
        {
            _context.Gabinete.Add(gabinete);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGabinete", new { id = gabinete.ID }, gabinete);
        }

        // DELETE: api/GabinetesAPI/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGabinete(int id)
        {
            var gabinete = await _context.Gabinete.FindAsync(id);
            if (gabinete == null)
            {
                return NotFound();
            }

            _context.Gabinete.Remove(gabinete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GabineteExists(int id)
        {
            return _context.Gabinete.Any(e => e.ID == id);
        }
    }
}
