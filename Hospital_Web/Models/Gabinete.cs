using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class Gabinete : Sala
    {
        /// <summary>
        /// Descrição do Gabinete.
        /// </summary>
        [Required (ErrorMessage = " A {0} é de preenchimento obrigatório")]
        [StringLength(200)]
        [Display(Name = "Descrição do Gabinete")]
        [DataType(DataType.MultilineText)]
        [DisplayFormat(NullDisplayText = "Sem descrição")]
        [UIHint("TextArea")]
        [RegularExpression(@"^[a-zA-Zà-üÀ-Ü0-9\s.,;:!?()\-]+$", ErrorMessage = "A {0} contém caracteres inválidos.")]
        public string Descricao { get; set; } = string.Empty;

        /// <summary>
        /// Equipamento disponível no gabinete.
        /// </summary>
        [StringLength(500)]
        [Display(Name = "Equipamento")]
        [DataType(DataType.MultilineText)]
        [DisplayFormat(NullDisplayText = "Sem equipamento")]
        [UIHint("TextArea")]
        [RegularExpression(@"^[a-zA-Zà-üÀ-Ü0-9\s.,;:!?()\-]+$", ErrorMessage = "O {0} contém caracteres inválidos.")]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public string Equipamento { get; set; } = string.Empty;

        /// <summary>
        /// Disponibilidade do gabinete.
        /// </summary>
        [Display(Name = "Disponível")]
        public bool Disponivel { get; set; } = false;

        // Navigation properties
        public virtual ICollection<Consulta> Consultas { get; set; } = [];
    }
}
