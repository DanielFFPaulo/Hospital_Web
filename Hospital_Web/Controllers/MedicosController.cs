using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital_Web.Data;
using Hospital_Web.Models;
using Microsoft.AspNetCore.Identity;
using Hospital_Web.Services;


namespace Hospital_Web.Controllers
{
    /// <summary>
    /// Controlador responsável pela gestão dos médicos no sistema hospitalar.
    /// Permite criar, editar, visualizar, pesquisar e remover médicos, além de criar contas associadas.
    /// </summary>
    public class MedicosController(
        Hospital_WebContext context,
        UserManager<ApplicationUser> userManager,
        IEmailSender emailSender) : Controller
    {
        private readonly Hospital_WebContext _context = context;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IEmailSender _emailSender = emailSender;

        /// <summary>
        /// Lista os médicos existentes, com opção de pesquisa por nome.
        /// </summary>
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Medico == null)
                return Problem("Entity set 'Hospital_WebContext.Medico' is null.");

            var medicos = from m in _context.Medico
                          select m;

            if (!String.IsNullOrEmpty(searchString))
                medicos = medicos.Where(m => m.Nome!.ToUpper().Contains(searchString.ToUpper()));

            return View(await medicos.ToListAsync());
        }

        /// <summary>
        /// Mostra os detalhes de um médico específico.
        /// </summary>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var medico = await _context.Medico.FirstOrDefaultAsync(m => m.N_Processo == id);
            if (medico == null)
                return NotFound();

            return View(medico);
        }

        /// <summary>
        /// Devolve o formulário para criar um novo médico.
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Cria um novo médico, a conta associada no sistema, e envia email com credenciais.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Especialidade,Numero_de_ordem,Anos_de_experiencia,N_Processo,Nome,DataDeNascimento,sexo,Morada,Grupo_Sanguineo,Telemovel,TelemovelAlt,Email,NIF,Cod_Postal,Localidade, Grupo_Sanguineo")] Medico medico)
        {
            if (!ModelState.IsValid)
                return View(medico);

            // Criação do utilizador com senha temporária
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
                // Gravar médico na base de dados
                _context.Medico.Add(medico);
                await _context.SaveChangesAsync();

                // Associar médico ao utilizador criado
                user.MedicoId = medico.N_Processo;
                await _userManager.UpdateAsync(user);

                // Atribuir o role "Medico"
                await _userManager.AddToRoleAsync(user, "Medico");

                await transaction.CommitAsync();

                // Enviar email com credenciais
                string prefixo = medico.Nome.ToLower().EndsWith("a") ? "Dra." : "Dr.";
                await _emailSender.SendEmailAsync(
                    medico.Email,
                    "Bem-vindo ao Portal do Hospital",
                    $@"
                    <p>Ola {prefixo} {medico.Nome},</p>
                    <p>Seja bem-vindo ao nosso sistema do hospital. A sua conta foi criada com sucesso.</p>
                    <p><a href='https://localhost:7140/Identity/Account/Login'>Entrar no sistema Hospital</a></p>
                    <p><strong>Credenciais:</strong><br>
                    Email: {medico.Email}<br>
                    Senha: {senhaTemporaria}</p>
                    <p>Sera solicitado a alterar a senha no primeiro login.</p>");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Rollback se falhar
                await _userManager.DeleteAsync(user);
                await transaction.RollbackAsync();
                ModelState.AddModelError("", "Erro ao criar médico. Nenhum dado foi gravado.");
                return View(medico);
            }
        }

        /// <summary>
        /// Devolve o formulário para editar um médico.
        /// </summary>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var medico = await _context.Medico.FindAsync(id);
            if (medico == null)
                return NotFound();

            return View(medico);
        }

        /// <summary>
        /// Guarda as alterações feitas a um médico existente.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Especialidade,Numero_de_ordem,Anos_de_experiencia,N_Processo,Nome,DataDeNascimento,sexo,Morada,Grupo_Sanguineo,Telemovel,TelemovelAlt,Email,NIF,Cod_Postal,Localidade")] Medico medico)
        {
            if (id != medico.N_Processo)
                return NotFound();

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
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }

        /// <summary>
        /// Mostra os dados de um médico antes de o apagar.
        /// </summary>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var medico = await _context.Medico.FirstOrDefaultAsync(m => m.N_Processo == id);
            if (medico == null)
                return NotFound();

            return View(medico);
        }

        /// <summary>
        /// Apaga um médico e o(s) utilizador(es) associados à sua conta.
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Remover utilizadores associados
            var users = await _context.Users
                .Where(u => u.MedicoId == id)
                .ToListAsync();

            foreach (var user in users)
                _context.Users.Remove(user);

            // Remover médico
            var medico = await _context.Medico.FindAsync(id);
            if (medico != null)
                _context.Medico.Remove(medico);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verifica se existe um médico com o N_Processo fornecido.
        /// </summary>
        private bool MedicoExists(int id)
        {
            return _context.Medico.Any(e => e.N_Processo == id);
        }
    }
}

