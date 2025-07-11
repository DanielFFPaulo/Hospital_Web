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
    public class FuncionarioLimpezasAPIController : ControllerBase
    {
        private readonly Hospital_WebContext _context;

        public FuncionarioLimpezasAPIController(Hospital_WebContext context)
        {
            _context = context;
        }

        // GET: api/FuncionarioLimpezasAPI
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<FuncionarioLimpeza>> GetFuncionarioLimpeza()
        {
            return Unauthorized("Ninguem tem permissão para pedir por todos os registos de uma tabela da base de dados");
        }

        // GET: api/FuncionarioLimpezasAPI/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<FuncionarioLimpeza>> GetFuncionarioLimpeza(int id)
        {
            var funcionarioLimpeza = await _context.FuncionarioLimpeza.FindAsync(id);

            if (funcionarioLimpeza == null)
            {
                return NotFound();
            }

            return funcionarioLimpeza;
        }

        // PUT: api/FuncionarioLimpezasAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutFuncionarioLimpeza(int id, FuncionarioLimpeza funcionarioLimpeza)
        {
            if (id != funcionarioLimpeza.N_Processo || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Entry(funcionarioLimpeza).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioLimpezaExists(id))
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

        // POST: api/FuncionarioLimpezasAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<FuncionarioLimpeza>> PostFuncionarioLimpeza(FuncionarioLimpeza funcionarioLimpeza)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.FuncionarioLimpeza.Add(funcionarioLimpeza);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFuncionarioLimpeza", new { id = funcionarioLimpeza.N_Processo }, funcionarioLimpeza);
        }

        // DELETE: api/FuncionarioLimpezasAPI/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFuncionarioLimpeza(int id)
        {
            var funcionarioLimpeza = await _context.FuncionarioLimpeza.FindAsync(id);
            if (funcionarioLimpeza == null)
            {
                return NotFound();
            }

            _context.FuncionarioLimpeza.Remove(funcionarioLimpeza);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FuncionarioLimpezaExists(int id)
        {
            return _context.FuncionarioLimpeza.Any(e => e.N_Processo == id);
        }
    }
}
