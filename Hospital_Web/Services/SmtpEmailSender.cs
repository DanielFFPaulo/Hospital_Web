// Importa a biblioteca para enviar emails via SMTP (System.Net.Mail e redundante aqui, pois nao esta a ser usado)
using System.Net.Mail;

// Importa a biblioteca MimeKit, usada para construir mensagens de email formatadas
using MimeKit;

// Importa a classe que permite enviar emails via protocolo SMTP com suporte a autenticacao e seguranca
using MailKit.Net.Smtp;

// Importa o tipo de seguranca usado na conexao SMTP (ex: TLS/SSL)
using MailKit.Security;

// Permite injetar configuracoes atraves do sistema de configuracao do ASP.NET Core
using Microsoft.Extensions.Options;

// Define o namespace onde a classe esta inserida
namespace Hospital_Web.Services
{
    // Define a classe SmtpEmailSender que implementa a interface IEmailSender
    public class SmtpEmailSender : IEmailSender
    {
        // Campo privado onde serao armazenadas as configuracoes de email (como servidor, utilizador, senha)
        private readonly EmailSettings _settings;

        // Construtor da classe, recebe as configuracoes via injecao de dependencia
        public SmtpEmailSender(IOptions<EmailSettings> settings)
        {
            // Extrai o valor real das configuracoes (da classe EmailSettings)
            _settings = settings.Value;
        }

        // Metodo assincrono responsavel por enviar um email
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            // Cria uma nova mensagem MIME (formato de email estruturado)
            var emailMessage = new MimeMessage();

            // Define o remetente do email com nome e endereco retirados das configuracoes
            emailMessage.From.Add(new MailboxAddress(_settings.FromName, _settings.FromEmail));

            // Define o destinatario do email, usando o endereco passado como argumento
            emailMessage.To.Add(MailboxAddress.Parse(email));

            // Define o assunto do email
            emailMessage.Subject = subject;

            // Define o corpo da mensagem como HTML (permitindo formatacao e links)
            emailMessage.Body = new TextPart("html") { Text = message };

            // Cria uma instancia do cliente SMTP (MailKit)
            using var client = new MailKit.Net.Smtp.SmtpClient();

            // Liga-se ao servidor SMTP com as configuracoes fornecidas e encriptacao TLS
            await client.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort, SecureSocketOptions.StartTls);

            // Autentica com o nome de utilizador e senha fornecidos nas configuracoes
            await client.AuthenticateAsync(_settings.Username, _settings.Password);

            // Envia a mensagem construida
            await client.SendAsync(emailMessage);

            // Encerra a conexao com o servidor SMTP de forma limpa
            await client.DisconnectAsync(true);
        }
    }
}