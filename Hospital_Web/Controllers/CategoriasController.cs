using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital_Web.Data;
using Hospital_Web.Models;

namespace Hospital_Web.Controllers
{
    [Authorize(Roles = "MÉDICO,ENFERMEIRO")] // Só médicos e enfermeiros podem aceder
    public class CategoriasController : Controller
    {
        private readonly Hospital_WebContext _context;

        public CategoriasController(Hospital_WebContext context)
        {
            _context = context;
        }

        // GET: Consultas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Consulta.ToListAsync());
        }

        // GET: Consultas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var consulta = await _context.Consulta.FirstOrDefaultAsync(m => m.Episodio == id);
            if (consulta == null) return NotFound();

            return View(consulta);
        }

        // GET: Consultas/Create
        [Authorize(Roles = "MÉDICO")] // Apenas médicos podem criar
        public IActionResult Create()
        {
            return View();
        }

        // POST: Consultas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "MÉDICO")] // Apenas médicos podem submeter
        public async Task<IActionResult> Create([Bind("Id,Data,UtenteId")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consulta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consulta);
        }

        // etc...
    }
}
