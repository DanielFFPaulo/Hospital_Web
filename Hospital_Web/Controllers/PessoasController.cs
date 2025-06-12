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
            if (!ModelState.IsValid)
                return View(pessoa);

            _context.Pessoa.Add(pessoa);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
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
            {
                //  Apagar também o utilizador do Identity (caso exista)
                var user = await _userManager.FindByEmailAsync(pessoa.Email);
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                }

                try
                {
                    _context.Pessoa.Remove(pessoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Não é possível eliminar a pessoa porque existem registos associados (ex: consultas).");
                    return View("Delete", pessoa);
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
