using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital_Web.Data;
using Hospital_Web.Models;
using Hospital_Web.Services;
using Microsoft.AspNetCore.Identity;

namespace Hospital_Web.Controllers
{
    /// <summary>
    /// Controlador responsável pela gestão de pessoas no sistema hospitalar.
    /// Permite criar, editar, consultar, e apagar registos da entidade Pessoa.
    /// Também remove utilizadores associados no Identity, se existirem.
    /// </summary>
    public class PessoasController : Controller
    {
        private readonly Hospital_WebContext _context;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Construtor que injeta dependências do contexto, sistema de emails e gestão de utilizadores.
        /// </summary>
        public PessoasController(
          Hospital_WebContext context,
          IEmailSender emailSender,
          UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _emailSender = emailSender;
            _userManager = userManager;
        }

        /// <summary>
        /// Mostra a lista de todas as pessoas registadas.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pessoa.ToListAsync());
        }

        /// <summary>
        /// Mostra os detalhes de uma pessoa específica.
        /// </summary>
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

        /// <summary>
        /// Mostra o formulário para criação de uma nova pessoa.
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Cria uma nova pessoa após submissão do formulário.
        /// </summary>
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

        /// <summary>
        /// Mostra o formulário para editar uma pessoa existente.
        /// </summary>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var pessoa = await _context.Pessoa.FindAsync(id);
            if (pessoa == null)
                return NotFound();

            return View(pessoa);
        }

        /// <summary>
        /// Mostra a confirmação para apagar uma pessoa específica.
        /// </summary>
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

        /// <summary>
        /// Elimina uma pessoa e, se existir, também o utilizador associado no sistema Identity.
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoa = await _context.Pessoa.FindAsync(id);

            if (pessoa != null)
            {
                // Tenta remover o utilizador associado no Identity pelo email
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
                    // Caso existam dependências (ex: consultas), mostra erro e não elimina
                    ModelState.AddModelError("", "Nao e possivel eliminar a pessoa porque existem registos associados (ex: consultas).");
                    return View("Delete", pessoa);
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
