// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Hospital_Web.Models;
using Hospital_Web.Data;
using Microsoft.EntityFrameworkCore;



/// <summary>
/// Define o namespace da página de login dentro da área Identity.
/// </summary>
namespace Hospital_Web.Areas.Identity.Pages.Account
{
    /// <summary>
    /// Modelo da página Razor responsável pela autenticação dos utilizadores.
    /// </summary>
    public class LoginModel : PageModel
    {
        /// <summary>
        /// Gerencia operações de login como autenticação com password e 2FA.
        /// </summary>
        private readonly SignInManager<ApplicationUser> _signInManager;

        /// <summary>
        /// Permite obter e atualizar informações sobre utilizadores.
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Regista mensagens de log, como login bem-sucedido ou falhas.
        /// </summary>
        private readonly ILogger<LoginModel> _logger;

        /// <summary>
        /// Contexto da base de dados da aplicação para acesso a entidades como Utente.
        /// </summary>
        private readonly Hospital_WebContext _context;

        /// <summary>
        /// Construtor que injeta as dependências necessárias.
        /// </summary>
        public LoginModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<LoginModel> logger,
            Hospital_WebContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Modelo de entrada com email, password e lembrar login.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// Lista de esquemas de autenticação externa disponíveis (ex: Google, Facebook).
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        /// URL para redirecionamento após login bem-sucedido.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Mensagem de erro persistente entre pedidos (ex: após redirecionamento).
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Classe aninhada que representa os campos do formulário de login.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Email do utilizador que está a tentar fazer login.
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            /// <summary>
            /// Password do utilizador.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            /// <summary>
            /// Indica se o utilizador deseja manter a sessão iniciada.
            /// </summary>
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        /// <summary>
        /// Executado quando a página é carregada via GET.
        /// Limpa cookies externos e inicializa dados como ReturnUrl e logins externos.
        /// </summary>
        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Limpa qualquer cookie de autenticação externa anterior
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        /// <summary>
        /// Executado quando o formulário de login é submetido (POST).
        /// Valida credenciais e trata lógicas como obrigatoriedade de alterar senha, 2FA e associação a Utente.
        /// </summary>
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");

                    var user = await _userManager.FindByEmailAsync(Input.Email);

                    // Associa automaticamente o utilizador à entidade Utente, se ainda não estiver associado
                    if (user != null && user.UtenteId == null)
                    {
                        var utente = await _context.Utente.FirstOrDefaultAsync(u => u.Email == user.Email);
                        if (utente != null)
                        {
                            user.UtenteId = utente.N_Processo;
                            await _userManager.UpdateAsync(user);
                            await _userManager.AddToRoleAsync(user, "Utente");
                            await _signInManager.RefreshSignInAsync(user);
                        }
                    }

                    // Se o utilizador estiver marcado para alterar a senha, redireciona para isso
                    if (user != null && user.DeveAlterarSenha)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: Input.RememberMe);
                        return RedirectToPage("/Account/ForceChangePassword");
                    }

                    return RedirectToAction("Index", "Home");
                }

                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }

            // Se a validação do modelo falhar, volta a mostrar a página
            return Page();
        }
    }
}




