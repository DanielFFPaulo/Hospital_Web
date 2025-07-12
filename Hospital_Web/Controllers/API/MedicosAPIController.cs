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
    /// Controlador de API responsável pela gestão de médicos.
    /// Requer autenticação via JWT Bearer e perfil "Admin".
    /// </summary>
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class MedicosAPIController : ControllerBase
    {
        /// <summary>
        /// Contexto da base de dados usado para aceder à tabela de médicos.
        /// </summary>
        private readonly Hospital_WebContext _context;

        /// <summary>
        /// Construtor que injeta o contexto da base de dados.
        /// </summary>
        public MedicosAPIController(Hospital_WebContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Devolve a lista de todos os médicos.
        /// Acesso restrito a utilizadores com perfil "Admin".
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<Medico>> GetMedico()
        {
            return Unauthorized("Ninguem tem permissão para pedir por todos os registos de uma tabela da base de dados"); ;
        }

        /// <summary>
        /// Devolve um médico específico pelo seu número de processo.
        /// Acesso restrito a utilizadores com perfil "Admin".
        /// </summary>
        /// <param name="id">Número de processo do médico</param>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Medico>> GetMedico(int id)
        {
            var medico = await _context.Medico.FindAsync(id);

            if (medico == null)
            {
                return NotFound();
            }

            return medico;
        }

        /// <summary>
        /// Atualiza os dados de um médico existente.
        /// Acesso restrito a utilizadores com perfil "Admin".
        /// </summary>
        /// <param name="id">Número de processo do médico</param>
        /// <param name="medico">Objeto com os dados atualizados</param>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutMedico(int id, Medico medico)
        {
            if (id != medico.N_Processo || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Entry(medico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicoExists(id))
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
        /// Cria um novo médico.
        /// Acesso restrito a utilizadores com perfil "Admin".
        /// </summary>
        /// <param name="medico">Objeto com os dados do médico a criar</param>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Medico>> PostMedico(Medico medico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Medico.Add(medico);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedico", new { id = medico.N_Processo }, medico);
        }

        /// <summary>
        /// Elimina um médico com base no seu número de processo.
        /// Acesso restrito a utilizadores com perfil "Admin".
        /// </summary>
        /// <param name="id">Número de processo do médico a eliminar</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMedico(int id)
        {
            var medico = await _context.Medico.FindAsync(id);
            if (medico == null)
            {
                return NotFound();
            }

            _context.Medico.Remove(medico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Verifica se existe um médico com o número de processo fornecido.
        /// </summary>
        /// <param name="id">Número de processo do médico</param>
        private bool MedicoExists(int id)
        {
            return _context.Medico.Any(e => e.N_Processo == id);
        }
    }
}
