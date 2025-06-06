﻿using System;
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
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Utente == null)
            {
                return Problem("Entity set 'Hospital_WebContext.Utente'  is null.");
            }

            var utentes = from u in _context.Utente
                          select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                utentes = utentes.Where(u => u.Nome!.ToUpper().Contains(searchString.ToUpper()));
            }

            return View(await utentes.ToListAsync());
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
            ViewData["Medico_Associado_Id"] = new SelectList(
                _context.Medico
                    .Select(m => new {
                        N_Processo = m.N_Processo,
                        DisplayValue = "OM" + m.Numero_de_ordem + " - " + m.Nome
                    }),
                "N_Processo",
                "DisplayValue"
            );

            return View();
        }

        // POST: Utentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Estado_clinico,Alergias,Seguro_de_Saude,Data_de_Registo,Medico_Associado_Id,N_Processo,Nome,DataDeNascimento,sexo,Morada,Grupo_Sanguineo,Telemovel,TelemovelAlt,Email,NIF,Cod_Postal,Localidade")] Utente utente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(utente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Medico_Associado_Id"] = new SelectList(
                _context.Medico
                    .Select(m => new {
                        N_Processo = m.N_Processo,
                        DisplayValue = "OM" + m.Numero_de_ordem + " - " + m.Nome
                    }),
                "N_Processo",
                "DisplayValue",
                utente.Medico_Associado_Id
            );

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
            ViewData["Medico_Associado_Id"] = new SelectList(_context.Medico.Select(m => new { N_Processo = m.N_Processo, DisplayValue = "OM" + m.Numero_de_ordem + " - " + m.Nome }), "N_Processo", "DisplayValue");
            return View(utente);
        }

        // POST: Utentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Estado_clinico,Alergias,Seguro_de_Saude,Data_de_Registo,Medico_Associado_Id,N_Processo,Nome,DataDeNascimento,sexo,Morada,Grupo_Sanguineo,Telemovel,TelemovelAlt,Email,NIF,Cod_Postal,Localidade")] Utente utente)
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
            ViewData["Medico_Associado_Id"] = new SelectList(_context.Medico.Select(m => new { N_Processo = m.N_Processo, DisplayValue = "OM-" + m.Numero_de_ordem + " - " + m.Nome }), "N_Processo", "DisplayValue");
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
