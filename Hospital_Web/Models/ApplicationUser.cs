using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Hospital_Web.Models
{
    public class ApplicationUser : IdentityUser 
    {
        public bool DeveAlterarSenha { get; set; } = false;

        public int? UtenteId { get; set; }  

        [ForeignKey("UtenteId")]
        public Utente? Utente { get; set; }

        public int? MedicoId { get; set; }

        public virtual Medico? Medico { get; set; }

        public int? FuncionarioLimpezaId { get; set; }
        public FuncionarioLimpeza? FuncionarioLimpeza { get; set; }
    }
}
