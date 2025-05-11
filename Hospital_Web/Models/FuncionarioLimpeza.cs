using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class FuncionarioLimpeza : Pessoa
    {
        [Required]
        [StringLength(20)]
        public string Turno { get; set; }

        [StringLength(20)]
        public string Tamanho_Uniforme { get; set; }

        [DataType(DataType.Date)]
        public DateTime Data_de_contratacao { get; set; }

        [StringLength(500)]
        public string Certificacoes { get; set; }

        // Navigation property
        public virtual ICollection<LimpezaSala> LimpezasDeSala { get; set; } = [];
    }

}
