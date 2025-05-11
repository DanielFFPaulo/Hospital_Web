using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class Pessoa
    {
        [Key]
        public int N_Processo { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        public int Idade { get; set; }

        [DataType(DataType.Date)]
        public DateTime Data_de_Nascimento { get; set; }

        [StringLength(200)]
        public string Morada { get; set; }

        [StringLength(20)]
        public string Telefone1 { get; set; }

        [StringLength(20)]
        public string Telefone2 { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(20)]
        public string NIF { get; set; }

        [StringLength(20)]
        public string Cod_postal { get; set; }

        [StringLength(100)]
        public string Localidade { get; set; }

        // Navigation property
        public virtual Utilizador? User { get; set; } = null!;

        [NotMapped] // This prevents EF from mapping this to the database
        public virtual string Discriminator => this.GetType().Name;

    }
}
