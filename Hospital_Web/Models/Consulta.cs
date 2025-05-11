using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class Consulta
    {
        [Key]
        public int Episodio { get; set; }
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan Hora { get; set; }

        [StringLength(500)]
        public string Diagnostico { get; set; }

        [StringLength(500)]
        public string Tratamento { get; set; }

        [StringLength(1000)]
        public string Observacoes { get; set; }

        // Foreign keys
        public int Medico_Id { get; set; }

        [ForeignKey("Medico_Id")]
        public virtual Medico? Medico { get; set; }

        public int Utente_Id { get; set; }

        [ForeignKey("Utente_Id")]
        public virtual Utente? Utente { get; set; }

        public int Gabinete_Id { get; set; }

        [ForeignKey("Gabinete_Id")]
        public virtual Gabinete? Gabinete { get; set; }
    }

}
