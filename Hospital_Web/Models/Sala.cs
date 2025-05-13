using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class Sala
    {
        /// <summary>
        /// Identificador único da sala.
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// Bloco da sala.
        /// </summary>
        [Required(ErrorMessage = "É necessário definir o {0}")]
        [StringLength(1)]
        [Display(Name = "Bloco")]
        [RegularExpression(@"^[A-Z]$", ErrorMessage = "O {0} deve ser uma letra maiúscula de A-Z.")]
        public string Bloco { get; set; } = string.Empty;

        [Required(ErrorMessage = "É necessário definir o {0}")]
        public int Andar { get; set; }

        [Required(ErrorMessage = "É necessário definir o {0}")]
        public int Numero { get; set; }

        // Navigation property for limpezas
        public virtual ICollection<LimpezaSala> LimpezasDeSala { get; set; } = [];

        // Discriminator property will be added automatically by EF Core
    }
}
