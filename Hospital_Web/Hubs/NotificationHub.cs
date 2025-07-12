using Microsoft.AspNetCore.SignalR;

namespace Hospital_Web.Hubs
{
    /// <summary>
    /// Hub de SignalR para envio de notificações em tempo real.
    /// Esta classe permite que clientes (por exemplo, navegadores) estabeleçam ligação com o servidor
    /// e recebam notificações instantâneas.
    /// </summary>
    public class NotificationHub : Hub
    {
        // Neste momento, o Hub está vazio.
        // Podemos adicionar métodos aqui para serem chamados pelos clientes via JavaScript ou por outros servidores.
        //
        // Exemplo possível a adicionar:
        // public async Task EnviarMensagem(string userId, string mensagem)
        // {
        //     await Clients.User(userId).SendAsync("ReceberMensagem", mensagem);
        // }
    }
}
