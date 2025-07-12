// Define o namespace onde a interface esta localizada
namespace Hospital_Web.Services
{
    // Define uma interface chamada IEmailSender
    // Interfaces sao contratos que obrigam qualquer classe que a implemente
    // a fornecer uma implementacao para os metodos aqui definidos
    public interface IEmailSender
    {
        // Metodo assincrono para envio de emails
        // Recebe como parametros:
        // - email: o endereco do destinatario
        // - subject: o assunto do email
        // - message: o corpo do email
        // Retorna uma Task porque sera executado de forma assincrona
        Task SendEmailAsync(string email, string subject, string message);
    }
}