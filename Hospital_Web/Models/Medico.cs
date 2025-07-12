using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Web.Models
{
    public class Medico : Pessoa
    {
        /// <summary>
        /// Especialidade do medico.
        /// </summary>
        [Required(ErrorMessage = "A {0} e obrigatoria")]
        [StringLength(100)]
        public string Especialidade { get; set; } = string.Empty;


        /// <summary>
        ///     Numero de Ordem do medico.
        /// </summary>

        [Required(ErrorMessage = "A {0} e obrigatoria")]
        [StringLength(100)]
        [Display(Name = "Numero de Ordem")]
        [RegularExpression(@"^[0-9]{1,10}$", ErrorMessage = "O {0} deve conter apenas numeros.")]
        [DisplayFormat(NullDisplayText = "HC-")]
        public string Numero_de_ordem { get; set; } = string.Empty;

        /// <summary>
        /// Numero de anos de experiencia do medico.
        /// </summary>
        [Required(ErrorMessage = "Os {0} devem ser definidos")]
        [Display(Name = "Anos de experiencia")]
        [RegularExpression(@"^[0-9]{1,2}$", ErrorMessage = "O {0} deve conter apenas numeros.")]
        [Range(0, 99, ErrorMessage = "O {0} deve estar entre {1} e {2}")]
        public int Anos_de_experiencia { get; set; }


        [NotMapped]
        public string DisplayName { get{ return "CT-" + Numero_de_ordem + "\t" + Nome; } }

        // Navigation property
        public virtual ICollection<Consulta> Consultas { get; set; } = [];

        // Navigation property for Utentes assigned to this Medico
        public virtual ICollection<Utente> UtentesAssociados { get; set; } = [];

    }
}
