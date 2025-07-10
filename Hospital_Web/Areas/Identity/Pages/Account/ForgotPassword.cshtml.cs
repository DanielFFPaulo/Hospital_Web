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


public class ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailSenderApp emailSender) : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IEmailSenderApp _emailSender = emailSender;

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public class InputModel
    {

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
        {
            // Avoid revealing whether the user exists
            return RedirectToPage("./ForgotPasswordConfirmation");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        var callbackUrl = Url.Page(
            "/Account/ResetPassword",
            pageHandler: null,
            values: new { area = "Identity", code = encodedToken, email = Input.Email },
            protocol: Request.Scheme);

        Console.WriteLine($"[DEBUG] Link de recuperação: {callbackUrl}");

        await _emailSender.SendEmailAsync(
            Input.Email,
            "Recuperação de Password - Hospital",
            $"<p>Olá,</p><p>Para redefinir a sua password <a href='{HtmlEncoder.Default.Encode(callbackUrl ?? string.Empty)}'>clique aqui</a>.</p>");

        return RedirectToPage("./ForgotPasswordConfirmation");
    }
}
