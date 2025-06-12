using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital_Web.Data;
using Hospital_Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;


namespace Hospital_Web.Controllers
{
    [AllowAnonymous]
    public class ConsultasController : Controller
    {
        private readonly Hospital_WebContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ConsultasController(Hospital_WebContext db, UserManager<ApplicationUser> userManager)
        {
            _context = db;
            _userManager = userManager;
        }


        // GET: Consultas
        public async Task<IActionResult> Index(string nomeUtente, DateTime? dataConsulta)
        {
            ApplicationUser user = null;

            if (User.Identity.IsAuthenticated)
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
                // Utente autenticado: mostra apenas as suas consultas
                consultas = consultas.Where(c => c.Utente_Id == user.UtenteId);

                if (dataConsulta.HasValue)
                    consultas = consultas.Where(c => c.Data.Date == dataConsulta.Value.Date);
            }
            else if (user?.MedicoId != null)
            {
                // Médico autenticado: pode filtrar
                if (!string.IsNullOrWhiteSpace(nomeUtente))
                    consultas = consultas.Where(c => c.Utente.Nome.Contains(nomeUtente));

                if (dataConsulta.HasValue)
                    consultas = consultas.Where(c => c.Data.Date == dataConsulta.Value.Date);
            }
            else
            {
                // Visitante não autenticado: pode filtrar também
                if (!string.IsNullOrWhiteSpace(nomeUtente))
                    consultas = consultas.Where(c => c.Utente.Nome.Contains(nomeUtente));

                if (dataConsulta.HasValue)
                    consultas = consultas.Where(c => c.Data.Date == dataConsulta.Value.Date);
            }

            var listaConsultas = await consultas.ToListAsync();
            return View(listaConsultas);
        }

        





        // GET: Consultas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consulta
                .Include(c => c.Gabinete)
                .Include(c => c.Medico)
                .Include(c => c.Utente)
                .FirstOrDefaultAsync(m => m.Episodio == id);
            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        // GET: Consultas/Create
        public IActionResult Create()
        {
            ViewData["Gabinete_Id"] = new SelectList(_context.Gabinete, "ID", "Bloco");
            ViewData["Medico_Id"] = new SelectList(_context.Medico, "N_Processo", "Nome");
            ViewData["Utente_Id"] = new SelectList(_context.Utente, "N_Processo", "Nome");
            return View();
        }

        // POST: Consultas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Episodio,Data,Hora,Diagnostico,Tratamento,Observacoes,Medico_Id,Utente_Id,Gabinete_Id")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consulta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Gabinete_Id"] = new SelectList(_context.Gabinete, "ID", "Bloco", consulta.Gabinete_Id);
            ViewData["Medico_Id"] = new SelectList(_context.Medico, "N_Processo", "NIF", consulta.Medico_Id);
            ViewData["Utente_Id"] = new SelectList(_context.Utente, "N_Processo", "NIF", consulta.Utente_Id);
            return View(consulta);
        }

        // GET: Consultas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consulta.FindAsync(id);
            if (consulta == null)
            {
                return NotFound();
            }
            ViewData["Gabinete_Id"] = new SelectList(_context.Gabinete, "ID", "Bloco", consulta.Gabinete_Id);
            ViewData["Medico_Id"] = new SelectList(_context.Medico, "N_Processo", "NIF", consulta.Medico_Id);
            ViewData["Utente_Id"] = new SelectList(_context.Utente, "N_Processo", "NIF", consulta.Utente_Id);
            return View(consulta);
        }

        // POST: Consultas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Episodio,Data,Hora,Diagnostico,Tratamento,Observacoes,Medico_Id,Utente_Id,Gabinete_Id")] Consulta consulta)
        {
            if (id != consulta.Episodio)
            {
                return NotFound();
            }

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
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Gabinete_Id"] = new SelectList(_context.Gabinete, "ID", "Bloco", consulta.Gabinete_Id);
            ViewData["Medico_Id"] = new SelectList(_context.Medico, "N_Processo", "NIF", consulta.Medico_Id);
            ViewData["Utente_Id"] = new SelectList(_context.Utente, "N_Processo", "NIF", consulta.Utente_Id);
            return View(consulta);
        }

        // GET: Consultas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consulta
                .Include(c => c.Gabinete)
                .Include(c => c.Medico)
                .Include(c => c.Utente)
                .FirstOrDefaultAsync(m => m.Episodio == id);
            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        // POST: Consultas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consulta = await _context.Consulta.FindAsync(id);
            if (consulta != null)
            {
                _context.Consulta.Remove(consulta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultaExists(int id)
        {
            return _context.Consulta.Any(e => e.Episodio == id);
        }
    }
}
