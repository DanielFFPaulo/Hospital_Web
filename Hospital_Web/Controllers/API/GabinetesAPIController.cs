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
    /// Controlador de API responsável pela gestão de gabinetes.
    /// Requer autenticação via JWT Bearer e perfil "Admin".
    /// </summary>
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class GabinetesAPIController : ControllerBase
    {
        /// <summary>
        /// Contexto da base de dados que dá acesso à tabela Gabinete.
        /// </summary>
        private readonly Hospital_WebContext _context;

        /// <summary>
        /// Construtor que injeta o contexto da base de dados.
        /// </summary>
        public GabinetesAPIController(Hospital_WebContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Devolve a lista de todos os gabinetes existentes.
        /// Acesso restrito a utilizadores com perfil "Admin".
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin, Medico")]
        public ActionResult<IEnumerable<Gabinete>> GetGabinete()
        {
            return Unauthorized("Ninguem tem permissão para pedir por todos os registos de uma tabela da base de dados");
        }

        /// <summary>
        /// Devolve os dados de um gabinete específico com base no ID.
        /// Acesso restrito a utilizadores com perfil "Admin".
        /// </summary>
        /// <param name="id">ID do gabinete a obter</param>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Medico")]
        public async Task<ActionResult<Gabinete>> GetGabinete(int id)
        {
            var gabinete = await _context.Gabinete.FindAsync(id);

            if (gabinete == null)
            {
                return NotFound();
            }

            return gabinete;
        }

        /// <summary>
        /// Atualiza os dados de um gabinete existente.
        /// Acesso restrito a utilizadores com perfil "Admin".
        /// </summary>
        /// <param name="id">ID do gabinete</param>
        /// <param name="gabinete">Objeto com os dados atualizados</param>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Medico")]
        public async Task<IActionResult> PutGabinete(int id, Gabinete gabinete)
        {
            if (id != gabinete.ID || !ModelState.IsValid)
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

        /// <summary>
        /// Cria um novo gabinete na base de dados.
        /// Acesso restrito a utilizadores com perfil "Admin".
        /// </summary>
        /// <param name="gabinete">Objeto do novo gabinete</param>
        [HttpPost]
        [Authorize(Roles = "Admin, Medico")]
        public async Task<ActionResult<Gabinete>> PostGabinete(Gabinete gabinete)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Gabinete.Add(gabinete);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGabinete", new { id = gabinete.ID }, gabinete);
        }

        /// <summary>
        /// Elimina um gabinete existente com base no ID.
        /// Acesso restrito a utilizadores com perfil "Admin".
        /// </summary>
        /// <param name="id">ID do gabinete a eliminar</param>
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

        /// <summary>
        /// Verifica se existe um gabinete com o ID fornecido.
        /// </summary>
        /// <param name="id">ID do gabinete</param>
        private bool GabineteExists(int id)
        {
            return _context.Gabinete.Any(e => e.ID == id);
        }
    }
}
