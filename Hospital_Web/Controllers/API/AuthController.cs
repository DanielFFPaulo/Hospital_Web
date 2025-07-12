using Hospital_Web.Models;
using Hospital_Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

/// <summary>
/// Define o namespace para controladores de API relacionados com autenticação.
/// </summary>
namespace Hospital_Web.Controllers.API
{
    /// <summary>
    /// Controlador de API que expõe endpoints de autenticação (login) e gera tokens JWT.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Serviço que permite operações com utilizadores (procurar por email, etc.).
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Serviço que verifica as credenciais de login.
        /// </summary>
        private readonly SignInManager<ApplicationUser> _signInManager;

        /// <summary>
        /// Serviço customizado responsável por gerar tokens JWT.
        /// </summary>
        private readonly TokenService _tokenService;

        /// <summary>
        /// Construtor que injeta dependências necessárias ao controlador.
        /// </summary>
        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            TokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Endpoint anónimo que autentica o utilizador e devolve um token JWT.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            /// <summary>
            /// Procura o utilizador pelo email fornecido.
            /// </summary>
            var user = await _userManager.FindByEmailAsync(login.Email);

            /// <summary>
            /// Se não existir, devolve 401 Unauthorized.
            /// </summary>
            if (user == null)
                return Unauthorized("Utilizador nao encontrado");

            /// <summary>
            /// Verifica se a password está correcta.
            /// </summary>
            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            /// <summary>
            /// Se falhar, devolve 401 Unauthorized.
            /// </summary>
            if (!result.Succeeded)
                return Unauthorized("Credenciais invalidas");

            /// <summary>
            /// Gera token JWT para o utilizador autenticado.
            /// </summary>
            var token = await _tokenService.GenerateTokenAsync(user);

            /// <summary>
            /// Devolve 200 OK com o token no corpo da resposta.
            /// </summary>
            return Ok(new { token });
        }
    }

    /// <summary>
    /// DTO que transporta email e password recebidos no corpo do pedido.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Email introduzido pelo utilizador.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Password introduzida pelo utilizador.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}

