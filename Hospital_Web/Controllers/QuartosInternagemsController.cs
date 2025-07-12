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
    /// Controlador responsável pela gestão dos quartos de internamento no hospital.
    /// Permite listar, visualizar detalhes, criar, editar e eliminar quartos.
    /// </summary>
    public class QuartosInternagemsController : Controller
    {
        private readonly Hospital_WebContext _context;

        /// <summary>
        /// Construtor que recebe o contexto da base de dados.
        /// </summary>
        public QuartosInternagemsController(Hospital_WebContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Mostra todos os quartos de internamento.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            return View(await _context.QuartosInternagem.ToListAsync());
        }

        /// <summary>
        /// Mostra os detalhes de um quarto específico.
        /// </summary>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var quartosInternagem = await _context.QuartosInternagem
                .FirstOrDefaultAsync(m => m.ID == id);
            if (quartosInternagem == null)
                return NotFound();

            return View(quartosInternagem);
        }

        /// <summary>
        /// Mostra o formulário de criação de um novo quarto.
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Cria um novo quarto de internamento a partir do formulário.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao,Tipo,Capacidade,ID,Bloco,Andar,Numero")] QuartosInternagem quartosInternagem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quartosInternagem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quartosInternagem);
        }

        /// <summary>
        /// Mostra o formulário de edição de um quarto existente.
        /// </summary>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var quartosInternagem = await _context.QuartosInternagem.FindAsync(id);
            if (quartosInternagem == null)
                return NotFound();

            return View(quartosInternagem);
        }

        /// <summary>
        /// Atualiza os dados de um quarto de internamento após edição.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Descricao,Tipo,Capacidade,ID,Bloco,Andar,Numero")] QuartosInternagem quartosInternagem)
        {
            if (id != quartosInternagem.ID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quartosInternagem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuartosInternagemExists(quartosInternagem.ID))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(quartosInternagem);
        }

        /// <summary>
        /// Mostra a confirmação para eliminar um quarto de internamento.
        /// </summary>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var quartosInternagem = await _context.QuartosInternagem
                .FirstOrDefaultAsync(m => m.ID == id);
            if (quartosInternagem == null)
                return NotFound();

            return View(quartosInternagem);
        }

        /// <summary>
        /// Elimina um quarto de internamento da base de dados após confirmação.
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quartosInternagem = await _context.QuartosInternagem.FindAsync(id);
            if (quartosInternagem != null)
            {
                _context.QuartosInternagem.Remove(quartosInternagem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verifica se um quarto de internamento com o ID especificado existe na base de dados.
        /// </summary>
        private bool QuartosInternagemExists(int id)
        {
            return _context.QuartosInternagem.Any(e => e.ID == id);
        }
    }
}
