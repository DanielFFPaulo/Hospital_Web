using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class Gabinete : Sala
    {
        [Required]
        [StringLength(200)]
        public string Descricao { get; set; }

        [StringLength(500)]
        public string Equipamento { get; set; }

        public bool Disponivel { get; set; }

        // Navigation properties
        public virtual ICollection<Consulta> Consultas { get; set; } = [];
    }
}
