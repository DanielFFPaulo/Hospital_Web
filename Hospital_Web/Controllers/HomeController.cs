using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hospital_Web.Models;

namespace Hospital_Web.Models
{
    /// <summary>
    /// Controlador responsável pelas páginas principais do site,
    /// como a homepage, privacidade e informações gerais.
    /// Requer autenticação para aceder, exceto onde indicado com [AllowAnonymous].
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {
        /// <summary>
        /// Logger para rastrear eventos e erros do controlador.
        /// </summary>
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Construtor que injeta o serviço de logging.
        /// </summary>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Página inicial da aplicação (home).
        /// Requer que o utilizador esteja autenticado.
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Página com informações adicionais sobre o sistema.
        /// Requer autenticação.
        /// </summary>
        public IActionResult Info()
        {
            return View();
        }

        /// <summary>
        /// Página com a política de privacidade.
        /// Requer autenticação.
        /// </summary>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Página de erro, acessível a todos os utilizadores.
        /// Mostra detalhes técnicos em caso de erro.
        /// </summary>
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}