using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class Gabinete
    {
        [Key]
        [Display(Name = "Identificador")]
        public int N_Identificador { get; set; }

        public string Descricao { get; set; }

        public string Equipamento { get; set; }

        public bool Disponivel { get; set; }

        // Navigation properties
        public virtual ICollection<Consulta> Consultas { get; set; }
    }
}
