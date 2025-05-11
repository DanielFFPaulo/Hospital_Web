using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class QuartosInternagem : Sala
    {
        [Required]
        [StringLength(200)]
        public string Descricao { get; set; }

        [Required]
        [StringLength(50)]
        public string Tipo { get; set; }

        // Navigation property
        public virtual ICollection<Internamento> Internamentos { get; set; } = [];
    }
}
