using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace Hospital_Web.Services
{
    public class DummyEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Apenas simula envio — NÃO ENVIA NADA
            Console.WriteLine($"Simulado envio para {email}: {subject}");
            return Task.CompletedTask;
        }

    }
}

