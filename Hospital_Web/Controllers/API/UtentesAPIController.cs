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


/// <summary>
/// Define o namespace para os controladores de API da aplicação.
/// </summary>
namespace Hospital_Web.Controllers.API
{
    /// <summary>
    /// API responsável pela gestão dos utentes do hospital.
    /// Requer autenticação por token JWT e perfil de administrador.
    /// </summary>
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class UtentesAPIController : ControllerBase
    {
        /// <summary>
        /// Contexto da base de dados do hospital.
        /// </summary>
        private readonly Hospital_WebContext _context;

        /// <summary>
        /// Construtor que recebe o contexto da base de dados.
        /// </summary>
        public UtentesAPIController(Hospital_WebContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todos os utentes registados na base de dados.
        /// Acesso restrito a administradores.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<Utente>> GetUtente()
        {
            return Unauthorized("Ninguem tem permissão para pedir por todos os registos de uma tabela da base de dados");
        }

        /// <summary>
        /// Obtém um utente específico pelo seu número de processo.
        /// Acesso restrito a administradores.
        /// </summary>
        /// <param name="id">Número de processo do utente</param>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Utente>> GetUtente(int id)
        {
            var utente = await _context.Utente.FindAsync(id);

            if (utente == null)
            {
                return NotFound();
            }

            return utente;
        }

        /// <summary>
        /// Atualiza os dados de um utente existente.
        /// Acesso restrito a administradores.
        /// </summary>
        /// <param name="id">Número de processo do utente</param>
        /// <param name="utente">Objeto utente com dados atualizados</param>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutUtente(int id, Utente utente)
        {
            if (id != utente.N_Processo || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Entry(utente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtenteExists(id))
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

        /// <summary>
        /// Regista um novo utente na base de dados.
        /// Acesso restrito a administradores.
        /// </summary>
        /// <param name="utente">Objeto utente a ser criado</param>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Utente>> PostUtente(Utente utente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Utente.Add(utente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUtente", new { id = utente.N_Processo }, utente);
        }

        /// <summary>
        /// Elimina um utente da base de dados.
        /// Acesso restrito a administradores.
        /// </summary>
        /// <param name="id">Número de processo do utente</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUtente(int id)
        {
            var utente = await _context.Utente.FindAsync(id);
            if (utente == null)
            {
                return NotFound();
            }

            _context.Utente.Remove(utente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Verifica se um utente com o número de processo especificado existe.
        /// </summary>
        /// <param name="id">Número de processo do utente</param>
        private bool UtenteExists(int id)
        {
            return _context.Utente.Any(e => e.N_Processo == id);
        }
    }
}
