using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class Medico : Pessoa
    {
        [Required]
        [StringLength(100)]
        public string Especialidade { get; set; }

        public int Numero_de_ordem { get; set; }

        public int Anos_de_experiencia { get; set; }

        // Navigation property
        public virtual ICollection<Consulta> Consultas { get; set; } = [];

        // Navigation property for Utentes assigned to this Medico
        public virtual ICollection<Utente> UtentesAssociados { get; set; } = [];
    }
}
