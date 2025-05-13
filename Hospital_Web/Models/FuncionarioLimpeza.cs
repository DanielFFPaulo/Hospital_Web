using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class FuncionarioLimpeza : Pessoa
    {
        public enum Turnos
        {
            [Display(Name = "Manhã")]
            Manha,
            [Display(Name = "Tarde")]
            Tarde,
            [Display(Name = "Noite")]
            Noite
        }

        public enum Uniformes
        {
            [Display(Name = "XS")]
            XS,
            [Display(Name = "S")]
            S,
            [Display(Name = "M")]
            M,
            [Display(Name = "L")]
            L,
            [Display(Name = "XL")]
            XL,
            [Display(Name = "XXL")]
            XXL,
            [Display(Name = "XXXL")]
            XXXL
        }


        [Required(ErrorMessage ="O {0} é obrigatorio")]
        [Display(Name = "Turno")]
        public Turnos Turno { get; set; }

        [Required(ErrorMessage = " O {0} é obrigatorio")]
        [Display(Name = "Tamanho do uniforme")]
        public Uniformes Tamanho_Uniforme { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de contratação")]
        public DateTime Data_de_contratacao { get; set; }

        [StringLength(500)]
        [Display(Name = "Certificações")]
        public string Certificacoes { get; set; } = string.Empty;

        // Navigation property
        public virtual ICollection<LimpezaSala> LimpezasDeSala { get; set; } = [];
    }

}
