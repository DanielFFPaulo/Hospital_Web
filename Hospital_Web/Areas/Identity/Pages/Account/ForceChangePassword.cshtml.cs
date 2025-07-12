using System.ComponentModel.DataAnnotations;
using Hospital_Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


/// <summary>
/// Define o namespace onde esta página Razor se encontra, dentro da área Identity, página de conta.
/// </summary>
namespace Hospital_Web.Areas.Identity.Pages.Account
{
    /// <summary>
    /// Modelo da página Razor responsável por forçar o utilizador a alterar a sua password.
    /// Recebe UserManager e SignInManager via injeção de dependência.
    /// </summary>
    public class ForceChangePasswordModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : PageModel
    {
        /// <summary>
        /// Permite realizar operações relacionadas com utilizadores, como redefinir a password.
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        /// <summary>
        /// Permite operações de autenticação, como renovar sessão após alteração de senha.
        /// </summary>
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

        /// <summary>
        /// Modelo de entrada de dados do formulário, que será automaticamente preenchido com os dados enviados.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        /// <summary>
        /// Classe interna usada para representar os dados do formulário de alteração de password.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Campo obrigatório para a nova password. É tratada como campo de tipo password.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            public string NewPassword { get; set; } = string.Empty;

            /// <summary>
            /// Campo obrigatório para confirmar a nova password.
            /// Deve coincidir com o campo NewPassword.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            [StringLength(100, MinimumLength = 6, ErrorMessage = "A {0} deve ter entre {2} e {1} caracteres.")]
            [Compare("NewPassword", ErrorMessage = "As passwords nao coincidem.")]
            public string ConfirmPassword { get; set; } = string.Empty;
        }

        /// <summary>
        /// Método executado quando a página é acedida via GET.
        /// Neste caso, não faz nada além de exibir o formulário.
        /// </summary>
        public void OnGet() { }

        /// <summary>
        /// Método executado quando o formulário é submetido (POST).
        /// Tenta redefinir a password do utilizador autenticado.
        /// </summary>
        public async Task<IActionResult> OnPostAsync()
        {
            /// <summary>
            /// Se os dados do formulário não forem válidos, volta a mostrar a página com os erros.
            /// </summary>
            if (!ModelState.IsValid)
                return Page();

            /// <summary>
            /// Obtém o utilizador autenticado atual.
            /// </summary>
            var user = await _userManager.GetUserAsync(User);

            /// <summary>
            /// Se não houver utilizador autenticado, redireciona para a página de login.
            /// </summary>
            if (user == null)
                return RedirectToPage("/Login");

            /// <summary>
            /// Gera um token válido para redefinir a password do utilizador.
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
                /// Marca que o utilizador já não precisa de alterar a senha (campo personalizado).
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
                /// Redireciona o utilizador para a página de consultas após sucesso.
                /// </summary>
                return Redirect("/Consultas");
            }

            /// <summary>
            /// Se houve erros ao alterar a password, adiciona-os ao ModelState para exibição.
            /// </summary>
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            /// <summary>
            /// Reexibe a página com os erros de validação ou redefinição.
            /// </summary>
            return Page();
        }
    }
}