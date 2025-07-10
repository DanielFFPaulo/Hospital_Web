using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital_Web.Data;
using Hospital_Web.Models;
using BCrypt;
using Microsoft.AspNetCore.Authorization;

namespace Hospital_Web.Controllers.API
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasAPIController(Hospital_WebContext context) : ControllerBase
    {
        private readonly Hospital_WebContext _context = context;


        // GET: api/PessoasAPI
        [HttpGet]
        public ActionResult<IEnumerable<Pessoa>> GetPessoa()
        {
            return Unauthorized("Ninguem tem permissão para pedir por todos os registos da Base de Dados");
        }



        // GET: api/PessoasAPI/5
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> GetPessoa(int id)
        {
            var pessoa = await _context.Pessoa.FindAsync(id);

            if (pessoa == null)
            {
                return NotFound();
            }

            return pessoa;
        }

        // PUT: api/PessoasAPI/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutPessoa(int id, Pessoa pessoa)
        {

            if (id != pessoa.N_Processo)
            {
                return BadRequest();
            }

            _context.Entry(pessoa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaExists(id))
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

        // POST: api/PessoasAPI
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Pessoa>> PostPessoa(Pessoa pessoa)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Pessoa.Add(pessoa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPessoa), new { id = pessoa.N_Processo }, pessoa);
        }

        // DELETE: api/PessoasAPI/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePessoa(int id)
        {


            var pessoa = await _context.Pessoa.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }


            var consultasPendentes = await _context.Consulta
                .Where(c => c.Medico_Id == id || c.Utente_Id == id)
                .ToListAsync();


            if (consultasPendentes.Any()){
                return Conflict("Não é possível eliminar: esta pessoa tem consultas associadas.");
            }

            try
            {
                _context.Pessoa.Remove(pessoa);
                await _context.SaveChangesAsync();
                

            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Erro ao eliminar: {ex.Message}");
            }
            return StatusCode(200,pessoa.Nome + " foi apagado(a)");
        }

        private bool PessoaExists(int id)
        {
            return _context.Pessoa.Any(e => e.N_Processo == id);
        }
    }
}
// check model is valid!!!!