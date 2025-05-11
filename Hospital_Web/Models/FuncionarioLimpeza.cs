using Hospital_Web.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Web.Models
{
    public class FuncionarioLimpeza
    {
        [Key]
        [ForeignKey("Funcionario")]
        [Display(Name = "ID Funcionário")]
        public int ID_Funcionario { get; set; }

        [StringLength(50)]
        public string Turno { get; set; }

        // Navigation properties
        public virtual Funcionario Funcionario { get; set; }
    }
}
