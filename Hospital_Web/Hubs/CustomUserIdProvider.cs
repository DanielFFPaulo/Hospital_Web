namespace Hospital_Web.Hubs
{
    // Importa os namespaces necessários:
    // - Microsoft.AspNetCore.SignalR: para trabalhar com Hubs do SignalR.
    // - System.Security.Claims: para acessar os dados de identidade do utilizador.
    using Microsoft.AspNetCore.SignalR;
    using System.Security.Claims;

    /// <summary>
    /// Classe que implementa IUserIdProvider para personalizar a forma como o SignalR identifica os utilizadores.
    /// </summary>
    public class CustomUserIdProvider : IUserIdProvider
    {
        /// <summary>
        /// Método chamado pelo SignalR para obter o identificador único de um utilizador conectado.
        /// </summary>
        /// <param name="connection">Contexto da conexão atual do SignalR.</param>
        /// <returns>O ID do utilizador, baseado na claim "NameIdentifier", ou null se não estiver autenticado.</returns>
        public string? GetUserId(HubConnectionContext connection)
        {
            // Acede ao utilizador autenticado e retorna o valor da claim que representa o identificador único do utilizador (geralmente o ID no Identity).
            return connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
