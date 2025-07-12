using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class Utente : Pessoa
    {



        /// <summary>
        /// Estado clinico do Utente.
        /// </summary>
        [Display(Name = "Estado clinico")]
        [DataType(DataType.MultilineText)]
        [DisplayFormat(NullDisplayText = "Sem estado clinico")]
        [UIHint("TextArea")]
        [RegularExpression(@"^[a-zA-Za-üa-Ü0-9\s.,;:!?()\-]+$", ErrorMessage = "O {0} contem caracteres invalidos.")]
        [Required(ErrorMessage = "O {0} e de preenchimento obrigatorio")]
        public string Estado_clinico { get; set; } = string.Empty;



        /// <summary>
        /// Alergias do Utente.
        /// </summary>
        [StringLength(200)]
        [Display(Name = "Alergias")]
        [DataType(DataType.MultilineText)]
        [DisplayFormat(NullDisplayText = "Sem alergias")]
        [UIHint("TextArea")]
        [RegularExpression(@"^[a-zA-Za-üa-Ü0-9\s.,;:!?()\-]+$", ErrorMessage = "A {0} contem caracteres invalidos.")]
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
        /// Identificador do medico associado ao Utente. (Opcional)
        /// </summary>
        public int? Medico_Associado_Id { get; set; }

        /// <summary>
        ///     
        /// Objeto usado para referenciar o utente em listas.
        /// </summary>
        [NotMapped]
        public string DisplayName { get { return $"{N_Processo}- ({Nome})"; } }

        [ForeignKey("Medico_Associado_Id")]
        public virtual Medico? MedicoAssociado { get; set; }

        // Navigation properties
        public virtual ICollection<Consulta> Consultas { get; set; } = [];
        public virtual ICollection<Internamento> Internamentos { get; set; } = [];

    }
}
