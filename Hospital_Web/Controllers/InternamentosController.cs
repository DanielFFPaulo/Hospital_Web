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
    public class InternamentosController : Controller
    {
        private readonly Hospital_WebContext _context;

        public InternamentosController(Hospital_WebContext context)
        {
            _context = context;
        }

        // GET: Internamentos
        public async Task<IActionResult> Index()
        {
            var hospital_WebContext = _context.Internamento.Include(i => i.Consulta).Include(i => i.Quarto).Include(i => i.Utente);
            return View(await hospital_WebContext.ToListAsync());
        }

        // GET: Internamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var internamento = await _context.Internamento
                .Include(i => i.Consulta)
                .Include(i => i.Quarto)
                .Include(i => i.Utente)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (internamento == null)
            {
                return NotFound();
            }

            return View(internamento);
        }

        // GET: Internamentos/Create
        public IActionResult Create()
        {
            ViewData["Consulta_Id"] = new SelectList(
                _context.Consulta
                    .Select(c => new {
                        c.Episodio,
                        DisplayText = $"#{c.Episodio} - {c.Data:dd/MM/yyyy} - {c.Diagnostico}"
                    }),
                "Episodio",
                "DisplayText"
            );
            ViewData["Quarto_Id"] = new SelectList(_context.QuartosInternagem.Select(q => new {ID = q.ID, DisplayText = q.Bloco + q.Andar.ToString() + q.Numero.ToString("D2")}), "ID", "DisplayText");
            ViewData["Utente_Id"] = new SelectList(_context.Utente, "N_Processo", "NIF");
            return View();
        }

        // POST: Internamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Data_Hora_Entrada,Data_Hora_Saida,Utente_Id,Quarto_Id,Consulta_Id")] Internamento internamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(internamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Consulta_Id"] = new SelectList(
                _context.Consulta
                    .Select(c => new {
                        c.Episodio,
                        DisplayText = $"#{c.Episodio} - {c.Data:dd/MM/yyyy} - {c.Diagnostico}"
                    }),
                "Episodio",
                "DisplayText",
                internamento.Consulta_Id
            );

            ViewData["Quarto_Id"] = new SelectList(_context.QuartosInternagem.Select(q => new { ID = q.ID, DisplayText = q.Bloco + q.Andar.ToString() + q.Numero.ToString("D2") }), "ID", "DisplayText", internamento.Quarto_Id);
            ViewData["Utente_Id"] = new SelectList(_context.Utente, "N_Processo", "NIF", internamento.Utente_Id);
            return View(internamento);
        }

        // GET: Internamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var internamento = await _context.Internamento.FindAsync(id);
            if (internamento == null)
            {
                return NotFound();
            }
            ViewData["Consulta_Id"] = new SelectList(
                _context.Consulta
                    .Select(c => new {
                        c.Episodio,
                        DisplayText = $"#{c.Episodio} - {c.Data:dd/MM/yyyy} - {c.Diagnostico}"
                    }),
                "Episodio",
                "DisplayText",
                internamento.Consulta_Id
            );
            ViewData["Quarto_Id"] = new SelectList(_context.QuartosInternagem.Select(q => new { ID = q.ID, DisplayText = q.Bloco + q.Andar.ToString() + q.Numero.ToString("D2") }), "ID", "DisplayText", internamento.Quarto_Id);
            ViewData["Utente_Id"] = new SelectList(_context.Utente, "N_Processo", "NIF", internamento.Utente_Id);
            return View(internamento);
        }

        // POST: Internamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Data_Hora_Entrada,Data_Hora_Saida,Utente_Id,Quarto_Id,Consulta_Id")] Internamento internamento)
        {
            if (id != internamento.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(internamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InternamentoExists(internamento.ID))
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
            ViewData["Consulta_Id"] = new SelectList(
                _context.Consulta
                    .Select(c => new {
                        c.Episodio,
                        DisplayText = $"#{c.Episodio} - {c.Data:dd/MM/yyyy} - {c.Diagnostico}"
                    }),
                "Episodio",
                "DisplayText",
                internamento.Consulta_Id
            );
            ViewData["Quarto_Id"] = new SelectList(_context.QuartosInternagem.Select(q => new { ID = q.ID, DisplayText = q.Bloco + q.Andar.ToString() + q.Numero.ToString("D2") }), "ID", "DisplayText", internamento.Quarto_Id);
            ViewData["Utente_Id"] = new SelectList(_context.Utente, "N_Processo", "NIF", internamento.Utente_Id);
            return View(internamento);
        }

        // GET: Internamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var internamento = await _context.Internamento
                .Include(i => i.Consulta)
                .Include(i => i.Quarto)
                .Include(i => i.Utente)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (internamento == null)
            {
                return NotFound();
            }

            return View(internamento);
        }

        // POST: Internamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var internamento = await _context.Internamento.FindAsync(id);
            if (internamento != null)
            {
                _context.Internamento.Remove(internamento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InternamentoExists(int id)
        {
            return _context.Internamento.Any(e => e.ID == id);
        }
    }
}
