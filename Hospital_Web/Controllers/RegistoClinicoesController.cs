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
    public class RegistoClinicoesController : Controller
    {
        private readonly Hospital_WebContext _context;

        public RegistoClinicoesController(Hospital_WebContext context)
        {
            _context = context;
        }

        // GET: RegistoClinicoes
        public async Task<IActionResult> Index()
        {
            var hospital_WebContext = _context.RegistoClinico.Include(r => r.ConsultaNavigation).Include(r => r.MedicoNavigation).Include(r => r.UtenteNavigation);
            return View(await hospital_WebContext.ToListAsync());
        }

        // GET: RegistoClinicoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registoClinico = await _context.RegistoClinico
                .Include(r => r.ConsultaNavigation)
                .Include(r => r.MedicoNavigation)
                .Include(r => r.UtenteNavigation)
                .FirstOrDefaultAsync(m => m.Episodio == id);
            if (registoClinico == null)
            {
                return NotFound();
            }

            return View(registoClinico);
        }

        // GET: RegistoClinicoes/Create
        public IActionResult Create()
        {
            ViewData["Episodio"] = new SelectList(_context.Consulta, "Episodio", "Episodio");
            ViewData["Medico"] = new SelectList(_context.Medico, "ID_Medico", "ID_Medico");
            ViewData["Utente"] = new SelectList(_context.Utente, "N_Processo", "Nome");
            return View();
        }

        // POST: RegistoClinicoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Utente,Medico,Episodio,Data,Diagnostico,Tratamento,Observacoes")] RegistoClinico registoClinico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registoClinico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Episodio"] = new SelectList(_context.Consulta, "Episodio", "Episodio", registoClinico.Episodio);
            ViewData["Medico"] = new SelectList(_context.Medico, "ID_Medico", "ID_Medico", registoClinico.Medico);
            ViewData["Utente"] = new SelectList(_context.Utente, "N_Processo", "Nome", registoClinico.Utente);
            return View(registoClinico);
        }

        // GET: RegistoClinicoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registoClinico = await _context.RegistoClinico.FindAsync(id);
            if (registoClinico == null)
            {
                return NotFound();
            }
            ViewData["Episodio"] = new SelectList(_context.Consulta, "Episodio", "Episodio", registoClinico.Episodio);
            ViewData["Medico"] = new SelectList(_context.Medico, "ID_Medico", "ID_Medico", registoClinico.Medico);
            ViewData["Utente"] = new SelectList(_context.Utente, "N_Processo", "Nome", registoClinico.Utente);
            return View(registoClinico);
        }

        // POST: RegistoClinicoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Utente,Medico,Episodio,Data,Diagnostico,Tratamento,Observacoes")] RegistoClinico registoClinico)
        {
            if (id != registoClinico.Episodio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registoClinico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistoClinicoExists(registoClinico.Episodio))
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
            ViewData["Episodio"] = new SelectList(_context.Consulta, "Episodio", "Episodio", registoClinico.Episodio);
            ViewData["Medico"] = new SelectList(_context.Medico, "ID_Medico", "ID_Medico", registoClinico.Medico);
            ViewData["Utente"] = new SelectList(_context.Utente, "N_Processo", "Nome", registoClinico.Utente);
            return View(registoClinico);
        }

        // GET: RegistoClinicoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registoClinico = await _context.RegistoClinico
                .Include(r => r.ConsultaNavigation)
                .Include(r => r.MedicoNavigation)
                .Include(r => r.UtenteNavigation)
                .FirstOrDefaultAsync(m => m.Episodio == id);
            if (registoClinico == null)
            {
                return NotFound();
            }

            return View(registoClinico);
        }

        // POST: RegistoClinicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registoClinico = await _context.RegistoClinico.FindAsync(id);
            if (registoClinico != null)
            {
                _context.RegistoClinico.Remove(registoClinico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistoClinicoExists(int id)
        {
            return _context.RegistoClinico.Any(e => e.Episodio == id);
        }
    }
}
