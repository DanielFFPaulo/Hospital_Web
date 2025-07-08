// Define o namespace onde esta classe está incluída
namespace Hospital_Web.Services
{
    // Classe que representa as definições/configurações para envio de email
    public class EmailSettings
    {
        // Nome do remetente que aparecerá nos emails enviados
        public string FromName { get; set; }

        // Endereço de email do remetente
        public string FromEmail { get; set; }

        // Endereço do servidor SMTP (ex: smtp.gmail.com)
        public string SmtpServer { get; set; }

        // Porta utilizada para enviar emails (ex: 587 para TLS, 465 para SSL)
        public int SmtpPort { get; set; }

        // Nome de utilizador usado na autenticação com o servidor SMTP
        public string Username { get; set; }

        // Palavra-passe correspondente ao nome de utilizador, usada para autenticação SMTP
        public string Password { get; set; }
    }
}