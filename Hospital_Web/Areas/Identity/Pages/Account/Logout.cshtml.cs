using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Hospital_Web.Models;

/// <summary>
/// Define o namespace da página de logout dentro da área Identity.
/// </summary>
namespace Hospital_Web.Areas.Identity.Pages.Account
{
    /// <summary>
    /// Modelo da página Razor responsável por terminar a sessão do utilizador (logout).
    /// </summary>
    public class LogoutModel : PageModel
    {
        /// <summary>
        /// Serviço responsável por gerir a autenticação e sessões de utilizadores.
        /// </summary>
        private readonly SignInManager<ApplicationUser> _signInManager;

        /// <summary>
        /// Construtor que injeta o SignInManager necessário para realizar o logout.
        /// </summary>
        public LogoutModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        /// <summary>
        /// Método chamado quando o utilizador confirma a ação de logout (POST).
        /// Termina a sessão atual e redireciona para a página de login.
        /// </summary>
        public async Task<IActionResult> OnPost(string? returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Account/Login");
        }
    }
}
