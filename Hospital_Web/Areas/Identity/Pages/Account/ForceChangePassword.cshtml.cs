using System.ComponentModel.DataAnnotations;
using Hospital_Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


/// <summary>
/// Define o namespace onde esta pagina Razor se encontra, dentro da area Identity, pagina de conta.
/// </summary>
namespace Hospital_Web.Areas.Identity.Pages.Account
{
    /// <summary>
    /// Modelo da pagina Razor responsavel por forçar o utilizador a alterar a sua password.
    /// Recebe UserManager e SignInManager via injeção de dependência.
    /// </summary>
    public class ForceChangePasswordModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : PageModel
    {
        /// <summary>
        /// Permite realizar operações relacionadas com utilizadores, como redefinir a password.
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        /// <summary>
        /// Permite operações de autenticação, como renovar sessão apos alteração de senha.
        /// </summary>
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

        /// <summary>
        /// Modelo de entrada de dados do formulario, que sera automaticamente preenchido com os dados enviados.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        /// <summary>
        /// Classe interna usada para representar os dados do formulario de alteração de password.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Campo obrigatorio para a nova password. e tratada como campo de tipo password.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            public string NewPassword { get; set; } = string.Empty;

            /// <summary>
            /// Campo obrigatorio para confirmar a nova password.
            /// Deve coincidir com o campo NewPassword.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            [StringLength(100, MinimumLength = 6, ErrorMessage = "A {0} deve ter entre {2} e {1} caracteres.")]
            [Compare("NewPassword", ErrorMessage = "As passwords nao coincidem.")]
            public string ConfirmPassword { get; set; } = string.Empty;
        }

        /// <summary>
        /// Metodo executado quando a pagina e acedida via GET.
        /// Neste caso, não faz nada alem de exibir o formulario.
        /// </summary>
        public void OnGet() { }

        /// <summary>
        /// Metodo executado quando o formulario e submetido (POST).
        /// Tenta redefinir a password do utilizador autenticado.
        /// </summary>
        public async Task<IActionResult> OnPostAsync()
        {
            /// <summary>
            /// Se os dados do formulario não forem validos, volta a mostrar a pagina com os erros.
            /// </summary>
            if (!ModelState.IsValid)
                return Page();

            /// <summary>
            /// Obtem o utilizador autenticado atual.
            /// </summary>
            var user = await _userManager.GetUserAsync(User);

            /// <summary>
            /// Se não houver utilizador autenticado, redireciona para a pagina de login.
            /// </summary>
            if (user == null)
                return RedirectToPage("/Login");

            /// <summary>
            /// Gera um token valido para redefinir a password do utilizador.
            /// </summary>
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            /// <summary>
            /// Tenta redefinir a password do utilizador com o token gerado e a nova password fornecida.
            /// </summary>
            var result = await _userManager.ResetPasswordAsync(user, token, Input.NewPassword);

            /// <summary>
            /// Se a redefinição da password for bem-sucedida...
            /// </summary>
            if (result.Succeeded)
            {
                /// <summary>
                /// Marca que o utilizador ja não precisa de alterar a senha (campo personalizado).
                /// </summary>
                user.DeveAlterarSenha = false;

                /// <summary>
                /// Atualiza os dados do utilizador na base de dados.
                /// </summary>
                await _userManager.UpdateAsync(user);

                /// <summary>
                /// Atualiza a sessão de login para refletir as alterações feitas ao utilizador.
                /// </summary>
                await _signInManager.RefreshSignInAsync(user);

                /// <summary>
                /// Redireciona o utilizador para a pagina de consultas apos sucesso.
                /// </summary>
                return Redirect("/Consultas");
            }

            /// <summary>
            /// Se houve erros ao alterar a password, adiciona-os ao ModelState para exibição.
            /// </summary>
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            /// <summary>
            /// Reexibe a pagina com os erros de validação ou redefinição.
            /// </summary>
            return Page();
        }
    }
}