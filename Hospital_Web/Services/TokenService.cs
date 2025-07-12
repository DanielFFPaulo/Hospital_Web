
// Importa a funcionalidade de Identity do ASP.NET Core (para gerir utilizadores)
using Hospital_Web.Models;
using Microsoft.AspNetCore.Identity;

// Importa classes relacionadas com seguranca de tokens
using Microsoft.IdentityModel.Tokens;

// Importa classes para criacao de tokens JWT
using System.IdentityModel.Tokens.Jwt;

// Importa classes para manipular claims (informacoes dentro do token)
using System.Security.Claims;

// Importa classe para codificacao de strings (como chaves secretas)
using System.Text;

// Define o namespace onde esta classe esta incluida
namespace Hospital_Web.Services
{
    /// <summary>
    /// Classe responsavel pela geracao de tokens JWT (JSON Web Tokens)
    /// </summary>
    public class TokenService
    {
        // Campo para aceder as configuracoes (como a chave secreta, tempo de expiracao, etc.)
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;

        // Construtor que recebe as configuracoes da aplicacao por injecao de dependencia

        public TokenService(IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }


        /// <summary>
        /// Gera um token JWT para o utilizador fornecido
        /// </summary>  
        public async Task<string> GenerateTokenAsync(IdentityUser user)
        {
            // Obtem a seccao "Jwt" do ficheiro de configuracao (appsettings.json)
            var jwtSettings = _config.GetSection("Jwt");

            // Verifica se a chave secreta nao e nula ou vazia
            var secretKey = jwtSettings["Key"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("JWT secret key is not configured.");
            }

            // Cria uma chave simetrica a partir da string secreta definida nas configuracoes
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            // Define as credenciais de assinatura com algoritmo HmacSha256
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Saber 
            var emailClaimValue = user.Email ?? throw new InvalidOperationException("User email is null.");

            // Define os dados (claims) a incluir dentro do token
            var roles = await _userManager.GetRolesAsync((ApplicationUser)user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Add each role as a claim
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            // Cria o token JWT com todas as propriedades definidas
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],                // Quem emitiu o token
                audience: jwtSettings["Audience"],            // Quem pode usar o token
                claims: claims,                               // As claims que identificam o utilizador
                expires: DateTime.UtcNow.AddHours(            // Quando o token expira
                    Convert.ToDouble(jwtSettings["ExpireHours"])
                ),
                signingCredentials: creds                     // Credenciais usadas para assinar o token
            );

            // Converte o token em string e retorna
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
