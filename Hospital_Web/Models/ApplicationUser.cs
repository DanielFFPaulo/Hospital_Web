using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Hospital_Web.Models
{
    /// <summary>
    /// Classe que representa um utilizador autenticado no sistema.
    /// Herda de IdentityUser para incluir funcionalidades de autenticação e autorização (UserName, PasswordHash, etc.).
    /// Contém também referências a entidades como Utente, Médico e Funcionário de Limpeza.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Indica se o utilizador deve alterar a senha no próximo login.
        /// Usado geralmente quando uma conta é criada com senha temporária.
        /// </summary>
        public bool DeveAlterarSenha { get; set; } = false;

        /// <summary>
        /// Chave estrangeira (opcional) que associa este utilizador a um Utente.
        /// </summary>
        public int? UtenteId { get; set; }

        /// <summary>
        /// Navegação para o utente associado. Esta propriedade permite o acesso aos dados do utente.
        /// </summary>
        [ForeignKey("UtenteId")]
        public Utente? Utente { get; set; }

        /// <summary>
        /// Chave estrangeira (opcional) que associa este utilizador a um Médico.
        /// </summary>
        public int? MedicoId { get; set; }

        /// <summary>
        /// Navegação para o médico associado. `virtual` permite carregamento tardio (lazy loading).
        /// </summary>
        public virtual Medico? Medico { get; set; }

        /// <summary>
        /// Chave estrangeira (opcional) que associa este utilizador a um Funcionário de Limpeza.
        /// </summary>
        public int? FuncionarioLimpezaId { get; set; }

        /// <summary>
        /// Navegação para o funcionário de limpeza associado.
        /// </summary>
        public FuncionarioLimpeza? FuncionarioLimpeza { get; set; }
    }
}

