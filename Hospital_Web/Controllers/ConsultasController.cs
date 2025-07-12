using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital_Web.Data;
using Hospital_Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Hospital_Web.Hubs;



/// <summary>
/// Namespace dos controladores da aplicação web do hospital.
/// </summary>
namespace Hospital_Web.Controllers
{
    /// <summary>
    /// Controlador responsável pela gestão das consultas. Permite visualização e manipulação
    /// de consultas por utentes, médicos e visitantes (acesso anónimo permitido).
    /// </summary>
    [AllowAnonymous]
    public class ConsultasController : Controller
    {
        /// <summary>
        /// Contexto da base de dados.
        /// </summary>
        private readonly Hospital_WebContext _context;

        /// <summary>
        /// Gerenciador de utilizadores (Identity).
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Contexto do hub SignalR para envio de notificações.
        /// </summary>
        private readonly IHubContext<NotificationHub> _hubContext;

        /// <summary>
        /// Construtor do controlador. Injeta as dependências do contexto, UserManager e SignalR.
        /// </summary>
        public ConsultasController(Hospital_WebContext db, UserManager<ApplicationUser> userManager, IHubContext<NotificationHub> hubContext)
        {
            _context = db;
            _userManager = userManager;
            _hubContext = hubContext;
        }

        /// <summary>
        /// Mostra a lista de consultas com possibilidade de filtragem por nome do utente ou data.
        /// O conteúdo mostrado varia consoante o tipo de utilizador autenticado.
        /// </summary>
        public async Task<IActionResult> Index(string nomeUtente, DateTime? dataConsulta)
        {
            ApplicationUser? user = null;

            if (User?.Identity?.IsAuthenticated == true)
            {
                var userId = _userManager.GetUserId(User);
                user = await _context.Users
                    .Include(u => u.Utente)
                    .Include(u => u.Medico)
                    .FirstOrDefaultAsync(u => u.Id == userId);
            }

            var consultas = _context.Consulta
                .Include(c => c.Gabinete)
                .Include(c => c.Medico)
                .Include(c => c.Utente)
                .AsQueryable();

            if (user?.UtenteId != null)
            {
                consultas = consultas.Where(c => c.Utente_Id == user.UtenteId);

                if (dataConsulta.HasValue)
                    consultas = consultas.Where(c => c.Data.Date == dataConsulta.Value.Date);
            }
            else if (user?.MedicoId != null)
            {
                if (!string.IsNullOrWhiteSpace(nomeUtente))
                    consultas = consultas.Where(c => c.Utente != null && c.Utente.Nome.Contains(nomeUtente));

                if (dataConsulta.HasValue)
                    consultas = consultas.Where(c => c.Data.Date == dataConsulta.Value.Date);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(nomeUtente))
                    consultas = consultas.Where(c => c.Utente != null && c.Utente.Nome.Contains(nomeUtente));

                if (dataConsulta.HasValue)
                    consultas = consultas.Where(c => c.Data.Date == dataConsulta.Value.Date);
            }

            var listaConsultas = await consultas.ToListAsync();
            return View(listaConsultas);
        }

        /// <summary>
        /// Mostra os detalhes de uma consulta específica.
        /// </summary>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var consulta = await _context.Consulta
                .Include(c => c.Gabinete)
                .Include(c => c.Medico)
                .Include(c => c.Utente)
                .FirstOrDefaultAsync(m => m.Episodio == id);

            if (consulta == null)
                return NotFound();

            return View(consulta);
        }

        /// <summary>
        /// Mostra o formulário para criação de uma nova consulta.
        /// </summary>
        public IActionResult Create()
        {
            ViewData["Gabinete_Id"] = new SelectList(_context.Gabinete, "ID", "Denominacao");
            ViewData["Medico_Id"] = new SelectList(_context.Medico, "N_Processo", "DisplayName");
            ViewData["Utente_Id"] = new SelectList(_context.Utente, "N_Processo", "DisplayName");
            return View();
        }

        /// <summary>
        /// Regista uma nova consulta e envia notificação ao utente associado.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Episodio,Data,Hora,Diagnostico,Tratamento,Observacoes,Medico_Id,Utente_Id,Gabinete_Id")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consulta);
                await _context.SaveChangesAsync();

                var utente = await _context.Utente.FindAsync(consulta.Utente_Id);
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UtenteId == utente.N_Processo);

                if (user != null)
                {
                    string mensagem = $"Consulta marcada para {utente.Nome} no dia {consulta.Data:dd/MM/yyyy} às {consulta.Hora}.";
                    await _hubContext.Clients.User(user.Id).SendAsync("ReceberNotificacao", mensagem);
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["Gabinete_Id"] = new SelectList(_context.Gabinete, "ID", "Denominacao", consulta.Gabinete_Id);
            ViewData["Medico_Id"] = new SelectList(_context.Medico, "N_Processo", "DisplayName", consulta.Medico_Id);
            ViewData["Utente_Id"] = new SelectList(_context.Utente, "N_Processo", "DisplayName", consulta.Utente_Id);
            return View(consulta);
        }

        /// <summary>
        /// Mostra o formulário para editar uma consulta existente.
        /// </summary>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var consulta = await _context.Consulta.FindAsync(id);
            if (consulta == null)
                return NotFound();

            ViewData["Gabinete_Id"] = new SelectList(_context.Gabinete, "ID", "Denominacao", consulta.Gabinete_Id);
            ViewData["Medico_Id"] = new SelectList(_context.Medico, "N_Processo", "DisplayName", consulta.Medico_Id);
            ViewData["Utente_Id"] = new SelectList(_context.Utente, "N_Processo", "DisplayName", consulta.Utente_Id);
            return View(consulta);
        }

        /// <summary>
        /// Atualiza os dados de uma consulta existente.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Episodio,Data,Hora,Diagnostico,Tratamento,Observacoes,Medico_Id,Utente_Id,Gabinete_Id")] Consulta consulta)
        {
            if (id != consulta.Episodio)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consulta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultaExists(consulta.Episodio))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["Gabinete_Id"] = new SelectList(_context.Gabinete, "ID", "Denominacao", consulta.Gabinete_Id);
            ViewData["Medico_Id"] = new SelectList(_context.Medico, "N_Processo", "DisplayName", consulta.Medico_Id);
            ViewData["Utente_Id"] = new SelectList(_context.Utente, "N_Processo", "DisplayName", consulta.Utente_Id);
            return View(consulta);
        }

        /// <summary>
        /// Mostra a confirmação para eliminar uma consulta.
        /// </summary>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var consulta = await _context.Consulta
                .Include(c => c.Gabinete)
                .Include(c => c.Medico)
                .Include(c => c.Utente)
                .FirstOrDefaultAsync(m => m.Episodio == id);

            if (consulta == null)
                return NotFound();

            return View(consulta);
        }

        /// <summary>
        /// Elimina definitivamente uma consulta.
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consulta = await _context.Consulta.FindAsync(id);
            if (consulta != null)
                _context.Consulta.Remove(consulta);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verifica se uma consulta com o episódio especificado existe.
        /// </summary>
        private bool ConsultaExists(int id)
        {
            return _context.Consulta.Any(e => e.Episodio == id);
        }
    }
}
