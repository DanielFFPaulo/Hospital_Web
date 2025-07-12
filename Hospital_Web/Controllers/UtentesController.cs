using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital_Web.Data;
using Hospital_Web.Models;
using Hospital_Web.Services;
using Microsoft.AspNetCore.Identity;


namespace Hospital_Web.Controllers
{
    public class UtentesController : Controller
    {
        private readonly Hospital_WebContext _context;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;

        public UtentesController(
          Hospital_WebContext context,
          IEmailSender emailSender,
          UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _emailSender = emailSender;
            _userManager = userManager;
        }

        // GET: Utentes
        public async Task<IActionResult> Index()
        {
            var hospital_WebContext = _context.Utente.Include(u => u.MedicoAssociado);
            return View(await hospital_WebContext.ToListAsync());
        }

        // GET: Utentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utente = await _context.Utente
                .Include(u => u.MedicoAssociado)
                .FirstOrDefaultAsync(m => m.N_Processo == id);
            if (utente == null)
            {
                return NotFound();
            }

            return View(utente);
        }

        // GET: Utentes/Create
        public IActionResult Create()
        {
            ViewData["Medico_Associado_Id"] = new SelectList(_context.Medico, "N_Processo", "DisplayName");
            return View();
        }

        // POST: Utentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Estado_clinico,Grupo_Sanguineo,Alergias,Seguro_de_Saude,Data_de_Registo,Medico_Associado_Id,N_Processo,Nome,Idade,Data_de_Nascimento,Morada,Telemovel,TelemovelAlt,Email,NIF,Cod_Postal,Localidade")] Utente utente)
        {
            if (!ModelState.IsValid)
                return View(utente);

            // 1. Criar o Utente na base de dados
            _context.Add(utente);
            await _context.SaveChangesAsync();

            // 2. Criar utilizador no Identity com UtenteId
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

                // Remover utente se criacao de utilizador falhar
                _context.Utente.Remove(utente);
                await _context.SaveChangesAsync();

                return View(utente);
            }

            // 3. Adicionar ao role "Utente"
            //await _userManager.AddToRoleAsync(user, "Utente");

            // 4. Enviar email
            await _emailSender.SendEmailAsync(
                utente.Email,
                "Bem-vindo ao Portal do Hospital",
                $@"
<p>Ola {utente.Nome},</p>
<p>Seja bem-vindo ao nosso sistema do hospital. A sua conta foi criada com sucesso.</p>
<p><strong>Credenciais:</strong><br>
Email: {utente.Email}<br>
Senha temporaria: {senhaTemporaria}</p>
<p><a href='https://localhost:7140/Identity/Account/Login'>Entrar no sistema Hospital</a></p>
<p>Sera solicitado a alterar a senha no primeiro login.</p>");

            return RedirectToAction(nameof(Index));
        }

        // GET: Utentes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utente = await _context.Utente.FindAsync(id);
            if (utente == null)
            {
                return NotFound();
            }

            ViewData["Medico_Associado_Id"] = new SelectList(_context.Medico, "N_Processo", "DisplayName", utente.Medico_Associado_Id);
            return View(utente);
        }



        // POST: Utentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("N_Processo,Nome,DataDeNascimento,genero,Morada,Localidade,Cod_Postal,Telemovel,TelemovelAlt,Email,NIF,Grupo_Sanguineo,Estado_clinico,Alergias,Seguro_de_Saude,Data_de_Registo,Medico_Associado_Id")] Utente utente)

        {
            if (id != utente.N_Processo)
            {
                return NotFound();
            }

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
            ViewData["Medico_Associado_Id"] = new SelectList(_context.Medico, "N_Processo", "DisplayName", utente.Medico_Associado_Id);
            return View(utente);
        }

        // GET: Utentes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utente = await _context.Utente
                .Include(u => u.MedicoAssociado)
                .FirstOrDefaultAsync(m => m.N_Processo == id);
            if (utente == null)
            {
                return NotFound();
            }

            return View(utente);
        }

        // POST: Utentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var consultas = await _context.Consulta
                .Where(u => u.Utente_Id == id)
                .ToListAsync();

            foreach (var consulta in consultas)
            {
                _context.Consulta.Remove(consulta);
            }

            var users = await _context.Users
                .Where(u => u.UtenteId == id)
                .ToListAsync();

            foreach (var user in users)
            {
                _context.Users.Remove(user);
            }

            var utente = await _context.Utente.FindAsync(id);
            if (utente != null)
            {
                _context.Utente.Remove(utente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtenteExists(int id)
        {
            return _context.Utente.Any(e => e.N_Processo == id);
        }
    }
}
