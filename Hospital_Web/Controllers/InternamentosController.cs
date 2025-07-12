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
    /// <summary>
    /// Controlador responsavel por gerir os internamentos dos utentes no hospital.
    /// Inclui funcionalidades para listar, criar, editar, visualizar e apagar internamentos.
    /// </summary>
    public class InternamentosController : Controller
    {
        private readonly Hospital_WebContext _context;

        /// <summary>
        /// Injeta o contexto da base de dados no controlador.
        /// </summary>
        public InternamentosController(Hospital_WebContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Mostra a lista de internamentos, com as respetivas relações carregadas (Consulta, Quarto, Utente).
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var hospital_WebContext = _context.Internamento
                .Include(i => i.Consulta)
                .Include(i => i.Quarto)
                .Include(i => i.Utente);

            return View(await hospital_WebContext.ToListAsync());
        }

        /// <summary>
        /// Mostra os detalhes de um internamento especifico.
        /// </summary>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var internamento = await _context.Internamento
                .Include(i => i.Consulta)
                .Include(i => i.Quarto)
                .Include(i => i.Utente)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (internamento == null)
                return NotFound();

            return View(internamento);
        }

        /// <summary>
        /// Devolve a view para criar um novo internamento.
        /// Carrega dropdowns com dados das consultas, quartos e utentes.
        /// </summary>
        public IActionResult Create()
        {
            ViewData["Consulta_Id"] = new SelectList(
                _context.Consulta.Select(c => new {
                    c.Episodio,
                    DisplayText = $"#{c.Episodio} - {c.Data:dd/MM/yyyy} - {c.Diagnostico}"
                }), "Episodio", "DisplayText");

            ViewData["Quarto_Id"] = new SelectList(
                _context.QuartosInternagem.Select(q => new {
                    ID = q.ID,
                    DisplayText = q.Denominacao
                }), "ID", "DisplayText");

            ViewData["Utente_Id"] = new SelectList(_context.Utente, "N_Processo", "NIF");

            return View();
        }

        /// <summary>
        /// Cria um novo internamento, se os dados forem validos.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DataHoraEntrada,DataHoraSaida,Utente_Id,Quarto_Id,Consulta_Id")] Internamento internamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(internamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Recarrega dropdowns em caso de erro
            ViewData["Consulta_Id"] = new SelectList(
                _context.Consulta.Select(c => new {
                    c.Episodio,
                    DisplayText = $"#{c.Episodio} - {c.Data:dd/MM/yyyy} - {c.Diagnostico}"
                }), "Episodio", "DisplayText", internamento.Consulta_Id);

            ViewData["Quarto_Id"] = new SelectList(
                _context.QuartosInternagem.Select(q => new {
                    ID = q.ID,
                    DisplayText = q.Denominacao
                }), "ID", "DisplayText", internamento.Quarto_Id);

            ViewData["Utente_Id"] = new SelectList(_context.Utente, "N_Processo", "NIF", internamento.Utente_Id);

            return View(internamento);
        }

        /// <summary>
        /// Devolve a view para editar um internamento existente.
        /// </summary>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var internamento = await _context.Internamento.FindAsync(id);
            if (internamento == null)
                return NotFound();

            ViewData["Consulta_Id"] = new SelectList(
                _context.Consulta.Select(c => new {
                    c.Episodio,
                    DisplayText = $"#{c.Episodio} - {c.Data:dd/MM/yyyy} - {c.Diagnostico}"
                }), "Episodio", "DisplayText", internamento.Consulta_Id);

            ViewData["Quarto_Id"] = new SelectList(
                _context.QuartosInternagem.Select(q => new {
                    ID = q.ID,
                    DisplayText = q.Denominacao
                }), "ID", "DisplayText", internamento.Quarto_Id);

            ViewData["Utente_Id"] = new SelectList(_context.Utente, "N_Processo", "NIF", internamento.Utente_Id);

            return View(internamento);
        }

        /// <summary>
        /// Guarda as alterações feitas a um internamento.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DataHoraEntrada,DataHoraSaida,Utente_Id,Quarto_Id,Consulta_Id")] Internamento internamento)
        {
            if (id != internamento.ID)
                return NotFound();

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
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            // Recarrega dropdowns em caso de erro
            ViewData["Consulta_Id"] = new SelectList(
                _context.Consulta.Select(c => new {
                    c.Episodio,
                    DisplayText = $"#{c.Episodio} - {c.Data:dd/MM/yyyy} - {c.Diagnostico}"
                }), "Episodio", "DisplayText", internamento.Consulta_Id);

            ViewData["Quarto_Id"] = new SelectList(
                _context.QuartosInternagem.Select(q => new {
                    ID = q.ID,
                    DisplayText = q.Denominacao
                }), "ID", "DisplayText", internamento.Quarto_Id);

            ViewData["Utente_Id"] = new SelectList(_context.Utente, "N_Processo", "NIF", internamento.Utente_Id);

            return View(internamento);
        }

        /// <summary>
        /// Mostra os detalhes de um internamento antes de apagar.
        /// </summary>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var internamento = await _context.Internamento
                .Include(i => i.Consulta)
                .Include(i => i.Quarto)
                .Include(i => i.Utente)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (internamento == null)
                return NotFound();

            return View(internamento);
        }

        /// <summary>
        /// Apaga um internamento confirmado pelo utilizador.
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var internamento = await _context.Internamento.FindAsync(id);
            if (internamento != null)
            {
                _context.Internamento.Remove(internamento);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verifica se um internamento com determinado ID existe na base de dados.
        /// </summary>
        private bool InternamentoExists(int id)
        {
            return _context.Internamento.Any(e => e.ID == id);
        }
    }
}
