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
    /// Controlador responsável pela gestão das limpezas realizadas nas salas do hospital.
    /// Permite criar, visualizar, editar e remover registos de limpezas.
    /// </summary>
    public class LimpezaSalasController : Controller
    {
        private readonly Hospital_WebContext _context;

        /// <summary>
        /// Construtor que injeta o contexto da base de dados.
        /// </summary>
        public LimpezaSalasController(Hospital_WebContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Mostra a lista de limpezas de salas, com os dados dos funcionários e salas associados.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var hospital_WebContext = _context.LimpezaSala
                .Include(l => l.Funcionario)
                .Include(l => l.Sala);
            return View(await hospital_WebContext.ToListAsync());
        }

        /// <summary>
        /// Mostra os detalhes de uma limpeza de sala específica.
        /// </summary>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var limpezaSala = await _context.LimpezaSala
                .Include(l => l.Funcionario)
                .Include(l => l.Sala)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (limpezaSala == null)
                return NotFound();

            return View(limpezaSala);
        }

        /// <summary>
        /// Devolve o formulário para criar um novo registo de limpeza de sala.
        /// </summary>
        public IActionResult Create()
        {
            ViewData["Funcionario_Id"] = new SelectList(_context.FuncionarioLimpeza, "N_Processo", "NIF");
            ViewData["Sala_Id"] = new SelectList(_context.Sala, "ID", "Bloco");
            return View();
        }

        /// <summary>
        /// Cria um novo registo de limpeza de sala se os dados forem válidos.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Produto1,Produto2,Produto3,Data,Hora,Funcionario_Id,Sala_Id")] LimpezaSala limpezaSala)
        {
            if (ModelState.IsValid)
            {
                _context.Add(limpezaSala);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Recarrega dropdowns em caso de erro
            ViewData["Funcionario_Id"] = new SelectList(_context.FuncionarioLimpeza, "N_Processo", "NIF", limpezaSala.Funcionario_Id);
            ViewData["Sala_Id"] = new SelectList(_context.Sala, "ID", "Bloco", limpezaSala.Sala_Id);
            return View(limpezaSala);
        }

        /// <summary>
        /// Devolve o formulário de edição de um registo de limpeza de sala existente.
        /// </summary>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var limpezaSala = await _context.LimpezaSala.FindAsync(id);
            if (limpezaSala == null)
                return NotFound();

            ViewData["Funcionario_Id"] = new SelectList(_context.FuncionarioLimpeza, "N_Processo", "NIF", limpezaSala.Funcionario_Id);
            ViewData["Sala_Id"] = new SelectList(_context.Sala, "ID", "Bloco", limpezaSala.Sala_Id);
            return View(limpezaSala);
        }

        /// <summary>
        /// Guarda as alterações feitas a um registo de limpeza de sala.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Produto1,Produto2,Produto3,Data,Hora,Funcionario_Id,Sala_Id")] LimpezaSala limpezaSala)
        {
            if (id != limpezaSala.ID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(limpezaSala);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LimpezaSalaExists(limpezaSala.ID))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            // Recarrega dropdowns em caso de erro
            ViewData["Funcionario_Id"] = new SelectList(_context.FuncionarioLimpeza, "N_Processo", "NIF", limpezaSala.Funcionario_Id);
            ViewData["Sala_Id"] = new SelectList(_context.Sala, "ID", "Bloco", limpezaSala.Sala_Id);
            return View(limpezaSala);
        }

        /// <summary>
        /// Mostra os dados de uma limpeza de sala antes de confirmar a eliminação.
        /// </summary>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var limpezaSala = await _context.LimpezaSala
                .Include(l => l.Funcionario)
                .Include(l => l.Sala)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (limpezaSala == null)
                return NotFound();

            return View(limpezaSala);
        }

        /// <summary>
        /// Elimina um registo de limpeza de sala da base de dados.
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var limpezaSala = await _context.LimpezaSala.FindAsync(id);
            if (limpezaSala != null)
                _context.LimpezaSala.Remove(limpezaSala);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verifica se existe um registo de limpeza de sala com o ID fornecido.
        /// </summary>
        private bool LimpezaSalaExists(int id)
        {
            return _context.LimpezaSala.Any(e => e.ID == id);
        }
    }
}
