using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Web.Models
{
    public class Utente
    {
        [Key]
        [Display(Name = "Número de Processo")]
        public int N_Processo { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        public int? Idade { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Data_Nascimento { get; set; }

        [StringLength(255)]
        public string Morada { get; set; }

        [Display(Name = "Estado Clínico")]
        public string Estado_Clinico { get; set; }

        [StringLength(15)]
        public string Telefone1 { get; set; }

        [StringLength(15)]
        public string Telefone2 { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(15)]
        public string NIF { get; set; }

        // Navigation properties
        public virtual ICollection<Consulta> Consultas { get; set; }
        public virtual ICollection<RegistoClinico> RegistosClinicos { get; set; }
        public virtual ICollection<UtenteQuarto> UtentesQuartos { get; set; }
    }
}