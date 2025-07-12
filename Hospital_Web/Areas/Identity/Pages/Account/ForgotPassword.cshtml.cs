using Hospital_Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System;
using Microsoft.AspNetCore.Identity.UI.Services;
using Hospital_Web.Services;
using IEmailSenderApp = Hospital_Web.Services.IEmailSender;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;


/// <summary>
/// Modelo da pagina Razor responsavel pelo processo de recuperação de password.
/// </summary>
public class ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailSenderApp emailSender) : PageModel
{
    /// <summary>
    /// Permite realizar operações relacionadas com utilizadores, como procurar por email e gerar tokens.
    /// </summary>
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    /// <summary>
    /// Serviço de envio de email personalizado usado para enviar o link de redefinição de password.
    /// </summary>
    private readonly IEmailSenderApp _emailSender = emailSender;

    /// <summary>
    /// Modelo de dados do formulario onde o utilizador introduz o seu email.
    /// </summary>
    [BindProperty]
    public InputModel Input { get; set; } = new();

    /// <summary>
    /// Modelo interno com o campo de email introduzido pelo utilizador.
    /// </summary>
    public class InputModel
    {
        /// <summary>
        /// Endereço de email do utilizador que solicita a recuperação de password.
        /// </summary>
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }

    /// <summary>
    /// Metodo executado quando a pagina e acedida via GET.
    /// Neste caso, não faz nada alem de exibir o formulario.
    /// </summary>
    public void OnGet() { }

    /// <summary>
    /// Metodo executado quando o formulario de recuperação e submetido.
    /// Envia um email com link de redefinição se o email existir.
    /// </summary>
    public async Task<IActionResult> OnPostAsync()
    {
        /// <summary>
        /// Se os dados do formulario forem invalidos, retorna a pagina com erros.
        /// </summary>
        if (!ModelState.IsValid)
            return Page();

        /// <summary>
        /// Procura o utilizador com o email fornecido.
        /// </summary>
        var user = await _userManager.FindByEmailAsync(Input.Email);

        /// <summary>
        /// Se o utilizador não existir, redireciona sem revelar essa informação.
        /// </summary>
        if (user == null)
        {
            // Avoid revealing whether the user exists
            return RedirectToPage("./ForgotPasswordConfirmation");
        }

        /// <summary>
        /// Gera um token de redefinição de password para o utilizador.
        /// </summary>
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        /// <summary>
        /// Codifica o token gerado em Base64 para ser usado numa URL.
        /// </summary>
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        /// <summary>
        /// Cria o link de callback que sera enviado por email.
        /// Contem o token codificado e o email do utilizador.
        /// </summary>
        var callbackUrl = Url.Page(
            "/Account/ResetPassword",
            pageHandler: null,
            values: new { area = "Identity", code = encodedToken, email = Input.Email },
            protocol: Request.Scheme);

        /// <summary>
        /// Mostra o link de recuperação no terminal (debug).
        /// </summary>
        Console.WriteLine($"[DEBUG] Link de recuperacao: {callbackUrl}");

        /// <summary>
        /// Envia o email ao utilizador com o link de redefinição de password.
        /// </summary>
        await _emailSender.SendEmailAsync(
            Input.Email,
            "Recuperacao de Password - Hospital",
            $"<p>Ola,</p><p>Para redefinir a sua password <a href='{HtmlEncoder.Default.Encode(callbackUrl ?? string.Empty)}'>clique aqui</a>.</p>");

        /// <summary>
        /// Redireciona para a pagina de confirmação apos o envio do email.
        /// </summary>
        return RedirectToPage("./ForgotPasswordConfirmation");
    }
}
