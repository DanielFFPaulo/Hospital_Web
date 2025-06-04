using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class Internamento
    {
        /// <summary>
        /// Identificador único do internamento de um Utente.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Data e hora de entrada do Utente no internamento.
        /// </summary>
        [DataType(DataType.DateTime)]
        [Display(Name = "Data e hora de entrada")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataHoraEntrada { get; set; }

        /// <summary>
        /// Data e hora de saída do Utente do internamento.
        /// </summary>
        [DataType(DataType.DateTime)]
        [Display(Name = "Data e hora de saída")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? DataHoraSaida { get; set; }





        // Foreign keys
        [Display(Name = "Utente")]
        public int Utente_Id { get; set; }

        [ForeignKey("Utente_Id")]
        public virtual Utente? Utente { get; set; }



        [Display(Name = "Quarto")]
        public int Quarto_Id { get; set; }

        /// <summary>
        ///     
        /// Descrição do Quarto onde o Utente está internado.



        [ForeignKey("Quarto_Id")]
        public virtual QuartosInternagem? Quarto { get; set; }


        [Display(Name = "Consulta")]
        public int? Consulta_Id { get; set; }

        [ForeignKey("Consulta_Id")]
        public virtual Consulta? Consulta { get; set; }

    }

}
