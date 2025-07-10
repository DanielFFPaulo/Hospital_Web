using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class LimpezaSala
    {
        /// <summary>
        /// Identificador único da limpeza de sala.
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// Nome do primeiro produto utilizado na limpeza.
        /// </summary>
        [StringLength(100)]
        [Required(ErrorMessage = "É necessário definir o {0}")]
        [Display(Name = "Produto 1")]
        [RegularExpression(@"^[a-zA-Zà-üÀ-Ü0-9\s.,;:!?()\-]+$", ErrorMessage = "O {0} contém caracteres inválidos.")]
        [DisplayFormat(NullDisplayText = "Sem produto")]
        public string Produto1 { get; set; } = string.Empty;


        /// <summary>
        /// Nome do segundo produto utilizado na limpeza.
        /// </summary>
        [StringLength(100)]
        [Display(Name = "Produto 2")]
        [RegularExpression(@"^[a-zA-Zà-üÀ-Ü0-9\s.,;:!?()\-]+$", ErrorMessage = "O {0} contém caracteres inválidos.")]
        [DisplayFormat(NullDisplayText = "Sem produto")]
        public string? Produto2 { get; set; }



        /// <summary>
        /// Nome do terceiro produto utilizado na limpeza.
        /// </summary>
        [StringLength(100)]
        [Display(Name = "Produto 3")]
        [RegularExpression(@"^[a-zA-Zà-üÀ-Ü0-9\s.,;:!?()\-]+$", ErrorMessage = "O {0} contém caracteres inválidos.")]
        [DisplayFormat(NullDisplayText = "Sem produto")]
        public string? Produto3 { get; set; }

        /// <summary>
        /// Data da limpeza.
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Data da limpeza")]
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
        public DateTime Data { get; set; }


        /// <summary>
        /// Hora exata do inicio da limpeza.
        /// </summary>
        [DataType(DataType.Time)]
        [Display(Name = "Hora da limpeza")]
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
        public TimeSpan Hora { get; set; }

        // Foreign keys
        public int Funcionario_Id { get; set; }

        [ForeignKey("Funcionario_Id")]
        public virtual FuncionarioLimpeza? Funcionario { get; set; }

        public int Sala_Id { get; set; }

        [ForeignKey("Sala_Id")]
        public virtual Sala? Sala { get; set; }
    }

}
