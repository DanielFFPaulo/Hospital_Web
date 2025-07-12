using System.ComponentModel.DataAnnotations;
using System.Text;
using Hospital_Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

/// <summary>
/// Define o namespace da página de redefinição de password dentro da área Identity.
/// </summary>
namespace Hospital_Web.Areas.Identity.Pages.Account
{
    /// <summary>
    /// Página responsável por permitir ao utilizador redefinir a sua password a partir de um código enviado por email.
    /// </summary>
    public class ResetPasswordModel(UserManager<ApplicationUser> userManager) : PageModel
    {
        /// <summary>
        /// Serviço responsável por operações com utilizadores, como redefinir passwords.
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        /// <summary>
        /// Modelo preenchido com os dados introduzidos no formulário (email, nova password, etc).
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; } = new();  // Inicializado para evitar CS8618

        /// <summary>
        /// Modelo interno que representa os campos do formulário de redefinição.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Email do utilizador que está a redefinir a senha.
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;

            /// <summary>
            /// Nova password a ser definida pelo utilizador.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} e no maximo {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;

            /// <summary>
            /// Campo de confirmação da nova password; deve coincidir com Password.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirmar password")]
            [Compare("Password", ErrorMessage = "A password e a confirmacao nao coincidem.")]
            public string ConfirmPassword { get; set; } = string.Empty;

            /// <summary>
            /// Código de redefinição enviado por email (codificado em Base64).
            /// </summary>
            public string Code { get; set; } = string.Empty;
        }

        /// <summary>
        /// Método executado ao aceder à página via GET. Valida e decodifica o código enviado no email.
        /// </summary>
        public IActionResult OnGet(string? code = null, string? email = null)
        {
            if (code == null || email == null)
                return BadRequest("Codigo invalido.");

            Input = new InputModel
            {
                Email = email,
                Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code))
            };

            return Page();
        }

        /// <summary>
        /// Método executado quando o utilizador submete o formulário com a nova password.
        /// </summary>
        public async Task<IActionResult> OnPostAsync()
        {
            /// <summary>
            /// Se os dados submetidos forem inválidos, volta a mostrar o formulário com erros.
            /// </summary>
            if (!ModelState.IsValid)
                return Page();

            /// <summary>
            /// Procura o utilizador pelo email fornecido.
            /// </summary>
            var user = await _userManager.FindByEmailAsync(Input.Email);

            /// <summary>
            /// Se o utilizador não existir, redireciona para a página de confirmação mesmo assim (não revela falha).
            /// </summary>
            if (user == null) return RedirectToPage("./ResetPasswordConfirmation");

            /// <summary>
            /// Tenta redefinir a password com o código fornecido e a nova senha.
            /// </summary>
            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);

            /// <summary>
            /// Se a redefinição for bem-sucedida, redireciona para a área de consultas.
            /// </summary>
            if (result.Succeeded)
                return Redirect("/Consultas");

            /// <summary>
            /// Se houver erros, adiciona-os ao ModelState para exibição.
            /// </summary>
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            /// <summary>
            /// Reexibe a página com os erros encontrados.
            /// </summary>
            return Page();
        }
    }
}
