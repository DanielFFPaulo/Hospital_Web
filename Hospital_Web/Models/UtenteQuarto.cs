using Hospital_Web.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Web.Models
{
    [PrimaryKey(nameof(Utente), nameof(Quarto))]    
    public class UtenteQuarto
    {
        [Column(Order = 0)]
        public int Utente { get; set; }
        [Column(Order = 1)]
        public int Quarto { get; set; }

        // Navigation properties
        [ForeignKey("Utente")]
        public virtual Utente UtenteNavigation { get; set; }

        [ForeignKey("Quarto")]
        public virtual QuartoInternagem QuartoNavigation { get; set; }
    }
}
