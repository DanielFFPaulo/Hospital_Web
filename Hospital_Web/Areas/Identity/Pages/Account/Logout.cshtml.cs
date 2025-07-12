using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Hospital_Web.Models;

/// <summary>
/// Define o namespace da pagina de logout dentro da area Identity.
/// </summary>
namespace Hospital_Web.Areas.Identity.Pages.Account
{
    /// <summary>
    /// Modelo da pagina Razor responsavel por terminar a sessão do utilizador (logout).
    /// </summary>
    public class LogoutModel : PageModel
    {
        /// <summary>
        /// Serviço responsavel por gerir a autenticação e sessões de utilizadores.
        /// </summary>
        private readonly SignInManager<ApplicationUser> _signInManager;

        /// <summary>
        /// Construtor que injeta o SignInManager necessario para realizar o logout.
        /// </summary>
        public LogoutModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        /// <summary>
        /// Metodo chamado quando o utilizador confirma a ação de logout (POST).
        /// Termina a sessão atual e redireciona para a pagina de login.
        /// </summary>
        public async Task<IActionResult> OnPost(string? returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Account/Login");
        }
    }
}
