﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital_Web.Data;
using Hospital_Web.Models;
using Hospital_Web.Services;
using Microsoft.AspNetCore.Identity;

namespace Hospital_Web.Controllers
{
    public class PessoasController : Controller
    {
        private readonly Hospital_WebContext _context;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;

        public PessoasController(
          Hospital_WebContext context,
          IEmailSender emailSender,
          UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _emailSender = emailSender;
            _userManager = userManager;
        }

        // GET: Pessoas
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Pessoa == null)
            {
                return Problem("Entity set 'Hospital_WebContext.Pessoa'  is null.");
            }

            var pessoas = from p in _context.Pessoa
                         select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                pessoas = pessoas.Where(s => s.Nome!.ToUpper().Contains(searchString.ToUpper()));
            }

            return View(await pessoas.ToListAsync());
        }

        // GET: Pessoas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var pessoa = await _context.Pessoa
                .FirstOrDefaultAsync(m => m.N_Processo == id);
            if (pessoa == null)
                return NotFound();

            return View(pessoa);
        }

        // GET: Pessoas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pessoas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("N_Processo,Nome,DataDeNascimento,sexo,Morada,Grupo_Sanguineo,Telemovel,TelemovelAlt,Email,NIF,Cod_Postal,Localidade")] Pessoa pessoa)
        {
            if (!ModelState.IsValid)
                return View(pessoa);

            // Criar o utilizador primeiro
            var user = new ApplicationUser
            {
                UserName = pessoa.Email,
                Email = pessoa.Email,
                DeveAlterarSenha = true
            };

            string senhaTemporaria = "Hosp@" + Guid.NewGuid().ToString("N").Substring(0, 6);
            var result = await _userManager.CreateAsync(user, senhaTemporaria);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(pessoa);
            }

            // Se chegou aqui, o utilizador foi criado. Inicia transação para garantir integridade.
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Pessoa.Add(pessoa);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                await _emailSender.SendEmailAsync(
                    pessoa.Email,
                    "Bem-vindo ao Portal do Hospital",
                    $@"
            <p>Olá {pessoa.Nome},</p>
            <p>Seja bem-vindo ao nosso sistema do hospital. A sua conta foi criada com sucesso.</p>
            <p><strong>Credenciais de acesso:</strong><br>
            Email: {pessoa.Email}<br>
            Senha: {senhaTemporaria}</p>
            <p>Por favor, altere a sua senha após o primeiro login.</p>");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Se der erro ao criar Pessoa, remove o utilizador e faz rollback
                await _userManager.DeleteAsync(user);
                await transaction.RollbackAsync();

                ModelState.AddModelError("", "Erro ao criar a pessoa. Nenhum dado foi gravado.");
                return View(pessoa);
            }
        }


        // GET: Pessoas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var pessoa = await _context.Pessoa.FindAsync(id);
            if (pessoa == null)
                return NotFound();

            return View(pessoa);
       }
        // POST: Pessoas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("N_Processo,Nome,DataDeNascimento,sexo,Morada,Grupo_Sanguineo,Telemovel,TelemovelAlt,Email,NIF,Cod_Postal,Localidade")] Pessoa pessoa)
        {
            if (id != pessoa.N_Processo)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pessoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoa.N_Processo))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(pessoa);
      


        // GET: Pessoas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var pessoa = await _context.Pessoa
                .FirstOrDefaultAsync(m => m.N_Processo == id);
            if (pessoa == null)
                return NotFound();

            return View(pessoa);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoa = await _context.Pessoa.FindAsync(id);

            if (pessoa != null)
            {
                // 🧹 Apagar também o utilizador do Identity (caso exista)
                var user = await _userManager.FindByEmailAsync(pessoa.Email);
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                }

                _context.Pessoa.Remove(pessoa);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
