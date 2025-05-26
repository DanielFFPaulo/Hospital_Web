using Microsoft.AspNetCore.Identity;

namespace Hospital_Web.Models
{
    public class ApplicationUser : IdentityUser 
    {
        public bool DeveAlterarSenha { get; set; } = false;
    }
}
