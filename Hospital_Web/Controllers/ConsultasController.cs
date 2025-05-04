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
    public class ConsultasController : Controller
    {
        private readonly Hospital_WebContext _context;

        public ConsultasController(Hospital_WebContext context)
        {
            _context = context;
        }

        // GET: Consultas
        public async Task<IActionResult> Index()
        {
            var hospital_WebContext = _context.Consulta.Include(c => c.GabineteNavigation).Include(c => c.Medico).Include(c => c.Utente);
            return View(await hospital_WebContext.ToListAsync());
        }

        // GET: Consultas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consulta
                .Include(c => c.GabineteNavigation)
                .Include(c => c.Medico)
                .Include(c => c.Utente)
                .FirstOrDefaultAsync(m => m.Episodio == id);
            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        // GET: Consultas/Create
        public IActionResult Create()
        {
            ViewData["Gabinete"] = new SelectList(_context.Set<Gabinete>(), "N_Identificador", "N_Identificador");
            ViewData["ID_Medico"] = new SelectList(_context.Set<Medico>(), "ID_Medico", "ID_Medico");
            ViewData["ID_Utente"] = new SelectList(_context.Utente, "N_Processo", "Nome");
            return View();
        }

        // POST: Consultas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Episodio,Data,Hora,Descricao,ID_Medico,ID_Utente,Gabinete")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consulta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Gabinete"] = new SelectList(_context.Set<Gabinete>(), "N_Identificador", "N_Identificador", consulta.Gabinete);
            ViewData["ID_Medico"] = new SelectList(_context.Set<Medico>(), "ID_Medico", "ID_Medico", consulta.ID_Medico);
            ViewData["ID_Utente"] = new SelectList(_context.Utente, "N_Processo", "Nome", consulta.ID_Utente);
            return View(consulta);
        }

        // GET: Consultas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consulta.FindAsync(id);
            if (consulta == null)
            {
                return NotFound();
            }
            ViewData["Gabinete"] = new SelectList(_context.Set<Gabinete>(), "N_Identificador", "N_Identificador", consulta.Gabinete);
            ViewData["ID_Medico"] = new SelectList(_context.Set<Medico>(), "ID_Medico", "ID_Medico", consulta.ID_Medico);
            ViewData["ID_Utente"] = new SelectList(_context.Utente, "N_Processo", "Nome", consulta.ID_Utente);
            return View(consulta);
        }

        // POST: Consultas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Episodio,Data,Hora,Descricao,ID_Medico,ID_Utente,Gabinete")] Consulta consulta)
        {
            if (id != consulta.Episodio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consulta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultaExists(consulta.Episodio))
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
            ViewData["Gabinete"] = new SelectList(_context.Set<Gabinete>(), "N_Identificador", "N_Identificador", consulta.Gabinete);
            ViewData["ID_Medico"] = new SelectList(_context.Set<Medico>(), "ID_Medico", "ID_Medico", consulta.ID_Medico);
            ViewData["ID_Utente"] = new SelectList(_context.Utente, "N_Processo", "Nome", consulta.ID_Utente);
            return View(consulta);
        }

        // GET: Consultas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consulta
                .Include(c => c.GabineteNavigation)
                .Include(c => c.Medico)
                .Include(c => c.Utente)
                .FirstOrDefaultAsync(m => m.Episodio == id);
            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        // POST: Consultas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consulta = await _context.Consulta.FindAsync(id);
            if (consulta != null)
            {
                _context.Consulta.Remove(consulta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultaExists(int id)
        {
            return _context.Consulta.Any(e => e.Episodio == id);
        }
    }
}
