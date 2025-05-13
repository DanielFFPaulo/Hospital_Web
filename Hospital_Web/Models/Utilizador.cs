using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class Utilizador
    {
        /// <summary>
        /// Identificador único do utilizador.
        /// </summary>
        [Key]
        public int ID_Utilizador { get; set; }

        /// <summary>
        /// Nome de utilizador para autenticação.
        /// </summary>
        [Display(Name = "Nome de utilizador")]
        [StringLength(50)]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public string Nome_Utilizador { get; set; } = string.Empty;
        /// <summary>
        /// Senha para autenticação.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// Data de criação da conta do utilizador.
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
