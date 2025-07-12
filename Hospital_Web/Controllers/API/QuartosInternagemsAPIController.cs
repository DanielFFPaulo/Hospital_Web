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
    /// API responsável pela gestão de quartos de internamento.
    /// Requer autenticação via JWT Bearer e perfil de administrador para todas as ações.
    /// </summary>
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class QuartosInternagemsAPIController : ControllerBase
    {
        /// <summary>
        /// Contexto da base de dados injetado via construtor.
        /// </summary>
        private readonly Hospital_WebContext _context;

        /// <summary>
        /// Construtor que recebe o contexto da base de dados.
        /// </summary>
        public QuartosInternagemsAPIController(Hospital_WebContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém a lista de todos os quartos de internamento.
        /// Apenas acessível por administradores.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<QuartosInternagem>> GetQuartosInternagem()
        {
            return Unauthorized("Ninguem tem permissão para pedir por todos os registos de uma tabela da base de dados");
        }

        /// <summary>
        /// Obtém um quarto de internamento pelo seu ID.
        /// Apenas acessível por administradores.
        /// </summary>
        /// <param name="id">ID do quarto de internamento</param>
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

        /// <summary>
        /// Atualiza os dados de um quarto de internamento existente.
        /// Apenas acessível por administradores.
        /// </summary>
        /// <param name="id">ID do quarto</param>
        /// <param name="quartosInternagem">Objeto com dados atualizados</param>
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

        /// <summary>
        /// Cria um novo quarto de internamento.
        /// Apenas acessível por administradores.
        /// </summary>
        /// <param name="quartosInternagem">Objeto a ser criado</param>
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

        /// <summary>
        /// Elimina um quarto de internamento pelo seu ID.
        /// Apenas acessível por administradores.
        /// </summary>
        /// <param name="id">ID do quarto</param>
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

        /// <summary>
        /// Verifica se um quarto de internamento com determinado ID existe na base de dados.
        /// </summary>
        /// <param name="id">ID a verificar</param>
        private bool QuartosInternagemExists(int id)
        {
            return _context.QuartosInternagem.Any(e => e.ID == id);
        }
    }
}