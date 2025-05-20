// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Hospital_Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Hospital_Web.Data;

namespace Hospital_Web.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly Hospital_WebContext _context;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            Hospital_WebContext context)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "A categoria é obrigatória")]
            [Display(Name = "Categoria")]
            public string Categoria { get; set; }

            [Display(Name = "Especialidade")]
            public string? Especialidade { get; set; }

            [Display(Name = "Número de Ordem")]
            public string? NumeroDeOrdem { get; set; }

            [Display(Name = "Anos de Experiência")]
            public int? AnosDeExperiencia { get; set; }

            [Display(Name = "Estado clínico")]
            public string? EstadoClinico { get; set; }

            [Display(Name = "Grupo sanguíneo")]
            public string? GrupoSanguineo { get; set; }

            [Display(Name = "Alergias")]
            public string? Alergias { get; set; }

            [Display(Name = "Seguro de Saúde")]
            public string? SeguroDeSaude { get; set; }

            [Display(Name = "Médico Associado (opcional)")]
            public int? MedicoAssociadoId { get; set; }

            [Display(Name = "Turno")]
            public string? Turno { get; set; }

            [Display(Name = "Tamanho do uniforme")]
            public string? TamanhoUniforme { get; set; }

            [Display(Name = "Data de contratação")]
            [DataType(DataType.Date)]
            public DateTime? DataContratacao { get; set; }

            [Display(Name = "Certificações")]
            public string? Certificacoes { get; set; }

            [Display(Name = "Departamento")]
            public string? Departamento { get; set; }

            [Display(Name = "Função Principal")]
            public string? Funcao { get; set; }

            [Display(Name = "Data de Início de Funções")]
            [DataType(DataType.Date)]
            public DateTime? DataInicio { get; set; }



        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (Input.Categoria.ToUpper() == "MÉDICO")
                {
                    var medico = new Medico
                    {
                        Email = Input.Email,
                        Especialidade = Input.Especialidade,
                        Numero_de_ordem = Input.NumeroDeOrdem,
                        Anos_de_experiencia = Input.AnosDeExperiencia ?? 0
                    };

                    _context.Medico.Add(medico);
                    await _context.SaveChangesAsync();
                }

                else if (Input.Categoria.ToUpper() == "UTENTE")
                {
                    // Enum.TryParse para converter o grupo sanguíneo
                    bool grupoValido = Enum.TryParse<Utente.GrupoSanguineo>(Input.GrupoSanguineo?.Replace("+", "_Positivo").Replace("-", "_Negativo").Replace(" ", "_"), out var grupo);

                    var utente = new Utente
                    {
                        Email = Input.Email,
                        Estado_clinico = Input.EstadoClinico ?? "Sem estado clínico",
                        Grupo_Sanguineo = grupoValido ? grupo : Utente.GrupoSanguineo.O_Positivo,
                        Alergias = Input.Alergias,
                        Seguro_de_Saude = Input.SeguroDeSaude ?? string.Empty,
                        Medico_Associado_Id = Input.MedicoAssociadoId
                    };

                    _context.Utente.Add(utente);
                    await _context.SaveChangesAsync();
                }

                else if (Input.Categoria.ToUpper() == "EMPREGADO DE LIMPEZA")
                {
                    bool turnoValido = Enum.TryParse<FuncionarioLimpeza.Turnos>(Input.Turno, out var turno);
                    bool uniformeValido = Enum.TryParse<FuncionarioLimpeza.Uniformes>(Input.TamanhoUniforme, out var uniforme);

                    var funcionario = new FuncionarioLimpeza
                    {
                        Email = Input.Email,
                        Turno = turnoValido ? turno : FuncionarioLimpeza.Turnos.Manha,
                        Tamanho_Uniforme = uniformeValido ? uniforme : FuncionarioLimpeza.Uniformes.M,
                        Data_de_contratacao = Input.DataContratacao ?? DateTime.Now,
                        Certificacoes = Input.Certificacoes ?? string.Empty
                    };

                    _context.FuncionarioLimpeza.Add(funcionario);
                    await _context.SaveChangesAsync();
                }

                else if (Input.Categoria.ToUpper() == "ADMINISTRADOR")
                {
                    var admin = new Administrador
                    {
                        Email = Input.Email,
                        Departamento = Input.Departamento,
                        Funcao = Input.Funcao,
                        Data_de_Nascimento = DateTime.Now, // preencher conforme o teu modelo base
                        DataInicio = Input.DataInicio ?? DateTime.Now
                    };

                    _context.Administrador.Add(admin);
                    await _context.SaveChangesAsync();
                }







                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    // Atribuir a Role (categoria)
                    await _userManager.AddToRoleAsync(user, Input.Categoria);

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
