using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital_Web.Data;
using Hospital_Web.Models;
using Microsoft.AspNetCore.Identity;
using Hospital_Web.Services;


namespace Hospital_Web.Controllers
{
    public class MedicosController : Controller
    {
        private readonly Hospital_WebContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public MedicosController(
            Hospital_WebContext context,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        // GET: Medicos
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Medico == null)
            {
                return Problem("Entity set 'Hospital_WebContext.Medico'  is null.");
            }

            var medicos = from m in _context.Medico
                          select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                medicos = medicos.Where(m => m.Nome!.ToUpper().Contains(searchString.ToUpper()));
            }

            return View(await medicos.ToListAsync());
        }

        // GET: Medicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medico
                .FirstOrDefaultAsync(m => m.N_Processo == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // GET: Medicos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Especialidade,Numero_de_ordem,Anos_de_experiencia,N_Processo,Nome,DataDeNascimento,sexo,Morada,Grupo_Sanguineo,Telemovel,TelemovelAlt,Email,NIF,Cod_Postal,Localidade, Grupo_Sanguineo")] Medico medico)
        {
            if (!ModelState.IsValid)
                return View(medico);

            var user = new ApplicationUser
            {
                UserName = medico.Email,
                Email = medico.Email,
                DeveAlterarSenha = true
            };

            string senhaTemporaria = "Hosp@" + Guid.NewGuid().ToString("N").Substring(0, 6);
            var result = await _userManager.CreateAsync(user, senhaTemporaria);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(medico);
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Medico.Add(medico);
                await _context.SaveChangesAsync();

                user.MedicoId = medico.N_Processo;
                await _userManager.UpdateAsync(user);

                // Atribuir Role "Medico"
                await _userManager.AddToRoleAsync(user, "Medico");


                await transaction.CommitAsync();

                string prefixo = medico.Nome.ToLower().EndsWith("a") ? "Dra." : "Dr.";

                await _emailSender.SendEmailAsync(
                    medico.Email,
                    "Bem-vindo ao Portal do Hospital",
                    $@"
            <p>Olá {prefixo} {medico.Nome},</p>
            <p>Seja bem-vindo ao nosso sistema do hospital. A sua conta foi criada com sucesso.</p>
<p><a href='https://localhost:7140/Identity/Account/Login'>Entrar no sistema Hospital</a></p>
            <p><strong>Credenciais:</strong><br>
            Email: {medico.Email}<br>
            Senha: {senhaTemporaria}</p>
<p>Será solicitado a alterar a senha no primeiro login.</p>");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                await _userManager.DeleteAsync(user);
                await transaction.RollbackAsync();
                ModelState.AddModelError("", "Erro ao criar médico. Nenhum dado foi gravado.");
                return View(medico);
            }
        }

        // GET: Medicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medico.FindAsync(id);
            if (medico == null)
            {
                return NotFound();
            }
            return View(medico);
        }

        // POST: Medicos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Especialidade,Numero_de_ordem,Anos_de_experiencia,N_Processo,Nome,DataDeNascimento,sexo,Morada,Grupo_Sanguineo,Telemovel,TelemovelAlt,Email,NIF,Cod_Postal,Localidade")] Medico medico)
        {
            if (id != medico.N_Processo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoExists(medico.N_Processo))
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
            return View(medico);
        }

        // GET: Medicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medico
                .FirstOrDefaultAsync(m => m.N_Processo == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medico = await _context.Medico.FindAsync(id);
            if (medico != null)
            {
                _context.Medico.Remove(medico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicoExists(int id)
        {
            return _context.Medico.Any(e => e.N_Processo == id);
        }
    }
}
