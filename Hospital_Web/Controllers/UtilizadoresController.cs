using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital_Web.Data;
using Hospital_Web.Models;


namespace Hospital_Web.Controllers
{
    /// <summary>
    /// Controlador responsável pela gestão de utilizadores associados a pessoas no sistema.
    /// Permite criar, editar, visualizar e apagar utilizadores.
    /// </summary>
    public class UtilizadoresController : Controller
    {
        private readonly Hospital_WebContext _context;

        /// <summary>
        /// Injeta o contexto da base de dados.
        /// </summary>
        public UtilizadoresController(Hospital_WebContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Mostra a lista de todos os utilizadores com os dados da pessoa associada.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var hospital_WebContext = _context.Utilizador.Include(u => u.Pessoa);
            return View(await hospital_WebContext.ToListAsync());
        }

        /// <summary>
        /// Mostra os detalhes de um utilizador específico, incluindo a pessoa associada.
        /// </summary>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var utilizador = await _context.Utilizador
                .Include(u => u.Pessoa)
                .FirstOrDefaultAsync(m => m.ID_Utilizador == id);

            if (utilizador == null)
                return NotFound();

            return View(utilizador);
        }

        /// <summary>
        /// Apresenta o formulário de criação de um novo utilizador.
        /// Define a data de criação automaticamente.
        /// </summary>
        public IActionResult Create()
        {
            var utilizador = new Utilizador
            {
                Data_Criacao_Conta = DateTime.Now
            };

            ViewData["Pessoa_Id"] = new SelectList(_context.Pessoa, "N_Processo", "Nome");
            return View(utilizador);
        }

        /// <summary>
        /// Regista um novo utilizador na base de dados.
        /// Valida o modelo e guarda se for válido.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Utilizador,Nome_Utilizador,Password,Data_Criacao_Conta,Pessoa_Id")] Utilizador utilizador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(utilizador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Pessoa_Id"] = new SelectList(_context.Pessoa, "N_Processo", "Nome", utilizador.Pessoa_Id);
            return View(utilizador);
        }

        /// <summary>
        /// Apresenta o formulário de edição de um utilizador existente.
        /// </summary>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var utilizador = await _context.Utilizador.FindAsync(id);
            if (utilizador == null)
                return NotFound();

            ViewData["Pessoa_Id"] = new SelectList(_context.Pessoa, "N_Processo", "Nome", utilizador.Pessoa_Id);
            return View(utilizador);
        }

        /// <summary>
        /// Atualiza os dados de um utilizador.
        /// Verifica conflitos de concorrência e trata erros de atualização.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Utilizador,Nome_Utilizador,Password,Data_Criacao_Conta,Pessoa_Id")] Utilizador utilizador)
        {
            if (id != utilizador.ID_Utilizador)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utilizador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilizadorExists(utilizador.ID_Utilizador))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["Pessoa_Id"] = new SelectList(_context.Pessoa, "N_Processo", "Nome", utilizador.Pessoa_Id);
            return View(utilizador);
        }

        /// <summary>
        /// Apresenta o formulário de confirmação para apagar um utilizador.
        /// </summary>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var utilizador = await _context.Utilizador
                .Include(u => u.Pessoa)
                .FirstOrDefaultAsync(m => m.ID_Utilizador == id);

            if (utilizador == null)
                return NotFound();

            return View(utilizador);
        }

        /// <summary>
        /// Apaga o utilizador da base de dados após confirmação.
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utilizador = await _context.Utilizador.FindAsync(id);
            if (utilizador != null)
            {
                _context.Utilizador.Remove(utilizador);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verifica se existe um utilizador com o ID especificado.
        /// </summary>
        private bool UtilizadorExists(int id)
        {
            return _context.Utilizador.Any(e => e.ID_Utilizador == id);
        }
    }
}
