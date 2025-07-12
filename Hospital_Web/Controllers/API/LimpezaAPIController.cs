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
/// Define o namespace dos controladores de API da aplicação.
/// </summary>
namespace Hospital_Web.Controllers.API
{
    /// <summary>
    /// Controlador de API responsável pela gestão de registos de limpeza de salas.
    /// Requer autenticação via JWT Bearer e perfil "Admin".
    /// </summary>
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class LimpezaAPIController : ControllerBase
    {
        /// <summary>
        /// Contexto da base de dados que dá acesso à tabela LimpezaSala.
        /// </summary>
        private readonly Hospital_WebContext _context;

        /// <summary>
        /// Construtor que injeta o contexto da base de dados.
        /// </summary>
        public LimpezaAPIController(Hospital_WebContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Devolve a lista de todas as limpezas de sala registadas.
        /// Acesso restrito a utilizadores com perfil "Admin".
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<LimpezaSala>>> GetLimpezaSala()
        {
            return await _context.LimpezaSala.ToListAsync();
        }

        /// <summary>
        /// Devolve os dados de uma limpeza de sala específica pelo ID.
        /// Acesso restrito a utilizadores com perfil "Admin".
        /// </summary>
        /// <param name="id">ID da limpeza de sala</param>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<LimpezaSala>> GetLimpezaSala(int id)
        {
            var limpezaSala = await _context.LimpezaSala.FindAsync(id);

            if (limpezaSala == null)
            {
                return NotFound();
            }

            return limpezaSala;
        }

        /// <summary>
        /// Atualiza os dados de uma limpeza de sala existente.
        /// Acesso restrito a utilizadores com perfil "Admin".
        /// </summary>
        /// <param name="id">ID da limpeza</param>
        /// <param name="limpezaSala">Objeto com os dados atualizados</param>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutLimpezaSala(int id, LimpezaSala limpezaSala)
        {
            if (id != limpezaSala.ID)
            {
                return BadRequest();
            }

            _context.Entry(limpezaSala).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LimpezaSalaExists(id))
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
        /// Cria um novo registo de limpeza de sala.
        /// Acesso restrito a utilizadores com perfil "Admin".
        /// </summary>
        /// <param name="limpezaSala">Objeto com os dados da nova limpeza</param>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<LimpezaSala>> PostLimpezaSala(LimpezaSala limpezaSala)
        {
            _context.LimpezaSala.Add(limpezaSala);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLimpezaSala", new { id = limpezaSala.ID }, limpezaSala);
        }

        /// <summary>
        /// Elimina um registo de limpeza de sala com base no ID.
        /// Acesso restrito a utilizadores com perfil "Admin".
        /// </summary>
        /// <param name="id">ID da limpeza a eliminar</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteLimpezaSala(int id)
        {
            var limpezaSala = await _context.LimpezaSala.FindAsync(id);
            if (limpezaSala == null)
            {
                return NotFound();
            }

            _context.LimpezaSala.Remove(limpezaSala);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Verifica se existe um registo de limpeza de sala com o ID fornecido.
        /// </summary>
        /// <param name="id">ID a verificar</param>
        private bool LimpezaSalaExists(int id)
        {
            return _context.LimpezaSala.Any(e => e.ID == id);
        }
    }
}

