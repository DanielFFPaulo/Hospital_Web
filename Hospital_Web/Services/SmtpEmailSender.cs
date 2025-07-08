// Importa a biblioteca para enviar emails via SMTP (System.Net.Mail é redundante aqui, pois não está a ser usado)
using System.Net.Mail;

// Importa a biblioteca MimeKit, usada para construir mensagens de email formatadas
using MimeKit;

// Importa a classe que permite enviar emails via protocolo SMTP com suporte a autenticação e segurança
using MailKit.Net.Smtp;

// Importa o tipo de segurança usado na conexão SMTP (ex: TLS/SSL)
using MailKit.Security;

// Permite injetar configurações através do sistema de configuração do ASP.NET Core
using Microsoft.Extensions.Options;

// Define o namespace onde a classe está inserida
namespace Hospital_Web.Services
{
    // Define a classe SmtpEmailSender que implementa a interface IEmailSender
    public class SmtpEmailSender : IEmailSender
    {
        // Campo privado onde serão armazenadas as configurações de email (como servidor, utilizador, senha)
        private readonly EmailSettings _settings;

        // Construtor da classe, recebe as configurações via injeção de dependência
        public SmtpEmailSender(IOptions<EmailSettings> settings)
        {
            // Extrai o valor real das configurações (da classe EmailSettings)
            _settings = settings.Value;
        }

        // Método assíncrono responsável por enviar um email
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            // Cria uma nova mensagem MIME (formato de email estruturado)
            var emailMessage = new MimeMessage();

            // Define o remetente do email com nome e endereço retirados das configurações
            emailMessage.From.Add(new MailboxAddress(_settings.FromName, _settings.FromEmail));

            // Define o destinatário do email, usando o endereço passado como argumento
            emailMessage.To.Add(MailboxAddress.Parse(email));

            // Define o assunto do email
            emailMessage.Subject = subject;

            // Define o corpo da mensagem como HTML (permitindo formatação e links)
            emailMessage.Body = new TextPart("html") { Text = message };

            // Cria uma instância do cliente SMTP (MailKit)
            using var client = new MailKit.Net.Smtp.SmtpClient();

            // Liga-se ao servidor SMTP com as configurações fornecidas e encriptação TLS
            await client.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort, SecureSocketOptions.StartTls);

            // Autentica com o nome de utilizador e senha fornecidos nas configurações
            await client.AuthenticateAsync(_settings.Username, _settings.Password);

            // Envia a mensagem construída
            await client.SendAsync(emailMessage);

            // Encerra a conexão com o servidor SMTP de forma limpa
            await client.DisconnectAsync(true);
        }
    }
}