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
    /// Controlador de API responsável pela gestão de internamentos.
    /// Requer autenticação via JWT Bearer e perfil "Admin".
    /// </summary>
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class InternamentosAPIController : ControllerBase
    {
        /// <summary>
        /// Contexto da base de dados que permite aceder à tabela Internamento.
        /// </summary>
        private readonly Hospital_WebContext _context;

        /// <summary>
        /// Construtor que injeta o contexto da base de dados.
        /// </summary>
        public InternamentosAPIController(Hospital_WebContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Devolve a lista de todos os internamentos existentes.
        /// Acesso restrito a utilizadores com perfil "Admin".
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Internamento>>> GetInternamento()
        {
            return await _context.Internamento.ToListAsync();
        }

        /// <summary>
        /// Devolve os dados de um internamento específico com base no ID.
        /// Acesso restrito a utilizadores com perfil "Admin".
        /// </summary>
        /// <param name="id">ID do internamento</param>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Internamento>> GetInternamento(int id)
        {
            var internamento = await _context.Internamento.FindAsync(id);

            if (internamento == null)
            {
                return NotFound();
            }

            return internamento;
        }

        /// <summary>
        /// Atualiza os dados de um internamento existente.
        /// Acesso restrito a utilizadores com perfil "Admin".
        /// </summary>
        /// <param name="id">ID do internamento</param>
        /// <param name="internamento">Objeto com os dados atualizados</param>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutInternamento(int id, Internamento internamento)
        {
            if (id != internamento.ID)
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

        /// <summary>
        /// Cria um novo registo de internamento.
        /// Acesso restrito a utilizadores com perfil "Admin".
        /// </summary>
        /// <param name="internamento">Objeto do internamento a criar</param>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Internamento>> PostInternamento(Internamento internamento)
        {
            _context.Internamento.Add(internamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInternamento", new { id = internamento.ID }, internamento);
        }

        /// <summary>
        /// Elimina um internamento existente com base no ID.
        /// Acesso restrito a utilizadores com perfil "Admin".
        /// </summary>
        /// <param name="id">ID do internamento a eliminar</param>
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

        /// <summary>
        /// Verifica se existe um internamento com o ID fornecido.
        /// </summary>
        /// <param name="id">ID do internamento</param>
        private bool InternamentoExists(int id)
        {
            return _context.Internamento.Any(e => e.ID == id);
        }
    }
}
