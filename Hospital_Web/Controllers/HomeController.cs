using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Hospital_Web.Models;
using Microsoft.AspNetCore.Authorization;
using SendEmail.Models;




namespace Hospital_Web.Models;

[AllowAnonymous]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Ferramentas _ferramenta;

    // Injeta também a classe Ferramentas
    public HomeController(ILogger<HomeController> logger, Ferramentas ferramenta)
    {
        _logger = logger;
        _ferramenta = ferramenta;
    }

    // GET
    public IActionResult Index()
    {
        return View();
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> Index([Bind("Subject,Body,Destinatario")] Email email)
    {
        if (ModelState.IsValid)
        {
            var resposta = await _ferramenta.EnviaEmailAsync(email);

            string texto = "Email enviado em: " + DateTime.Now.ToString() +
                         "\r\n" + "Destinatário: " + email.Destinatario +
                         "\r\n" + "Assunto: " + email.Subject +
                         "\r\n" + "Corpo Email: " + email.Body;

            await _ferramenta.EscreveLogAsync("Home", "Index", texto, "");

            if (resposta == 0)
            {
                TempData["Mensagem"] = "S#:Email Enviado com sucesso.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Mensagem"] = "E#:Ocorreu um erro com o envio do Email.";
            }
        }

        return View(email);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
