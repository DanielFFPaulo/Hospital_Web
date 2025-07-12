using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class Gabinete : Sala
    {
        /// <summary>
        /// Descricao do Gabinete.
        /// </summary>
        [Required (ErrorMessage = " A {0} e de preenchimento obrigatorio")]
        [StringLength(200)]
        [Display(Name = "Descricao do Gabinete")]
        [DataType(DataType.MultilineText)]
        [DisplayFormat(NullDisplayText = "Sem descricao")]
        [UIHint("TextArea")]
        [RegularExpression(@"^[a-zA-Za-üa-Ü0-9\s.,;:!?()\-]+$", ErrorMessage = "A {0} contem caracteres invalidos.")]
        public string Descricao { get; set; } = string.Empty;

        /// <summary>
        /// Equipamento disponivel no gabinete.
        /// </summary>
        [StringLength(500)]
        [Display(Name = "Equipamento")]
        [DataType(DataType.MultilineText)]
        [DisplayFormat(NullDisplayText = "Sem equipamento")]
        [UIHint("TextArea")]
        [RegularExpression(@"^[a-zA-Za-üa-Ü0-9\s.,;:!?()\-]+$", ErrorMessage = "O {0} contem caracteres invalidos.")]
        [Required(ErrorMessage = "O {0} e de preenchimento obrigatorio")]
        public string Equipamento { get; set; } = string.Empty;

        /// <summary>
        /// Disponibilidade do gabinete.
        /// </summary>
        [Display(Name = "Disponivel")]
        public bool Disponivel { get; set; } = false;

        // Navigation properties
        public virtual ICollection<Consulta> Consultas { get; set; } = [];
    }
}
