using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class Utilizador
    {
        /// <summary>
        /// Identificador unico do utilizador.
        /// </summary>
        [Key]
        public int ID_Utilizador { get; set; }



        /// <summary>
        /// Nome de utilizador para autenticacao.
        /// </summary>
        [Display(Name = "Nome de utilizador")]
        [StringLength(50)]
        [Required(ErrorMessage = "O {0} e de preenchimento obrigatorio")]
        public string Nome_Utilizador { get; set; } = string.Empty;
        /// <summary>
        /// Senha para autenticacao.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// Data de criacao da conta do utilizador.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime Data_Criacao_Conta { get; set; } = DateTime.Now;

        // Foreign key for Pessoa
        /// <summary>
        /// Identificador da pessoa associada ao utilizador.
        /// </summary>
        public int? Pessoa_Id { get; set; }

        [ForeignKey("Pessoa_Id")]
        public virtual Pessoa? Pessoa { get; set; }

    }
}
