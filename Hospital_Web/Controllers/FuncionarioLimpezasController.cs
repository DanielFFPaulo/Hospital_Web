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


/// <summary>
/// Controlador responsável pela gestão de funcionários da limpeza no sistema hospitalar.
/// Inclui criação de conta de utilizador, envio de email de boas-vindas e operações CRUD.
/// </summary>
namespace Hospital_Web.Controllers
{
    public class FuncionarioLimpezasController : Controller
    {
        /// <summary>
        /// Contexto da base de dados.
        /// </summary>
        private readonly Hospital_WebContext _context;

        /// <summary>
        /// Gerenciador de utilizadores (Identity).
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Serviço de envio de emails.
        /// </summary>
        private readonly IEmailSender _emailSender;

        /// <summary>
        /// Construtor do controlador. Injeta as dependências necessárias.
        /// </summary>
        public FuncionarioLimpezasController(
            Hospital_WebContext context,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        /// <summary>
        /// Mostra a lista de todos os funcionários da limpeza.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            return View(await _context.FuncionarioLimpeza.ToListAsync());
        }

        /// <summary>
        /// Mostra os detalhes de um funcionário da limpeza.
        /// </summary>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var funcionarioLimpeza = await _context.FuncionarioLimpeza
                .FirstOrDefaultAsync(m => m.N_Processo == id);
            if (funcionarioLimpeza == null)
                return NotFound();

            return View(funcionarioLimpeza);
        }

        /// <summary>
        /// Mostra o formulário de criação de um novo funcionário da limpeza.
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Regista um novo funcionário da limpeza e cria conta de utilizador associada.
        /// Envia email com credenciais temporárias.
        /// </summary>
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

                // Atribuir Role "FuncionarioLimpeza"
                await _userManager.AddToRoleAsync(user, "FuncionarioLimpeza");

                await transaction.CommitAsync();

                await _emailSender.SendEmailAsync(
                    funcionarioLimpeza.Email,
                    "Bem-vindo ao Portal do Hospital",
                    $@"
<p>Ola {funcionarioLimpeza.Nome},</p>
<p>Seja bem-vindo ao nosso sistema do hospital. A sua conta foi criada com sucesso.</p>
<p><a href='https://localhost:7140/Identity/Account/Login'>Entrar no sistema Hospital</a></p>
<p><strong>Credenciais de acesso:</strong><br>
Email: {funcionarioLimpeza.Email}<br>
Senha: {senhaTemporaria}</p>
<p>Sera solicitado a alterar a senha no primeiro login.</p>");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                await _userManager.DeleteAsync(user);
                await transaction.RollbackAsync();
                ModelState.AddModelError("", "Erro ao criar o funcionario. Nenhum dado foi gravado.");
                return View(funcionarioLimpeza);
            }
        }

        /// <summary>
        /// Mostra o formulário para editar dados de um funcionário da limpeza.
        /// </summary>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var funcionarioLimpeza = await _context.FuncionarioLimpeza.FindAsync(id);
            if (funcionarioLimpeza == null)
                return NotFound();

            return View(funcionarioLimpeza);
        }

        /// <summary>
        /// Atualiza os dados de um funcionário da limpeza.
        /// </summary>
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

        /// <summary>
        /// Mostra o formulário de confirmação para eliminar um funcionário da limpeza.
        /// </summary>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var funcionarioLimpeza = await _context.FuncionarioLimpeza
                .FirstOrDefaultAsync(m => m.N_Processo == id);
            if (funcionarioLimpeza == null)
                return NotFound();

            return View(funcionarioLimpeza);
        }

        /// <summary>
        /// Elimina definitivamente um funcionário da limpeza e os utilizadores associados.
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users
                .Where(u => u.FuncionarioLimpezaId == id)
                .ToListAsync();

            foreach (var user in users)
                _context.Users.Remove(user);

            var funcionarioLimpeza = await _context.FuncionarioLimpeza.FindAsync(id);
            if (funcionarioLimpeza != null)
                _context.FuncionarioLimpeza.Remove(funcionarioLimpeza);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verifica se existe um funcionário da limpeza com o N_Processo fornecido.
        /// </summary>
        private bool FuncionarioLimpezaExists(int id)
        {
            return _context.FuncionarioLimpeza.Any(e => e.N_Processo == id);
        }
    }
}
