using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital_Web.Data;
using Hospital_Web.Models;

/// <summary>
/// Controlador responsavel pela gestão de gabinetes no sistema hospitalar.
/// Permite realizar operações CRUD (criar, ler, atualizar e eliminar).
/// </summary>
namespace Hospital_Web.Controllers
{
    public class GabinetesController : Controller
    {
        /// <summary>
        /// Contexto da base de dados.
        /// </summary>
        private readonly Hospital_WebContext _context;

        /// <summary>
        /// Construtor do controlador. Injeta o contexto da base de dados.
        /// </summary>
        public GabinetesController(Hospital_WebContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Mostra a lista de todos os gabinetes.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gabinete.ToListAsync());
        }

        /// <summary>
        /// Mostra os detalhes de um gabinete especifico.
        /// </summary>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var gabinete = await _context.Gabinete
                .FirstOrDefaultAsync(m => m.ID == id);
            if (gabinete == null)
                return NotFound();

            return View(gabinete);
        }

        /// <summary>
        /// Mostra o formulario para criar um novo gabinete.
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Processa a criação de um novo gabinete apos submissão do formulario.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao,Equipamento,Disponivel,ID,Bloco,Andar,Numero")] Gabinete gabinete)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gabinete);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gabinete);
        }

        /// <summary>
        /// Mostra o formulario para editar os dados de um gabinete.
        /// </summary>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var gabinete = await _context.Gabinete.FindAsync(id);
            if (gabinete == null)
                return NotFound();

            return View(gabinete);
        }

        /// <summary>
        /// Processa a edição dos dados de um gabinete apos submissão do formulario.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Descricao,Equipamento,Disponivel,ID,Bloco,Andar,Numero")] Gabinete gabinete)
        {
            if (id != gabinete.ID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gabinete);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GabineteExists(gabinete.ID))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gabinete);
        }

        /// <summary>
        /// Mostra o formulario de confirmação para eliminar um gabinete.
        /// </summary>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var gabinete = await _context.Gabinete
                .FirstOrDefaultAsync(m => m.ID == id);
            if (gabinete == null)
                return NotFound();

            return View(gabinete);
        }

        /// <summary>
        /// Elimina definitivamente um gabinete da base de dados.
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gabinete = await _context.Gabinete.FindAsync(id);
            if (gabinete != null)
                _context.Gabinete.Remove(gabinete);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verifica se um gabinete com o ID fornecido existe na base de dados.
        /// </summary>
        private bool GabineteExists(int id)
        {
            return _context.Gabinete.Any(e => e.ID == id);
        }
    }
}
