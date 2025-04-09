using Hospital_Web.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Web.Models
{
    public class Medico
    {
        [Key]
        [ForeignKey("Funcionario")]
        [Display(Name = "ID Médico")]
        public int ID_Medico { get; set; }

        [StringLength(100)]
        public string Especialidade { get; set; }

        // Navigation properties
        public virtual Funcionario Funcionario { get; set; }
        public virtual ICollection<Consulta> Consultas { get; set; }
        public virtual ICollection<RegistoClinico> RegistosClinicos { get; set; }
    }
}