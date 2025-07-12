using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class Internamento
    {
        /// <summary>
        /// Identificador unico do internamento de um Utente.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Data e hora de entrada do Utente no internamento.
        /// </summary>
        [Required(ErrorMessage = "A data e hora de entrada sao obrigatorias")]
        [Display(Name = "Data e hora de entrada")]
        [DataType(DataType.DateTime)]
        public DateTime DataHoraEntrada { get; set; }

        /// <summary>
        /// Data e hora de saida do Utente do internamento.
        /// </summary>
        [Display(Name = "Data e hora de saida")]
        [DataType(DataType.DateTime)]
        public DateTime? DataHoraSaida { get; set; }

        // Foreign keys
        [Display(Name = "Utente")]
        public int Utente_Id { get; set; }

        [ForeignKey("Utente_Id")]
        public virtual Utente? Utente { get; set; }

        [Display(Name = "Quarto")]
        public int Quarto_Id { get; set; }

        [ForeignKey("Quarto_Id")]
        public virtual QuartosInternagem? Quarto { get; set; }

        [Display(Name = "Consulta")]
        public int? Consulta_Id { get; set; }

        [ForeignKey("Consulta_Id")]
        public virtual Consulta? Consulta { get; set; }
    }

}
