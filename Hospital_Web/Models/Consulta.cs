using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class Consulta
    {
        /// <summary>
        /// Identificador único da consulta.
        /// </summary>
        [Key]
        public int Episodio { get; set; }

        /// <summary>
        /// Data da consulta.
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Data da consulta")]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public DateTime Data { get; set; }
        /// <summary>
        /// Hora exata do inicio da consulta.
        /// </summary>
        [DataType(DataType.Time)]
        [Display(Name = "Hora da consulta")]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
        public TimeSpan Hora { get; set; }
        /// <summary>
        /// Diagostico da consulta.
        /// </summary>
        [StringLength(500)]
        [Display(Name = "Diagnóstico")]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [DataType(DataType.MultilineText)]
        [DisplayFormat(NullDisplayText = "Sem diagnóstico")]
        [UIHint("TextArea")]
        [RegularExpression(@"^[a-zA-Zà-üÀ-Ü0-9\s.,;:!?()\-]+$", ErrorMessage = "O {0} contém caracteres inválidos.")]
        public string Diagnostico { get; set; } = string.Empty;
        /// <summary>
        /// Tratamento a ser seguido.
        /// </summary>
        [StringLength(500)]
        [Display(Name = "Tratamento")]
        [DataType(DataType.MultilineText)]
        [DisplayFormat(NullDisplayText = "Sem tratamento")]
        [UIHint("TextArea")]
        [RegularExpression(@"^[a-zA-Zà-üÀ-Ü0-9\s.,;:!?()\-]+$", ErrorMessage = "O {0} contém caracteres inválidos.")]
        public string Tratamento { get; set; } = string.Empty;






        /// <summary>
        /// Observações adicionais sobre a consulta.
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "Observações")]
        [DataType(DataType.MultilineText)]
        [DisplayFormat(NullDisplayText = "Sem observações")]
        [UIHint("TextArea")]
        [RegularExpression(@"^[a-zA-Zà-üÀ-Ü0-9\s.,;:!?()\-]+$", ErrorMessage = "O {0} contém caracteres inválidos.")]
        public string? Observacoes { get; set; }

        // Foreign keys
        /// <summary>
        /// Identificador do médico responsável pela consulta.
        /// </summary>
        [Display(Name = "Medico")]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public int Medico_Id { get; set; }

        [ForeignKey("Medico_Id")]
        public virtual Medico? Medico { get; set; }
        /// <summary>
        /// Identificador do utente associado à consulta.
        /// </summary>
        [Display(Name = "Utente")]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public int Utente_Id { get; set; }

        [ForeignKey("Utente_Id")]
        public virtual Utente? Utente { get; set; }
        /// <summary>
        /// Identificador do gabinete onde a consulta ocorre.
        /// </summary>
        [Display(Name = "Gabinete")]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public int Gabinete_Id { get; set; }

        [ForeignKey("Gabinete_Id")]
        public virtual Gabinete? Gabinete { get; set; }

  

    }

}
