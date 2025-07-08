
// Importa a funcionalidade de Identity do ASP.NET Core (para gerir utilizadores)
using Microsoft.AspNetCore.Identity;

// Importa classes relacionadas com segurança de tokens
using Microsoft.IdentityModel.Tokens;

// Importa classes para criação de tokens JWT
using System.IdentityModel.Tokens.Jwt;

// Importa classes para manipular claims (informações dentro do token)
using System.Security.Claims;

// Importa classe para codificação de strings (como chaves secretas)
using System.Text;

// Define o namespace onde esta classe está incluída
namespace Hospital_Web.Services
{
    /// <summary>
    /// Classe responsável pela geração de tokens JWT (JSON Web Tokens)
    /// </summary>
    public class TokenService
    {
        // Campo para aceder às configurações (como a chave secreta, tempo de expiração, etc.)
        private readonly IConfiguration _config;

        // Construtor que recebe as configurações da aplicação por injeção de dependência
        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Gera um token JWT para o utilizador fornecido
        /// </summary>
        public string GenerateToken(IdentityUser user)
        {
            // Obtém a secção "Jwt" do ficheiro de configuração (appsettings.json)
            var jwtSettings = _config.GetSection("Jwt");

            // Cria uma chave simétrica a partir da string secreta definida nas configurações
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));

            // Define as credenciais de assinatura com algoritmo HmacSha256
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Define os dados (claims) a incluir dentro do token
            var claims = new[]
            {
                // Claim "sub" (subject): identifica o utilizador pelo seu ID
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),

                // Claim "email": inclui o email do utilizador
                new Claim(JwtRegisteredClaimNames.Email, user.Email),

                // Claim "jti": identificador único do token (evita reutilização)
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

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
