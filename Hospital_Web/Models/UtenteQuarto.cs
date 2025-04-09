using Hospital_Web.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Web.Models
{
    public class UtenteQuarto
    {
        [Key]
        [Column(Order = 0)]
        public int Utente { get; set; }

        [Key]
        [Column(Order = 1)]
        public int Quarto { get; set; }

        // Navigation properties
        [ForeignKey("Utente")]
        public virtual Utente UtenteNavigation { get; set; }

        [ForeignKey("Quarto")]
        public virtual QuartoInternagem QuartoNavigation { get; set; }
    }
}
