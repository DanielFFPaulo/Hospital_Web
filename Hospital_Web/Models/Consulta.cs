using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Web.Models
{
    public class Consulta
    {
        [Key]
        public int Episodio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Data { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan? Hora { get; set; }

        public string Descricao { get; set; }

        [Display(Name = "Médico")]
        public int ID_Medico { get; set; }

        [Display(Name = "Utente")]
        public int ID_Utente { get; set; }

        public int Gabinete { get; set; }

        // Navigation properties
        [ForeignKey("ID_Medico")]
        public virtual Medico Medico { get; set; }

        [ForeignKey("ID_Utente")]
        public virtual Utente Utente { get; set; }

        [ForeignKey("Gabinete")]
        public virtual Gabinete GabineteNavigation { get; set; }

        public virtual ICollection<RegistoClinico> RegistosClinicos { get; set; }
    }
}
