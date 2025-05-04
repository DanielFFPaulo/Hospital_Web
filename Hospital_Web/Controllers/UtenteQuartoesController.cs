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
    public class UtenteQuartoesController : Controller
    {
        private readonly Hospital_WebContext _context;

        public UtenteQuartoesController(Hospital_WebContext context)
        {
            _context = context;
        }

        // GET: UtenteQuartoes
        public async Task<IActionResult> Index()
        {
            var hospital_WebContext = _context.UtenteQuarto.Include(u => u.QuartoNavigation).Include(u => u.UtenteNavigation);
            return View(await hospital_WebContext.ToListAsync());
        }

        // GET: UtenteQuartoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utenteQuarto = await _context.UtenteQuarto
                .Include(u => u.QuartoNavigation)
                .Include(u => u.UtenteNavigation)
                .FirstOrDefaultAsync(m => m.Utente == id);
            if (utenteQuarto == null)
            {
                return NotFound();
            }

            return View(utenteQuarto);
        }

        // GET: UtenteQuartoes/Create
        public IActionResult Create()
        {
            ViewData["Quarto"] = new SelectList(_context.Set<QuartoInternagem>(), "N_Identificador", "N_Identificador");
            ViewData["Utente"] = new SelectList(_context.Utente, "N_Processo", "Nome");
            return View();
        }

        // POST: UtenteQuartoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Utente,Quarto")] UtenteQuarto utenteQuarto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(utenteQuarto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Quarto"] = new SelectList(_context.Set<QuartoInternagem>(), "N_Identificador", "N_Identificador", utenteQuarto.Quarto);
            ViewData["Utente"] = new SelectList(_context.Utente, "N_Processo", "Nome", utenteQuarto.Utente);
            return View(utenteQuarto);
        }

        // GET: UtenteQuartoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utenteQuarto = await _context.UtenteQuarto.FindAsync(id);
            if (utenteQuarto == null)
            {
                return NotFound();
            }
            ViewData["Quarto"] = new SelectList(_context.Set<QuartoInternagem>(), "N_Identificador", "N_Identificador", utenteQuarto.Quarto);
            ViewData["Utente"] = new SelectList(_context.Utente, "N_Processo", "Nome", utenteQuarto.Utente);
            return View(utenteQuarto);
        }

        // POST: UtenteQuartoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Utente,Quarto")] UtenteQuarto utenteQuarto)
        {
            if (id != utenteQuarto.Utente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utenteQuarto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtenteQuartoExists(utenteQuarto.Utente))
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
            ViewData["Quarto"] = new SelectList(_context.Set<QuartoInternagem>(), "N_Identificador", "N_Identificador", utenteQuarto.Quarto);
            ViewData["Utente"] = new SelectList(_context.Utente, "N_Processo", "Nome", utenteQuarto.Utente);
            return View(utenteQuarto);
        }

        // GET: UtenteQuartoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utenteQuarto = await _context.UtenteQuarto
                .Include(u => u.QuartoNavigation)
                .Include(u => u.UtenteNavigation)
                .FirstOrDefaultAsync(m => m.Utente == id);
            if (utenteQuarto == null)
            {
                return NotFound();
            }

            return View(utenteQuarto);
        }

        // POST: UtenteQuartoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utenteQuarto = await _context.UtenteQuarto.FindAsync(id);
            if (utenteQuarto != null)
            {
                _context.UtenteQuarto.Remove(utenteQuarto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtenteQuartoExists(int id)
        {
            return _context.UtenteQuarto.Any(e => e.Utente == id);
        }
    }
}
