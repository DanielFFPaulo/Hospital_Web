using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class Funcionario
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        public int? Idade { get; set; }

        [StringLength(255)]
        public string Morada { get; set; }

        [StringLength(15)]
        public string Telefone1 { get; set; }

        [StringLength(15)]
        public string Telefone2 { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100)]
        public string Licenciatura { get; set; }

        [Display(Name = "Estado Civil")]
        [StringLength(50)]
        public string Estado_Civil { get; set; }

        [StringLength(15)]
        public string NIF { get; set; }

        // Navigation properties
        public virtual Medico Medico { get; set; }
        public virtual FuncionarioLimpeza FuncionarioLimpeza { get; set; }
        public virtual ICollection<LimpezaSala> LimpezasSalas { get; set; }
    }
}

