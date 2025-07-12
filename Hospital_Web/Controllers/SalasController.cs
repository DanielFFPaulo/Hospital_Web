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
    /// Controlador responsavel pela gestão das salas hospitalares.
    /// Permite listar, pesquisar, criar, editar e apagar salas.
    /// </summary>
    public class SalasController : Controller
    {
        private readonly Hospital_WebContext _context;

        /// <summary>
        /// Construtor que injeta o contexto da base de dados.
        /// </summary>
        public SalasController(Hospital_WebContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Apresenta a lista de todas as salas.
        /// Permite filtrar por bloco atraves da barra de pesquisa.
        /// </summary>
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Sala == null)
            {
                return Problem("Entity set 'Hospital_WebContext.Sala' is null.");
            }

            var sala = from s in _context.Sala select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                sala = sala.Where(s => s.Bloco!.ToUpper().Contains(searchString.ToUpper()));
            }

            return View(await sala.ToListAsync());
        }

        /// <summary>
        /// Mostra os detalhes de uma sala especifica com base no ID.
        /// </summary>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var sala = await _context.Sala.FirstOrDefaultAsync(m => m.ID == id);
            if (sala == null)
                return NotFound();

            return View(sala);
        }

        /// <summary>
        /// Exibe o formulario de criação de nova sala.
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Cria uma nova sala na base de dados apos submissão do formulario.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Bloco,Andar,Numero")] Sala sala)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sala);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sala);
        }

        /// <summary>
        /// Exibe o formulario para editar os dados de uma sala existente.
        /// </summary>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var sala = await _context.Sala.FindAsync(id);
            if (sala == null)
                return NotFound();

            return View(sala);
        }

        /// <summary>
        /// Guarda as alterações feitas na sala apos a edição.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Bloco,Andar,Numero")] Sala sala)
        {
            if (id != sala.ID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sala);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaExists(sala.ID))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sala);
        }

        /// <summary>
        /// Apresenta a confirmação de eliminação da sala.
        /// </summary>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var sala = await _context.Sala.FirstOrDefaultAsync(m => m.ID == id);
            if (sala == null)
                return NotFound();

            return View(sala);
        }

        /// <summary>
        /// Elimina a sala da base de dados apos confirmação.
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sala = await _context.Sala.FindAsync(id);
            if (sala != null)
            {
                _context.Sala.Remove(sala);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verifica se uma sala com o ID especificado existe na base de dados.
        /// </summary>
        private bool SalaExists(int id)
        {
            return _context.Sala.Any(e => e.ID == id);
        }
    }
}