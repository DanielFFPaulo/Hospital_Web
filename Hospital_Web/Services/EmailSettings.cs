// Define o namespace onde esta classe esta incluida
namespace Hospital_Web.Services
{
    // Classe que representa as definicoes/configuracoes para envio de email
    public class EmailSettings
    {
        // Nome do remetente que aparecera nos emails enviados
        public string FromName { get; set; } = string.Empty;

        // Endereco de email do remetente
        public string FromEmail { get; set; } = string.Empty;

        // Endereco do servidor SMTP (ex: smtp.gmail.com)
        public string SmtpServer { get; set; } = string.Empty;

        // Porta utilizada para enviar emails (ex: 587 para TLS, 465 para SSL)
        public int SmtpPort { get; set; }

        // Nome de utilizador usado na autenticacao com o servidor SMTP
        public string Username { get; set; } = string.Empty;

        // Palavra-passe correspondente ao nome de utilizador, usada para autenticacao SMTP
        public string Password { get; set; } = string.Empty;
    }
}