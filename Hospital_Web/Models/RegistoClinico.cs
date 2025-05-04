using Hospital_Web.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Web.Models
{
    public class RegistoClinico
    {
        public int Utente { get; set; }

        public int Medico { get; set; }

        [Key]
        public int Episodio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Data { get; set; }

        public string Diagnostico { get; set; }

        public string Tratamento { get; set; }

        public string Observacoes { get; set; }

        // Navigation properties
        [ForeignKey("Utente")]
        public virtual Utente UtenteNavigation { get; set; }

        [ForeignKey("Medico")]
        public virtual Medico MedicoNavigation { get; set; }

        [ForeignKey("Episodio")]
        public virtual Consulta ConsultaNavigation { get; set; }
    }
}
