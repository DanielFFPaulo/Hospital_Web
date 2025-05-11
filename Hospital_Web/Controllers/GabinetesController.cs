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
    public class GabinetesController : Controller
    {
        private readonly Hospital_WebContext _context;

        public GabinetesController(Hospital_WebContext context)
        {
            _context = context;
        }

        // GET: Gabinetes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gabinete.ToListAsync());
        }

        // GET: Gabinetes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gabinete = await _context.Gabinete
                .FirstOrDefaultAsync(m => m.ID == id);
            if (gabinete == null)
            {
                return NotFound();
            }

            return View(gabinete);
        }

        // GET: Gabinetes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gabinetes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Gabinetes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gabinete = await _context.Gabinete.FindAsync(id);
            if (gabinete == null)
            {
                return NotFound();
            }
            return View(gabinete);
        }

        // POST: Gabinetes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Descricao,Equipamento,Disponivel,ID,Bloco,Andar,Numero")] Gabinete gabinete)
        {
            if (id != gabinete.ID)
            {
                return NotFound();
            }

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
            return View(gabinete);
        }

        // GET: Gabinetes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gabinete = await _context.Gabinete
                .FirstOrDefaultAsync(m => m.ID == id);
            if (gabinete == null)
            {
                return NotFound();
            }

            return View(gabinete);
        }

        // POST: Gabinetes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gabinete = await _context.Gabinete.FindAsync(id);
            if (gabinete != null)
            {
                _context.Gabinete.Remove(gabinete);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GabineteExists(int id)
        {
            return _context.Gabinete.Any(e => e.ID == id);
        }
    }
}
