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
    public class QuartoInternagemsController : Controller
    {
        private readonly Hospital_WebContext _context;

        public QuartoInternagemsController(Hospital_WebContext context)
        {
            _context = context;
        }

        // GET: QuartoInternagems
        public async Task<IActionResult> Index()
        {
            return View(await _context.QuartoInternagem.ToListAsync());
        }

        // GET: QuartoInternagems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quartoInternagem = await _context.QuartoInternagem
                .FirstOrDefaultAsync(m => m.N_Identificador == id);
            if (quartoInternagem == null)
            {
                return NotFound();
            }

            return View(quartoInternagem);
        }

        // GET: QuartoInternagems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QuartoInternagems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("N_Identificador,Descricao,Tipo,Capacidade,Ocupado")] QuartoInternagem quartoInternagem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quartoInternagem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quartoInternagem);
        }

        // GET: QuartoInternagems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quartoInternagem = await _context.QuartoInternagem.FindAsync(id);
            if (quartoInternagem == null)
            {
                return NotFound();
            }
            return View(quartoInternagem);
        }

        // POST: QuartoInternagems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("N_Identificador,Descricao,Tipo,Capacidade,Ocupado")] QuartoInternagem quartoInternagem)
        {
            if (id != quartoInternagem.N_Identificador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quartoInternagem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuartoInternagemExists(quartoInternagem.N_Identificador))
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
            return View(quartoInternagem);
        }

        // GET: QuartoInternagems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quartoInternagem = await _context.QuartoInternagem
                .FirstOrDefaultAsync(m => m.N_Identificador == id);
            if (quartoInternagem == null)
            {
                return NotFound();
            }

            return View(quartoInternagem);
        }

        // POST: QuartoInternagems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quartoInternagem = await _context.QuartoInternagem.FindAsync(id);
            if (quartoInternagem != null)
            {
                _context.QuartoInternagem.Remove(quartoInternagem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuartoInternagemExists(int id)
        {
            return _context.QuartoInternagem.Any(e => e.N_Identificador == id);
        }
    }
}
