using System.ComponentModel.DataAnnotations;


namespace Hospital_Web.Models
{
    /// <summary>
    /// Classe que representa um funcionario de limpeza, herda da classe Pessoa.
    /// Contem informações adicionais como turno, tamanho do uniforme, data de contratação, certificações, etc.
    /// </summary>
    public class FuncionarioLimpeza : Pessoa
    {
        /// <summary>
        /// Enumeração que define os possiveis turnos de trabalho (Manhã, Tarde, Noite),
        /// com nomes legiveis definidos por [Display(Name = "...")].
        /// </summary>
        public enum Turnos
        {
            [Display(Name = "Manha")]
            Manha,

            [Display(Name = "Tarde")]
            Tarde,

            [Display(Name = "Noite")]
            Noite
        }

        /// <summary>
        /// Enumeração que define os tamanhos de uniforme disponiveis.
        /// Tambem usa [Display] para apresentar de forma legivel nas views.
        /// </summary>
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

        /// <summary>
        /// Propriedade obrigatoria que indica o turno do funcionario.
        /// </summary>
        [Required(ErrorMessage = "O {0} e obrigatorio")]
        [Display(Name = "Turno")]
        public Turnos Turno { get; set; }

        /// <summary>
        /// Propriedade obrigatoria que indica o tamanho do uniforme do funcionario.
        /// </summary>
        [Required(ErrorMessage = " O {0} e obrigatorio")]
        [Display(Name = "Tamanho do uniforme")]
        public Uniformes Tamanho_Uniforme { get; set; }

        /// <summary>
        /// Data de contratação do funcionario, exibida como data nas views.
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Data de contratacao")]
        public DateTime Data_de_contratacao { get; set; }

        /// <summary>
        /// Texto opcional com ate 500 caracteres que lista certificações do funcionario.
        /// </summary>
        [StringLength(500)]
        [Display(Name = "Certificacoes")]
        public string Certificacoes { get; set; } = string.Empty;

        /// <summary>
        /// Propriedade de navegação que representa a lista de limpezas de sala realizadas pelo funcionario.
        /// </summary>
        public virtual ICollection<LimpezaSala> LimpezasDeSala { get; set; } = [];
    }
}
