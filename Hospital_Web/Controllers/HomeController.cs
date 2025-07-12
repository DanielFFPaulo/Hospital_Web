using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hospital_Web.Models;

namespace Hospital_Web.Models
{
    /// <summary>
    /// Controlador respons�vel pelas p�ginas principais do site,
    /// como a homepage, privacidade e informa��es gerais.
    /// Requer autentica��o para aceder, exceto onde indicado com [AllowAnonymous].
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {
        /// <summary>
        /// Logger para rastrear eventos e erros do controlador.
        /// </summary>
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Construtor que injeta o servi�o de logging.
        /// </summary>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// P�gina inicial da aplica��o (home).
        /// Requer que o utilizador esteja autenticado.
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// P�gina com informa��es adicionais sobre o sistema.
        /// Requer autentica��o.
        /// </summary>
        public IActionResult Info()
        {
            return View();
        }

        /// <summary>
        /// P�gina com a pol�tica de privacidade.
        /// Requer autentica��o.
        /// </summary>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// P�gina de erro, acess�vel a todos os utilizadores.
        /// Mostra detalhes t�cnicos em caso de erro.
        /// </summary>
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}