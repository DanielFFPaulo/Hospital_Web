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

namespace Hospital_Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasAPIController : ControllerBase
    {
        private readonly Hospital_WebContext _context;
        private readonly string? _hashedPassword; // Marking _hashedPassword as nullable

        public PessoasAPIController(Hospital_WebContext context, IConfiguration configuration)
        {
            _context = context;
            _hashedPassword = configuration["ApiSettings:HashedAdminPassword"] ?? string.Empty; // Providing a fallback value
        }

        private bool validatePassword()
        {
            if (string.IsNullOrEmpty(_hashedPassword) ||
                !Request.Headers.TryGetValue("password", out var hashable) ||
                !BCrypt.Net.BCrypt.Verify(hashable, _hashedPassword))
            {
                return false;
            }return true;
        }

        // GET: api/PessoasAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pessoa>>> GetPessoa()
        {
            return Unauthorized("Ninguem tem permissão para pedir por todos os registos da Base de Dados");
        }



        // GET: api/PessoasAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> GetPessoa(int id)
        {

            var pessoa = await _context.Pessoa.FindAsync(id);

            if (pessoa == null)
            {
                return NotFound();
            }

            return (validatePassword())? pessoa: Unauthorized("Password invalida ou inexistente");
        }

        // PUT: api/PessoasAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoa(int id, Pessoa pessoa)
        {
            if (!validatePassword())
            {
                return Unauthorized("Password invalida ou inexistente");
            }

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
        public async Task<ActionResult<Pessoa>> PostPessoa(Pessoa pessoa)
        {
            if (!validatePassword())
            {
                return Unauthorized("Password invalida ou inexistente");
            }


            _context.Pessoa.Add(pessoa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPessoa), new { id = pessoa.N_Processo }, pessoa);
        }

        // DELETE: api/PessoasAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePessoa(int id)
        {

            if (!validatePassword())
            {
                return Unauthorized("Password invalida ou inexistente");
            }


            var pessoa = await _context.Pessoa.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }


            var consultasPendentes = await _context.Consulta.Where(c => c.Medico_Id == id || c.Utente_Id == id).ToListAsync();
            if (consultasPendentes != null){
                return StatusCode(400, "A pessoa existe");
            }

            _context.Pessoa.Remove(pessoa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PessoaExists(int id)
        {
            return _context.Pessoa.Any(e => e.N_Processo == id);
        }
    }
}
// check model is valid!!!!