using System.ComponentModel.DataAnnotations;
using System.Text;
using Hospital_Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

/// <summary>
/// Define o namespace da pagina de redefinição de password dentro da area Identity.
/// </summary>
namespace Hospital_Web.Areas.Identity.Pages.Account
{
    /// <summary>
    /// Pagina responsavel por permitir ao utilizador redefinir a sua password a partir de um codigo enviado por email.
    /// </summary>
    public class ResetPasswordModel(UserManager<ApplicationUser> userManager) : PageModel
    {
        /// <summary>
        /// Serviço responsavel por operações com utilizadores, como redefinir passwords.
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        /// <summary>
        /// Modelo preenchido com os dados introduzidos no formulario (email, nova password, etc).
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; } = new();  // Inicializado para evitar CS8618

        /// <summary>
        /// Modelo interno que representa os campos do formulario de redefinição.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Email do utilizador que esta a redefinir a senha.
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
            /// Codigo de redefinição enviado por email (codificado em Base64).
            /// </summary>
            public string Code { get; set; } = string.Empty;
        }

        /// <summary>
        /// Metodo executado ao aceder a pagina via GET. Valida e decodifica o codigo enviado no email.
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
        /// Metodo executado quando o utilizador submete o formulario com a nova password.
        /// </summary>
        public async Task<IActionResult> OnPostAsync()
        {
            /// <summary>
            /// Se os dados submetidos forem invalidos, volta a mostrar o formulario com erros.
            /// </summary>
            if (!ModelState.IsValid)
                return Page();

            /// <summary>
            /// Procura o utilizador pelo email fornecido.
            /// </summary>
            var user = await _userManager.FindByEmailAsync(Input.Email);

            /// <summary>
            /// Se o utilizador não existir, redireciona para a pagina de confirmação mesmo assim (não revela falha).
            /// </summary>
            if (user == null) return RedirectToPage("./ResetPasswordConfirmation");

            /// <summary>
            /// Tenta redefinir a password com o codigo fornecido e a nova senha.
            /// </summary>
            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);

            /// <summary>
            /// Se a redefinição for bem-sucedida, redireciona para a area de consultas.
            /// </summary>
            if (result.Succeeded)
                return Redirect("/Consultas");

            /// <summary>
            /// Se houver erros, adiciona-os ao ModelState para exibição.
            /// </summary>
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            /// <summary>
            /// Reexibe a pagina com os erros encontrados.
            /// </summary>
            return Page();
        }
    }
}
