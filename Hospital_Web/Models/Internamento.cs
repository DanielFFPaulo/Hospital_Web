using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class Internamento
    {
        [Key]
        public int ID { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Data_Hora_Entrada { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Data_Hora_Saida { get; set; }

        // Foreign keys
        public int Utente_Id { get; set; }

        [ForeignKey("Utente_Id")]
        public virtual Utente? Utente { get; set; }

        public int Quarto_Id { get; set; }

        [ForeignKey("Quarto_Id")]
        public virtual QuartosInternagem? Quarto { get; set; }

        public int? Consulta_Id { get; set; }

        [ForeignKey("Consulta_Id")]
        public virtual Consulta? Consulta { get; set; }
    }

}
