using Hospital_Web.Models; // onde está ApplicationUser
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Hospital_Web.Services; // onde está EmailService ou IEmailSender


public class AdministradorController : Controller
{
    private readonly Hospital_Web.Services.IEmailSender _emailSender;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdministradorController(
        UserManager<ApplicationUser> userManager,
        Hospital_Web.Services.IEmailSender emailSender)
    {
        _userManager = userManager;
        _emailSender = emailSender;
    }

    [HttpPost]
    public async Task<IActionResult> CriarUtilizador(string email)
    {
        // 1. Criar uma password aleatória (podes fazer mais seguro depois)
        var password = "Temp1234!"; // Ideal: gerar dinamicamente

        // 2. Criar utilizador
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email
        };

        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            // 3. Enviar email com as credenciais
            var subject = "Conta criada com sucesso";
            var body = $@"
                <h3>Bem-vindo ao Hospital</h3>
                <p>As suas credenciais são:</p>
                <p><strong>Email:</strong> {email}</p>
                <p><strong>Password:</strong> {password}</p>
                <p><a href='https://localhost:7140/Identity/Account/Login'>Clique aqui para iniciar sessão</a></p>
            ";

            await _emailSender.SendEmailAsync(email, subject, body);

            return RedirectToAction("Index");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        return View(); // ou retorna com erro
    }
}