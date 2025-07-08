using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital_Web.Data;
using Hospital_Web.Models;
using Microsoft.AspNetCore.Identity;
using Hospital_Web.Services;


namespace Hospital_Web.Controllers
{
    public class FuncionarioLimpezasController : Controller
    {
        private readonly Hospital_WebContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public FuncionarioLimpezasController(
            Hospital_WebContext context,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
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
        public async Task<IActionResult> Create([Bind("Nome,Idade,DatadeNascimento,Morada,Telemovel,TelemovelAlt,Email,NIF,Cod_Postal,Localidade,Tamanho_Uniforme,Data_de_contratacao,Turno,Certificacoes,N_Processo")] FuncionarioLimpeza funcionarioLimpeza)
        {
            if (!ModelState.IsValid)
                return View(funcionarioLimpeza);

            var user = new ApplicationUser
            {
                UserName = funcionarioLimpeza.Email,
                Email = funcionarioLimpeza.Email,
                DeveAlterarSenha = true
            };

            string senhaTemporaria = "Hosp@" + Guid.NewGuid().ToString("N").Substring(0, 6);
            var result = await _userManager.CreateAsync(user, senhaTemporaria);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(funcionarioLimpeza);
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.FuncionarioLimpeza.Add(funcionarioLimpeza);
                await _context.SaveChangesAsync();

                user.FuncionarioLimpezaId = funcionarioLimpeza.N_Processo;
                await _userManager.UpdateAsync(user);

                await transaction.CommitAsync();

                await _emailSender.SendEmailAsync(
                    funcionarioLimpeza.Email,
                    "Bem-vindo ao Portal do Hospital",
                    $@"
<p>Olá {funcionarioLimpeza.Nome},</p>
<p>Seja bem-vindo ao nosso sistema do hospital. A sua conta foi criada com sucesso.</p>
<p><a href='https://localhost:7140/Identity/Account/Login'>Entrar no sistema Hospital</a></p>
<p><strong>Credenciais de acesso:</strong><br>
Email: {funcionarioLimpeza.Email}<br>
Senha: {senhaTemporaria}</p>
<p>Será solicitado a alterar a senha no primeiro login.</p>");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                await _userManager.DeleteAsync(user);
                await transaction.RollbackAsync();
                ModelState.AddModelError("", "Erro ao criar o funcionário. Nenhum dado foi gravado.");
                return View(funcionarioLimpeza);
            }
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
        public async Task<IActionResult> Edit(int id, [Bind("Turno,Tamanho_Uniforme,Data_de_contratacao,Certificacoes,N_Processo,Nome,Idade,DataDeNascimento,Morada,Telemovel,TelemovelAlt,Email,NIF,Cod_Postal,Localidade,Grupo_Sanguineo")] FuncionarioLimpeza funcionarioLimpeza)
        {
            if (id != funcionarioLimpeza.N_Processo)
                return NotFound();

            if (!ModelState.IsValid)
                return View(funcionarioLimpeza);

            try
            {
                _context.Update(funcionarioLimpeza);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioLimpezaExists(funcionarioLimpeza.N_Processo))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
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
            var users = await _context.Users
             .Where(u => u.FuncionarioLimpezaId == id)
             .ToListAsync();

            // Remove each user
            foreach (var user in users)
            {
                _context.Users.Remove(user);
            }

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
