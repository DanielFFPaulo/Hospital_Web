using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class Utilizador
    {
        [Key]
        public int ID_Utilizador { get; set; }

        public string Nome_Utilizador { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        public DateTime Data_Criacao_Conta { get; set; }

        // Foreign key for Pessoa
        public int Pessoa_Id { get; set; }

        [ForeignKey("Pessoa_Id")]
        public virtual Pessoa? Pessoa { get; set; }
    }
}
