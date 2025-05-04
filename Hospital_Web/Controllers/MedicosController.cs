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
    public class MedicosController : Controller
    {
        private readonly Hospital_WebContext _context;

        public MedicosController(Hospital_WebContext context)
        {
            _context = context;
        }

        // GET: Medicos
        public async Task<IActionResult> Index()
        {
            var hospital_WebContext = _context.Medico.Include(m => m.Funcionario);
            return View(await hospital_WebContext.ToListAsync());
        }

        // GET: Medicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medico
                .Include(m => m.Funcionario)
                .FirstOrDefaultAsync(m => m.ID_Medico == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // GET: Medicos/Create
        public IActionResult Create()
        {
            ViewData["ID_Medico"] = new SelectList(_context.Funcionario, "ID", "Nome");
            return View();
        }

        // POST: Medicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Medico,Especialidade")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ID_Medico"] = new SelectList(_context.Funcionario, "ID", "Nome", medico.ID_Medico);
            return View(medico);
        }

        // GET: Medicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medico.FindAsync(id);
            if (medico == null)
            {
                return NotFound();
            }
            ViewData["ID_Medico"] = new SelectList(_context.Funcionario, "ID", "Nome", medico.ID_Medico);
            return View(medico);
        }

        // POST: Medicos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Medico,Especialidade")] Medico medico)
        {
            if (id != medico.ID_Medico)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoExists(medico.ID_Medico))
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
            ViewData["ID_Medico"] = new SelectList(_context.Funcionario, "ID", "Nome", medico.ID_Medico);
            return View(medico);
        }

        // GET: Medicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medico
                .Include(m => m.Funcionario)
                .FirstOrDefaultAsync(m => m.ID_Medico == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medico = await _context.Medico.FindAsync(id);
            if (medico != null)
            {
                _context.Medico.Remove(medico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicoExists(int id)
        {
            return _context.Medico.Any(e => e.ID_Medico == id);
        }
    }
}
