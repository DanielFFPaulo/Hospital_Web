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
/// Define o namespace para controladores de API da aplicação.
/// </summary>
namespace Hospital_Web.Controllers.API
{
    /// <summary>
    /// Controlador de API para operações CRUD sobre a entidade Consulta.
    /// Protegido por autenticação Bearer (JWT).
    /// </summary>
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasAPIController : ControllerBase
    {
        /// <summary>
        /// Contexto da base de dados que permite acesso à tabela Consulta.
        /// </summary>
        private readonly Hospital_WebContext _context;

        /// <summary>
        /// Construtor que injeta o contexto da base de dados.
        /// </summary>
        public ConsultasAPIController(Hospital_WebContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém a lista completa de consultas.
        /// Acesso restrito a utilizadores com perfil Admin.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin, Medico")]
        public ActionResult<IEnumerable<Consulta>> GetConsulta()
        {
            return Unauthorized("Ninguem tem permissão para pedir por todos os registos de uma tabela da base de dados");
        }

        /// <summary>
        /// Obtém uma consulta específica pelo seu ID (episódio).
        /// Acesso restrito a utilizadores com perfil Admin.
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Medico")]
        public async Task<ActionResult<Consulta>> GetConsulta(int id)
        {
            var consulta = await _context.Consulta.FindAsync(id);

            if (consulta == null)
            {
                return NotFound();
            }

            return consulta;
        }

        /// <summary>
        /// Atualiza os dados de uma consulta existente.
        /// Acesso restrito a utilizadores com perfil Admin.
        /// </summary>
        /// <param name="id">ID da consulta a ser atualizada</param>
        /// <param name="consulta">Objeto com os dados atualizados</param>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Medico")]
        public async Task<IActionResult> PutConsulta(int id, Consulta consulta)
        {
            if (id != consulta.Episodio || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Entry(consulta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultaExists(id))
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
        /// Cria uma nova consulta.
        /// Acesso restrito a utilizadores com perfil Admin.
        /// </summary>
        /// <param name="consulta">Objeto da nova consulta</param>
        [HttpPost]
        [Authorize(Roles = "Admin, Medico")]
        public async Task<ActionResult<Consulta>> PostConsulta(Consulta consulta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            _context.Consulta.Add(consulta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConsulta", new { id = consulta.Episodio }, consulta);
        }

        /// <summary>
        /// Elimina uma consulta pelo seu ID.
        /// Acesso restrito a utilizadores com perfil Admin.
        /// </summary>
        /// <param name="id">ID da consulta a eliminar</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Medico")]
        public async Task<IActionResult> DeleteConsulta(int id)
        {
            var consulta = await _context.Consulta.FindAsync(id);
            if (consulta == null)
            {
                return NotFound();
            }

            _context.Consulta.Remove(consulta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Verifica se existe uma consulta com o ID fornecido.
        /// </summary>
        /// <param name="id">ID da consulta</param>
        private bool ConsultaExists(int id)
        {
            return _context.Consulta.Any(e => e.Episodio == id);
        }
    }
}
