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


public class ForgotPasswordModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailSenderApp _emailSender;


    public ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailSenderApp emailSender)
    {
        _userManager = userManager;
        _emailSender = emailSender;
    }


    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var user = await _userManager.FindByEmailAsync(Input.Email);
        // if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
        //{
        //    Evita revelar se o utilizador existe
        //     return RedirectToPage("./ForgotPasswordConfirmation");
        // }

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
            $"<p>Olá,</p><p>Para redefinir a sua password <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clique aqui</a>.</p>");

        return RedirectToPage("./ForgotPasswordConfirmation");
    }
}
