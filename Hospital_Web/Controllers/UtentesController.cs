using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital_Web.Data;
using Hospital_Web.Models;
using Hospital_Web.Services;
using Microsoft.AspNetCore.Identity;


namespace Hospital_Web.Controllers
{
    /// <summary>
    /// Controlador responsável pela gestão de utentes no sistema hospitalar.
    /// Inclui operações de CRUD (criar, ler, atualizar, apagar) e integração com identidade e email.
    /// </summary>
    public class UtentesController : Controller
    {
        private readonly Hospital_WebContext _context;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Construtor que injeta o contexto da base de dados, gestor de utilizadores e serviço de email.
        /// </summary>
        public UtentesController(
          Hospital_WebContext context,
          IEmailSender emailSender,
          UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _emailSender = emailSender;
            _userManager = userManager;
        }

        /// <summary>
        /// Apresenta a lista de utentes com os seus médicos associados.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var hospital_WebContext = _context.Utente.Include(u => u.MedicoAssociado);
            return View(await hospital_WebContext.ToListAsync());
        }

        /// <summary>
        /// Mostra os detalhes de um utente específico.
        /// </summary>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var utente = await _context.Utente
                .Include(u => u.MedicoAssociado)
                .FirstOrDefaultAsync(m => m.N_Processo == id);

            if (utente == null)
                return NotFound();

            return View(utente);
        }

        /// <summary>
        /// Exibe o formulário de criação de novo utente.
        /// </summary>
        public IActionResult Create()
        {
            ViewData["Medico_Associado_Id"] = new SelectList(_context.Medico, "N_Processo", "DisplayName");
            return View();
        }

        /// <summary>
        /// Cria um novo utente, regista-o na base de dados e cria uma conta de utilizador associada.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(...)] Utente utente)
        {
            if (!ModelState.IsValid)
                return View(utente);

            _context.Add(utente);
            await _context.SaveChangesAsync();

            var user = new ApplicationUser
            {
                UserName = utente.Email,
                Email = utente.Email,
                DeveAlterarSenha = true,
                UtenteId = utente.N_Processo
            };

            string senhaTemporaria = "Hosp@" + Guid.NewGuid().ToString("N").Substring(0, 6);
            var result = await _userManager.CreateAsync(user, senhaTemporaria);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                _context.Utente.Remove(utente);
                await _context.SaveChangesAsync();
                return View(utente);
            }

            // Enviar email de boas-vindas
            await _emailSender.SendEmailAsync(
                utente.Email,
                "Bem-vindo ao Portal do Hospital",
                $@"<p>Ola {utente.Nome},</p>
                <p>Seja bem-vindo ao nosso sistema do hospital. A sua conta foi criada com sucesso.</p>
                <p><strong>Credenciais:</strong><br>
                Email: {utente.Email}<br>
                Senha temporaria: {senhaTemporaria}</p>
                <p><a href='https://localhost:7140/Identity/Account/Login'>Entrar no sistema Hospital</a></p>
                <p>Sera solicitado a alterar a senha no primeiro login.</p>");

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Exibe o formulário de edição dos dados de um utente.
        /// </summary>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var utente = await _context.Utente.FindAsync(id);
            if (utente == null)
                return NotFound();

            ViewData["Medico_Associado_Id"] = new SelectList(_context.Medico, "N_Processo", "DisplayName", utente.Medico_Associado_Id);
            return View(utente);
        }

        /// <summary>
        /// Atualiza os dados de um utente já existente.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind(...)] Utente utente)
        {
            if (id != utente.N_Processo)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtenteExists(utente.N_Processo))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["Medico_Associado_Id"] = new SelectList(_context.Medico, "N_Processo", "DisplayName", utente.Medico_Associado_Id);
            return View(utente);
        }

        /// <summary>
        /// Mostra a confirmação de eliminação do utente.
        /// </summary>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var utente = await _context.Utente
                .Include(u => u.MedicoAssociado)
                .FirstOrDefaultAsync(m => m.N_Processo == id);

            if (utente == null)
                return NotFound();

            return View(utente);
        }

        /// <summary>
        /// Elimina o utente da base de dados, incluindo consultas e conta de utilizador associada.
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consultas = await _context.Consulta
                .Where(u => u.Utente_Id == id)
                .ToListAsync();

            foreach (var consulta in consultas)
                _context.Consulta.Remove(consulta);

            var users = await _context.Users
                .Where(u => u.UtenteId == id)
                .ToListAsync();

            foreach (var user in users)
                _context.Users.Remove(user);

            var utente = await _context.Utente.FindAsync(id);
            if (utente != null)
                _context.Utente.Remove(utente);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verifica se existe um utente com o ID indicado.
        /// </summary>
        private bool UtenteExists(int id)
        {
            return _context.Utente.Any(e => e.N_Processo == id);
        }
    }
}
