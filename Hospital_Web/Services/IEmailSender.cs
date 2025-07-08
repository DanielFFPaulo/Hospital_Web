// Define o namespace onde a interface está localizada
namespace Hospital_Web.Services
{
    // Define uma interface chamada IEmailSender
    // Interfaces são contratos que obrigam qualquer classe que a implemente
    // a fornecer uma implementação para os métodos aqui definidos
    public interface IEmailSender
    {
        // Método assíncrono para envio de emails
        // Recebe como parâmetros:
        // - email: o endereço do destinatário
        // - subject: o assunto do email
        // - message: o corpo do email
        // Retorna uma Task porque será executado de forma assíncrona
        Task SendEmailAsync(string email, string subject, string message);
    }
}