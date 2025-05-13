using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class QuartosInternagem : Sala
    {
        public enum TipoQuarto
        {
            Normal,
            Privado,
            SemiPrivado
        }

        /// <summary>
        /// Descricao do estado do Quarto.
        /// </summary>
        [Required]
        [StringLength(200)]
        [Display(Name = "Descrição do Quarto")]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; } = string.Empty;




        /// <summary>
        /// Tipo de Quarto.
        /// </summary>
        [Display(Name = "Tipo de Quarto")]
        [Required(ErrorMessage ="O {0} deve ser definido")]
        public TipoQuarto Tipo { get; set; }




        /// <summary>
        /// Numero de camas do Quarto.
        /// </summary>
        [Required]
        [Display(Name = "Capacidade")]
        public int Capacidade { get; set; } = 4;







        // Navigation property
        public virtual ICollection<Internamento> Internamentos { get; set; } = [];
    }
}
