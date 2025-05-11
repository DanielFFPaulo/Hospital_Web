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
    public class UtentesController : Controller
    {
        private readonly Hospital_WebContext _context;

        public UtentesController(Hospital_WebContext context)
        {
            _context = context;
        }

        // GET: Utentes
        public async Task<IActionResult> Index()
        {
            var hospital_WebContext = _context.Utente.Include(u => u.MedicoAssociado);
            return View(await hospital_WebContext.ToListAsync());
        }

        // GET: Utentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utente = await _context.Utente
                .Include(u => u.MedicoAssociado)
                .FirstOrDefaultAsync(m => m.N_Processo == id);
            if (utente == null)
            {
                return NotFound();
            }

            return View(utente);
        }

        // GET: Utentes/Create
        public IActionResult Create()
        {
            ViewData["Medico_Associado_Id"] = new SelectList(_context.Medico, "N_Processo", "Nome");
            return View();
        }

        // POST: Utentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Estado_clinico,Grupo_Sanguineo,Alergias,Seguro_de_Saude,Data_de_Registo,Medico_Associado_Id,N_Processo,Nome,Idade,Data_de_Nascimento,Morada,Telefone1,Telefone2,Email,NIF,Cod_postal,Localidade")] Utente utente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(utente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Medico_Associado_Id"] = new SelectList(_context.Medico, "N_Processo", "Nome", utente.Medico_Associado_Id);
            return View(utente);
        }

        // GET: Utentes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utente = await _context.Utente.FindAsync(id);
            if (utente == null)
            {
                return NotFound();
            }
            ViewData["Medico_Associado_Id"] = new SelectList(_context.Medico, "N_Processo", "Nome", utente.Medico_Associado_Id);
            return View(utente);
        }

        // POST: Utentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Estado_clinico,Grupo_Sanguineo,Alergias,Seguro_de_Saude,Data_de_Registo,Medico_Associado_Id,N_Processo,Nome,Idade,Data_de_Nascimento,Morada,Telefone1,Telefone2,Email,NIF,Cod_postal,Localidade")] Utente utente)
        {
            if (id != utente.N_Processo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtenteExists(utente.N_Processo))
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
            ViewData["Medico_Associado_Id"] = new SelectList(_context.Medico, "N_Processo", "Nome", utente.Medico_Associado_Id);
            return View(utente);
        }

        // GET: Utentes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utente = await _context.Utente
                .Include(u => u.MedicoAssociado)
                .FirstOrDefaultAsync(m => m.N_Processo == id);
            if (utente == null)
            {
                return NotFound();
            }

            return View(utente);
        }

        // POST: Utentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utente = await _context.Utente.FindAsync(id);
            if (utente != null)
            {
                _context.Utente.Remove(utente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtenteExists(int id)
        {
            return _context.Utente.Any(e => e.N_Processo == id);
        }
    }
}
