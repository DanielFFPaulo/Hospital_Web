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
    public class QuartosInternagemsController : Controller
    {
        private readonly Hospital_WebContext _context;

        public QuartosInternagemsController(Hospital_WebContext context)
        {
            _context = context;
        }

        // GET: QuartosInternagems
        public async Task<IActionResult> Index()
        {
            return View(await _context.QuartosInternagem.ToListAsync());
        }

        // GET: QuartosInternagems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quartosInternagem = await _context.QuartosInternagem
                .FirstOrDefaultAsync(m => m.ID == id);
            if (quartosInternagem == null)
            {
                return NotFound();
            }

            return View(quartosInternagem);
        }

        // GET: QuartosInternagems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QuartosInternagems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao,Tipo,ID,Bloco,Andar,Numero")] QuartosInternagem quartosInternagem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quartosInternagem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quartosInternagem);
        }

        // GET: QuartosInternagems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quartosInternagem = await _context.QuartosInternagem.FindAsync(id);
            if (quartosInternagem == null)
            {
                return NotFound();
            }
            return View(quartosInternagem);
        }

        // POST: QuartosInternagems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Descricao,Tipo,ID,Bloco,Andar,Numero")] QuartosInternagem quartosInternagem)
        {
            if (id != quartosInternagem.ID)
            {
                return NotFound();
            }

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
            return View(quartosInternagem);
        }

        // GET: QuartosInternagems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quartosInternagem = await _context.QuartosInternagem
                .FirstOrDefaultAsync(m => m.ID == id);
            if (quartosInternagem == null)
            {
                return NotFound();
            }

            return View(quartosInternagem);
        }

        // POST: QuartosInternagems/Delete/5
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

        private bool QuartosInternagemExists(int id)
        {
            return _context.QuartosInternagem.Any(e => e.ID == id);
        }
    }
}
