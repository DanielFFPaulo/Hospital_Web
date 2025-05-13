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
    public class FuncionarioLimpezasController : Controller
    {
        private readonly Hospital_WebContext _context;

        public FuncionarioLimpezasController(Hospital_WebContext context)
        {
            _context = context;
        }

        // GET: FuncionarioLimpezas
        public async Task<IActionResult> Index()
        {
            return View(await _context.FuncionarioLimpeza.ToListAsync());
        }

        // GET: FuncionarioLimpezas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionarioLimpeza = await _context.FuncionarioLimpeza
                .FirstOrDefaultAsync(m => m.N_Processo == id);
            if (funcionarioLimpeza == null)
            {
                return NotFound();
            }

            return View(funcionarioLimpeza);
        }

        // GET: FuncionarioLimpezas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FuncionarioLimpezas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Turno,Tamanho_Uniforme,Data_de_contratacao,Certificacoes,N_Processo,Nome,Idade,Data_de_Nascimento,Morada,Telemovel,TelemovelAlt,Email,NIF,Cod_Postal,Localidade")] FuncionarioLimpeza funcionarioLimpeza)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcionarioLimpeza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(funcionarioLimpeza);
        }

        // GET: FuncionarioLimpezas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionarioLimpeza = await _context.FuncionarioLimpeza.FindAsync(id);
            if (funcionarioLimpeza == null)
            {
                return NotFound();
            }
            return View(funcionarioLimpeza);
        }

        // POST: FuncionarioLimpezas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Turno,Tamanho_Uniforme,Data_de_contratacao,Certificacoes,N_Processo,Nome,Idade,Data_de_Nascimento,Morada,Telemovel,TelemovelAlt,Email,NIF,Cod_Postal,Localidade")] FuncionarioLimpeza funcionarioLimpeza)
        {
            if (id != funcionarioLimpeza.N_Processo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionarioLimpeza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioLimpezaExists(funcionarioLimpeza.N_Processo))
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
            return View(funcionarioLimpeza);
        }

        // GET: FuncionarioLimpezas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionarioLimpeza = await _context.FuncionarioLimpeza
                .FirstOrDefaultAsync(m => m.N_Processo == id);
            if (funcionarioLimpeza == null)
            {
                return NotFound();
            }

            return View(funcionarioLimpeza);
        }

        // POST: FuncionarioLimpezas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionarioLimpeza = await _context.FuncionarioLimpeza.FindAsync(id);
            if (funcionarioLimpeza != null)
            {
                _context.FuncionarioLimpeza.Remove(funcionarioLimpeza);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioLimpezaExists(int id)
        {
            return _context.FuncionarioLimpeza.Any(e => e.N_Processo == id);
        }
    }
}
