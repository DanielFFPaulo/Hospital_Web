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
    /// Controlador de API responsavel pela gestão de funcionarios de limpeza.
    /// Requer autenticação via JWT Bearer.
    /// </summary>
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioLimpezasAPIController : ControllerBase
    {
        /// <summary>
        /// Contexto da base de dados que permite aceder a tabela FuncionarioLimpeza.
        /// </summary>
        private readonly Hospital_WebContext _context;

        /// <summary>
        /// Construtor que injeta o contexto da base de dados.
        /// </summary>
        public FuncionarioLimpezasAPIController(Hospital_WebContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Devolve a lista de todos os funcionarios de limpeza.
        /// Acesso restrito a utilizadores com perfil "Admin".
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<FuncionarioLimpeza>> GetFuncionarioLimpeza()
        {
            return Unauthorized("Ninguem tem permissão para pedir por todos os registos de uma tabela da base de dados");
        }

        /// <summary>
        /// Devolve os dados de um funcionario de limpeza especifico pelo ID (N_Processo).
        /// Acesso restrito a utilizadores com perfil "Admin".
        /// </summary>
        /// <param name="id">Numero de processo do funcionario</param>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<FuncionarioLimpeza>> GetFuncionarioLimpeza(int id)
        {
            var funcionarioLimpeza = await _context.FuncionarioLimpeza.FindAsync(id);

            if (funcionarioLimpeza == null)
            {
                return NotFound();
            }

            return funcionarioLimpeza;
        }

        /// <summary>
        /// Atualiza os dados de um funcionario de limpeza existente.
        /// Acesso restrito a utilizadores com perfil "Admin".
        /// </summary>
        /// <param name="id">ID do funcionario</param>
        /// <param name="funcionarioLimpeza">Objeto com os dados atualizados</param>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutFuncionarioLimpeza(int id, FuncionarioLimpeza funcionarioLimpeza)
        {
            if (id != funcionarioLimpeza.N_Processo || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Entry(funcionarioLimpeza).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioLimpezaExists(id))
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
        /// Cria um novo funcionario de limpeza na base de dados.
        /// Acesso restrito a utilizadores com perfil "Admin".
        /// </summary>
        /// <param name="funcionarioLimpeza">Objeto do novo funcionario</param>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<FuncionarioLimpeza>> PostFuncionarioLimpeza(FuncionarioLimpeza funcionarioLimpeza)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.FuncionarioLimpeza.Add(funcionarioLimpeza);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFuncionarioLimpeza", new { id = funcionarioLimpeza.N_Processo }, funcionarioLimpeza);
        }

        /// <summary>
        /// Elimina um funcionario de limpeza com base no seu ID.
        /// Acesso restrito a utilizadores com perfil "Admin".
        /// </summary>
        /// <param name="id">ID do funcionario a eliminar</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFuncionarioLimpeza(int id)
        {
            var funcionarioLimpeza = await _context.FuncionarioLimpeza.FindAsync(id);
            if (funcionarioLimpeza == null)
            {
                return NotFound();
            }

            _context.FuncionarioLimpeza.Remove(funcionarioLimpeza);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Verifica se existe um funcionario de limpeza com o ID fornecido.
        /// </summary>
        /// <param name="id">ID a verificar</param>
        private bool FuncionarioLimpezaExists(int id)
        {
            return _context.FuncionarioLimpeza.Any(e => e.N_Processo == id);
        }
    }
}
