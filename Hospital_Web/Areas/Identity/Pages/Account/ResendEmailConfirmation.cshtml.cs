using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Hospital_Web.Models;
using Hospital_Web.Services;



/// <summary>
/// Modelo da página responsável por reenviar o email de confirmação e redefinir a senha.
/// </summary>
public class ResendEmailConfirmationModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender) : PageModel
{
    /// <summary>
    /// Serviço para gerir utilizadores e operações como redefinir senha ou encontrar por email.
    /// </summary>
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    /// <summary>
    /// Serviço responsável por enviar emails.
    /// </summary>
    private readonly IEmailSender _emailSender = emailSender;

    /// <summary>
    /// Dados inseridos no formulário, preenchidos automaticamente pelo binding.
    /// </summary>
    [BindProperty]
    public InputModel Input { get; set; } = new InputModel(); // Inicializa a propriedade para evitar problemas com ela ser null

    /// <summary>
    /// Classe interna que representa o modelo de entrada com o email do utilizador.
    /// </summary>
    public class InputModel
    {
        /// <summary>
        /// Campo obrigatório que representa o email do utilizador.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }

    /// <summary>
    /// Método chamado quando a página é acedida via GET.
    /// Neste caso, não faz nada além de exibir o formulário.
    /// </summary>
    public void OnGet() { }

    /// <summary>
    /// Método executado quando o formulário é submetido (POST).
    /// Reenvia as credenciais ao utilizador, redefinindo a senha.
    /// </summary>
    public async Task<IActionResult> OnPostAsync()
    {
        /// <summary>
        /// Se o modelo for inválido (ex: email em branco), recarrega a página com erros.
        /// </summary>
        if (!ModelState.IsValid)
            return Page();

        /// <summary>
        /// Tenta encontrar o utilizador com base no email fornecido.
        /// </summary>
        var user = await _userManager.FindByEmailAsync(Input.Email);

        /// <summary>
        /// Se o utilizador não existir, redireciona para a confirmação sem revelar o erro.
        /// </summary>
        if (user == null)
            return RedirectToPage("./ResendEmailConfirmationConfirmation");

        /// <summary>
        /// Gera uma nova password temporária (ex: Hosp@ABC123).
        /// </summary>
        var novaPassword = $"Hosp@{Guid.NewGuid().ToString("N")[..6]}";

        /// <summary>
        /// Gera um token válido para redefinição da password.
        /// </summary>
        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        /// <summary>
        /// Redefine a password do utilizador com a nova password temporária.
        /// </summary>
        var resetResult = await _userManager.ResetPasswordAsync(user, resetToken, novaPassword);

        /// <summary>
        /// Se a redefinição falhar, mostra erro na página.
        /// </summary>
        if (!resetResult.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Erro ao redefinir a password.");
            return Page();
        }

        /// <summary>
        /// Cria a mensagem de email com as novas credenciais de acesso.
        /// </summary>
        var mensagem = $@"
        <p>Ola {user.UserName},</p>
        <p>Seja bem-vindo ao nosso sistema do <b>hospital</b>. A sua conta foi criada com sucesso.</p>
        <p><b>Credenciais de acesso:</b><br>
        Email: {user.Email}<br>
        Senha: {novaPassword}</p>
        <p>Por favor, altere a sua senha apos o primeiro login.</p>
    ";

        /// <summary>
        /// Envia o email com as novas credenciais ao utilizador.
        /// </summary>
        await _emailSender.SendEmailAsync(user.Email ?? string.Empty, "Reenvio de credenciais - Hospital", mensagem);

        /// <summary>
        /// Redireciona para a página de confirmação após o envio do email.
        /// </summary>
        return RedirectToPage("./ResendEmailConfirmationConfirmation");
    }
}
