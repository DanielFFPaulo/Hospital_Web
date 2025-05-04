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
    public class LimpezaSalasController : Controller
    {
        private readonly Hospital_WebContext _context;

        public LimpezaSalasController(Hospital_WebContext context)
        {
            _context = context;
        }

        // GET: LimpezaSalas
        public async Task<IActionResult> Index()
        {
            var hospital_WebContext = _context.LimpezaSala.Include(l => l.FuncionarioNavigation).Include(l => l.SalaNavigation);
            return View(await hospital_WebContext.ToListAsync());
        }

        // GET: LimpezaSalas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var limpezaSala = await _context.LimpezaSala
                .Include(l => l.FuncionarioNavigation)
                .Include(l => l.SalaNavigation)
                .FirstOrDefaultAsync(m => m.Funcionario == id);
            if (limpezaSala == null)
            {
                return NotFound();
            }

            return View(limpezaSala);
        }

        // GET: LimpezaSalas/Create
        public IActionResult Create()
        {
            ViewData["Funcionario"] = new SelectList(_context.Funcionario, "ID", "Nome");
            ViewData["Sala"] = new SelectList(_context.Set<Sala>(), "ID", "ID");
            return View();
        }

        // POST: LimpezaSalas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Funcionario,Sala,Produto1,Produto2,Produto3,Data,Hora")] LimpezaSala limpezaSala)
        {
            if (ModelState.IsValid)
            {
                _context.Add(limpezaSala);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Funcionario"] = new SelectList(_context.Funcionario, "ID", "Nome", limpezaSala.Funcionario);
            ViewData["Sala"] = new SelectList(_context.Set<Sala>(), "ID", "ID", limpezaSala.Sala);
            return View(limpezaSala);
        }

        // GET: LimpezaSalas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var limpezaSala = await _context.LimpezaSala.FindAsync(id);
            if (limpezaSala == null)
            {
                return NotFound();
            }
            ViewData["Funcionario"] = new SelectList(_context.Funcionario, "ID", "Nome", limpezaSala.Funcionario);
            ViewData["Sala"] = new SelectList(_context.Set<Sala>(), "ID", "ID", limpezaSala.Sala);
            return View(limpezaSala);
        }

        // POST: LimpezaSalas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Funcionario,Sala,Produto1,Produto2,Produto3,Data,Hora")] LimpezaSala limpezaSala)
        {
            if (id != limpezaSala.Funcionario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(limpezaSala);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LimpezaSalaExists(limpezaSala.Funcionario))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Funcionario"] = new SelectList(_context.Funcionario, "ID", "Nome", limpezaSala.Funcionario);
            ViewData["Sala"] = new SelectList(_context.Set<Sala>(), "ID", "ID", limpezaSala.Sala);
            return View(limpezaSala);
        }

        // GET: LimpezaSalas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var limpezaSala = await _context.LimpezaSala
                .Include(l => l.FuncionarioNavigation)
                .Include(l => l.SalaNavigation)
                .FirstOrDefaultAsync(m => m.Funcionario == id);
            if (limpezaSala == null)
            {
                return NotFound();
            }

            return View(limpezaSala);
        }

        // POST: LimpezaSalas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var limpezaSala = await _context.LimpezaSala.FindAsync(id);
            if (limpezaSala != null)
            {
                _context.LimpezaSala.Remove(limpezaSala);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LimpezaSalaExists(int id)
        {
            return _context.LimpezaSala.Any(e => e.Funcionario == id);
        }
    }
}
