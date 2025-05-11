using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class Sala
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        public string Bloco { get; set; }

        public int Andar { get; set; }

        public int Numero { get; set; }

        // Navigation property for limpezas
        public virtual ICollection<LimpezaSala> LimpezasDeSala { get; set; } = [];

        // Discriminator property will be added automatically by EF Core
    }
}
