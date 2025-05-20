using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Hospital_Web.Models;
using Microsoft.AspNetCore.Identity;

namespace Hospital_Web.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR")]
    public class AdministracaoController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdministracaoController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        // GET: Administradores/EditRole/id
        public async Task<IActionResult> EditRole(string id)
        {
            if (id == null) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();
            var userRoles = await _userManager.GetRolesAsync(user);

            ViewBag.AllRoles = allRoles;
            ViewBag.UserRoles = userRoles;
            ViewBag.UserId = user.Id;

            return View();
        }

        // POST: Administradores/EditRole
        [HttpPost]
        public async Task<IActionResult> EditRole(string userId, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var currentRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, currentRoles);

            if (!result.Succeeded)
                return BadRequest("Erro ao remover roles");

            result = await _userManager.AddToRolesAsync(user, roles);
            if (!result.Succeeded)
                return BadRequest("Erro ao adicionar novas roles");

            return RedirectToAction(nameof(Index));
        }

        // POST: Administradores/Delete/id
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }
    }
}
