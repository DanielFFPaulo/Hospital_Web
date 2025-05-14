using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace Hospital_Web.Services
{
    public class DummyEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Apenas simula envio de email (para testes)
            Console.WriteLine($"Email para: {email}");
            Console.WriteLine($"Assunto: {subject}");
            Console.WriteLine($"Mensagem: {htmlMessage}");
            return Task.CompletedTask;
        }
    }
}

