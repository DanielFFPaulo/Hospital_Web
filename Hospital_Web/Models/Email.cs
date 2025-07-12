using System.ComponentModel.DataAnnotations;

namespace SendEmail.Models
{
    /// <summary>
    /// Classe para recolher os dados de um Email
    /// </summary>
    public class Email
    {


        /// <summary>
        /// Endereco de email do destinatario do email
        /// </summary>
        [Required(ErrorMessage = "O {0} e de preenchimento obrigatorio.")]
        [EmailAddress(ErrorMessage = "Deve escrever um endereco de email valido...")]
        [Display(Name = "Destinatario")]
        public string Destinatario { get; set; } = string.Empty;

        /// <summary>
        /// Assunto do email
        /// </summary>
        [Required(ErrorMessage = "O {0} e de preenchimento obrigatorio.")]
        [Display(Name = "Assunto do email")]
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Corpo do email
        /// </summary>
        [Required(ErrorMessage = "O {0} e de preenchimento obrigatorio.")]
        [Display(Name = "Corpo do email")]
        public string Body { get; set; } = string.Empty;

    }
}
