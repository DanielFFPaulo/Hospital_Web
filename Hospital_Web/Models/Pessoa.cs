using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class Pessoa
    {
        [Key]
        public int N_Processo { get; set; }

        /// <summary>
        /// Nome da pessoa
        /// </summary>
        [Display(Name = "Nome")]
        [StringLength(50)]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public string Nome { get; set; } = string.Empty;






        /// <summary>
        /// Idade da pessoa
        /// </summary>
        [Display(Name = "Idade")]
        [Range(0, 120, ErrorMessage = "A {0} deve estar entre {1} e {2}")]
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
        [RegularExpression(@"^[0-9]{1,3}$", ErrorMessage = "A {0} deve conter apenas números.")]
        public int Idade { get; set; }







        /// <summary>
        /// Data de nascimento da pessoa
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
        public DateTime Data_de_Nascimento { get; set; }






        /// <summary>
        /// Morada do utilizador
        /// </summary>
        [Display(Name = "Morada")]
        [StringLength(50)]
        public string? Morada { get; set; }






        /// <summary>
        /// número de telemóvel do utilizador
        /// </summary>
        [Display(Name = "Telemóvel")]
        [StringLength(18)]
        [RegularExpression("(([+]|00)[0-9]{1,5})?[1-9][0-9]{5,10}", ErrorMessage = "Escreva um nº de telefone. Pode adicionar indicativo do país.")]
        public string? Telemovel { get; set; }






        /// <summary>
        /// número de telemóvel do utilizador
        /// </summary>
        [Display(Name = "Telemóvel Alternativo")]
        [StringLength(18)]
        [RegularExpression("(([+]|00)[0-9]{1,5})?[1-9][0-9]{5,10}", ErrorMessage = "Escreva um nº de telefone. Pode adicionar indicativo do país.")]
        public string? TelemovelAlt { get; set; }








        /// <summary>
        /// Email da pessoa
        /// </summary>
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;







        /// <summary>
        /// Número de identificação fiscal do Utilizador
        /// </summary>
        [Display(Name = "NIF")]
        [StringLength(9)]
        [RegularExpression("[1-9][0-9]{8}", ErrorMessage = "Deve escrever apenas 9 digitos no {0}")]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        public string NIF { get; set; } = string.Empty;








        /// <summary>
        /// Código Postal da  morada do utilizador
        /// </summary>
        [Display(Name = "Código Postal")]
        [StringLength(50)]
        [RegularExpression("[1-9][0-9]{3}-[0-9]{3} [A-Za-z ]+",
                           ErrorMessage = "No {0} só são aceites algarismos e letras inglesas.")]
        public string? Cod_Postal { get; set; }





        /// <summary>
        /// Localidade da morada do utilizador
        /// </summary>
        [StringLength(100)]
        [RegularExpression("[A-Za-z ]+", ErrorMessage = "No {0} só são aceites algarismos e letras inglesas.")]
        [Display(Name = "Localidade")]
        public string? Localidade { get; set; }

        // Navigation property
        public virtual Utilizador? User { get; set; } = null!;

        [NotMapped] // This prevents EF from mapping this to the database
        public virtual string Discriminator => this.GetType().Name;

    }
}
