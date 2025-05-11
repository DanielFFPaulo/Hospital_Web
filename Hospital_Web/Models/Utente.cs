using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class Utente : Pessoa
    {
        [StringLength(200)]
        public string Estado_clinico { get; set; }
        [StringLength(10)]
        public string Grupo_Sanguineo { get; set; }

        [StringLength(200)]
        public string Alergias { get; set; }

        [StringLength(100)]
        public string Seguro_de_Saude { get; set; }

        [DataType(DataType.Date)]
        public DateTime Data_de_Registo { get; set; }

        // Foreign key for Medico Associado
        public int? Medico_Associado_Id { get; set; }

        [ForeignKey("Medico_Associado_Id")]
        public virtual Medico? MedicoAssociado { get; set; }

        // Navigation properties
        public virtual ICollection<Consulta> Consultas { get; set; } = [];
        public virtual ICollection<Internamento> Internamentos { get; set; } = [];
    }
}
