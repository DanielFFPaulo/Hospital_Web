using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class Utente : Pessoa
    {



        /// <summary>
        /// Estado clínico do Utente.
        /// </summary>

        [Display(Name = "Estado clínico")]
        [DataType(DataType.MultilineText)]
        [DisplayFormat(NullDisplayText = "Sem estado clínico")]
        [UIHint("TextArea")]
        [RegularExpression(@"^[a-zA-Zà-üÀ-Ü0-9\s.,;:!?()\-]+$", ErrorMessage = "O {0} contém caracteres inválidos.")]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public string Estado_clinico { get; set; } = string.Empty;



        /// <summary>
        /// Alergias do Utente.
        /// </summary>
        [StringLength(200)]
        [Display(Name = "Alergias")]
        [DataType(DataType.MultilineText)]
        [DisplayFormat(NullDisplayText = "Sem alergias")]
        [UIHint("TextArea")]
        [RegularExpression(@"^[a-zA-Zà-üÀ-Ü0-9\s.,;:!?()\-]+$", ErrorMessage = "A {0} contém caracteres inválidos.")]
        public string? Alergias { get; set; } = string.Empty;

        /// <summary>
        /// Nome do seguro do utente.
        /// </summary>
        [StringLength(100)]
        public string Seguro_de_Saude { get; set; } = string.Empty;


        /// <summary>
        /// Data de registo do Utente.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime Data_de_Registo { get; set; } = DateTime.Now;

        // Foreign key for Medico Associado

        /// <summary>
        /// Identificador do médico associado ao Utente. (Opcional)
        /// </summary>
        public int? Medico_Associado_Id { get; set; }

        [ForeignKey("Medico_Associado_Id")]
        public virtual Medico? MedicoAssociado { get; set; }

        // Navigation properties
        public virtual ICollection<Consulta> Consultas { get; set; } = [];
        public virtual ICollection<Internamento> Internamentos { get; set; } = [];

    }
}
