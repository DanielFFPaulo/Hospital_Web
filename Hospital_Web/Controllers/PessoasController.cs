using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital_Web.Data;
using Hospital_Web.Models;
using Hospital_Web.Services;
using Microsoft.AspNetCore.Identity;

namespace Hospital_Web.Controllers
{
    public class PessoasController : Controller
    {
        private readonly Hospital_WebContext _context;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;

        public PessoasController(
          Hospital_WebContext context,
          IEmailSender emailSender,
          UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _emailSender = emailSender;
            _userManager = userManager;
        }

        // GET: Pessoas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pessoa.ToListAsync());
        }

        // GET: Pessoas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var pessoa = await _context.Pessoa
                .FirstOrDefaultAsync(m => m.N_Processo == id);
            if (pessoa == null)
                return NotFound();

            return View(pessoa);
        }

        // GET: Pessoas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pessoas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("N_Processo,Nome,Idade,Data_de_Nascimento,Morada,Telemovel,TelemovelAlt,Email,NIF,Cod_Postal,Localidade")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pessoa);
                await _context.SaveChangesAsync();

                var user = new ApplicationUser
                {
                    UserName = pessoa.Email,
                    Email = pessoa.Email,
                    DeveAlterarSenha = true
                };

                string senhaTemporaria = "Hosp@" + Guid.NewGuid().ToString("N").Substring(0, 6);

                var result = await _userManager.CreateAsync(user, senhaTemporaria);

                if (result.Succeeded)
                {
                    await _emailSender.SendEmailAsync(
                        pessoa.Email,
                        "Bem-vindo ao Portal do Hospital",
                        $@"
                        <p>Olá {pessoa.Nome},</p>
                        <p>Seja bem-vindo ao nosso sistema do hospital. A sua conta foi criada com sucesso.</p>
                        <p><strong>Credenciais de acesso:</strong><br>
                        Email: {pessoa.Email}<br>
                        Senha: {senhaTemporaria}</p>
                        <p>Por favor, altere a sua senha após o primeiro login.</p>"
                    );
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                    return View(pessoa);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(pessoa);
        }

        // GET: Pessoas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var pessoa = await _context.Pessoa.FindAsync(id);
            if (pessoa == null)
                return NotFound();

            return View(pessoa);
        }

        // POST: Pessoas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("N_Processo,Nome,Idade,Data_de_Nascimento,Morada,Telemovel,TelemovelAlt,Email,NIF,Cod_Postal,Localidade")] Pessoa pessoa)
        {
            if (id != pessoa.N_Processo)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pessoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoa.N_Processo))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(pessoa);
        }

        // GET: Pessoas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var pessoa = await _context.Pessoa
                .FirstOrDefaultAsync(m => m.N_Processo == id);
            if (pessoa == null)
                return NotFound();

            return View(pessoa);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoa = await _context.Pessoa.FindAsync(id);
            if (pessoa != null)
                _context.Pessoa.Remove(pessoa);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaExists(int id)
        {
            return _context.Pessoa.Any(e => e.N_Processo == id);
        }
    }
}
