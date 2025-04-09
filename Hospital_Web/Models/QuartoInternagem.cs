using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class QuartoInternagem
    {
        [Key]
        [Display(Name = "Identificador")]
        public int N_Identificador { get; set; }

        public string Descricao { get; set; }

        public string Tipo { get; set; }

        public int? Capacidade { get; set; }

        public bool Ocupado { get; set; }

        // Navigation properties
        public virtual ICollection<UtenteQuarto> UtentesQuartos { get; set; }
    }
}
