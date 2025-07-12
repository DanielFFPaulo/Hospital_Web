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

/// <summary>
/// Define o namespace para os controladores de API da aplicação.
/// </summary>
namespace Hospital_Web.Controllers.API
{
    /// <summary>
    /// API responsável pela gestão de entidades do tipo Pessoa.
    /// Requer autenticação via JWT Bearer. Algumas ações exigem perfil de "Admin".
    /// </summary>
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasAPIController(Hospital_WebContext context) : ControllerBase
    {
        /// <summary>
        /// Contexto da base de dados injetado via construtor.
        /// </summary>
        private readonly Hospital_WebContext _context = context;

        /// <summary>
        /// Impede o acesso à lista completa de pessoas.
        /// Acesso negado para todos os utilizadores.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Pessoa>> GetPessoa()
        {
            return Unauthorized("Ninguem tem permissao para pedir por todos os registos da Base de Dados");
        }

        /// <summary>
        /// Devolve os dados de uma pessoa com base no seu número de processo.
        /// Apenas acessível por administradores.
        /// </summary>
        /// <param name="id">Número de processo da pessoa</param>
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

        /// <summary>
        /// Atualiza os dados de uma pessoa.
        /// Apenas acessível por administradores.
        /// </summary>
        /// <param name="id">Número de processo</param>
        /// <param name="pessoa">Dados atualizados</param>
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

        /// <summary>
        /// Cria uma nova pessoa na base de dados.
        /// Apenas acessível por administradores.
        /// </summary>
        /// <param name="pessoa">Objeto com os dados da nova pessoa</param>
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

        /// <summary>
        /// Elimina uma pessoa com base no seu número de processo.
        /// Verifica se existem consultas associadas antes de apagar.
        /// Apenas acessível por administradores.
        /// </summary>
        /// <param name="id">Número de processo da pessoa</param>
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

            if (consultasPendentes.Any())
            {
                return Conflict("Nao e possivel eliminar: esta pessoa tem consultas associadas.");
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

            return StatusCode(200, pessoa.Nome + " foi apagado(a)");
        }

        /// <summary>
        /// Verifica se existe uma pessoa com o número de processo fornecido.
        /// </summary>
        /// <param name="id">Número de processo</param>
        private bool PessoaExists(int id)
        {
            return _context.Pessoa.Any(e => e.N_Processo == id);
        }
    }
}