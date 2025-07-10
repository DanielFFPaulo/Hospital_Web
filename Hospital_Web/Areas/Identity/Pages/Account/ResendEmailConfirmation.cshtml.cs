using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Hospital_Web.Models;
using Hospital_Web.Services;

public class ResendEmailConfirmationModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender) : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IEmailSender _emailSender = emailSender;

    [BindProperty]
    public InputModel Input { get; set; } = new InputModel(); // Inicializa a propriedade para evidar problemas com ela ser null

    public class InputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var user = await _userManager.FindByEmailAsync(Input.Email);
        if (user == null)
            return RedirectToPage("./ResendEmailConfirmationConfirmation");

        // Gerar nova password temporária
        var novaPassword = $"Hosp@{Guid.NewGuid().ToString("N")[..6]}";

        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        var resetResult = await _userManager.ResetPasswordAsync(user, resetToken, novaPassword);

        if (!resetResult.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Erro ao redefinir a password.");
            return Page();
        }


        // Enviar email com as novas credenciais
        var mensagem = $@"
        <p>Olá {user.UserName},</p>
        <p>Seja bem-vindo ao nosso sistema do <b>hospital</b>. A sua conta foi criada com sucesso.</p>
        <p><b>Credenciais de acesso:</b><br>
        Email: {user.Email}<br>
        Senha: {novaPassword}</p>
        <p>Por favor, altere a sua senha após o primeiro login.</p>
    ";

        await _emailSender.SendEmailAsync(user.Email ?? string.Empty, "Reenvio de credenciais - Hospital", mensagem);

        return RedirectToPage("./ResendEmailConfirmationConfirmation");
    }

}
