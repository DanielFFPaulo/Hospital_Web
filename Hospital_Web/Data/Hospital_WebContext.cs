using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hospital_Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Hospital_Web.Data
{
    /// <summary>
    /// Classe que representa o contexto da base de dados da aplicação.
    /// Herda de IdentityDbContext para incluir suporte ao Identity com a entidade ApplicationUser.
    /// </summary>
    public class Hospital_WebContext : IdentityDbContext<ApplicationUser>
    {
        // Construtor que recebe opções de configuração (string de conexão, etc.)
        public Hospital_WebContext(DbContextOptions<Hospital_WebContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Método chamado ao construir o modelo da base de dados.
        /// Utilizado para configurar relacionamentos entre entidades.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Chama a configuração base do IdentityDbContext
            base.OnModelCreating(modelBuilder);

            // Define relação Consulta → Utente (muitos para um)
            // Cada Consulta tem um Utente, e um Utente pode ter várias Consultas
            // O DeleteBehavior.NoAction evita que apagar um utente apague automaticamente as consultas
            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Utente)
                .WithMany(u => u.Consultas)
                .HasForeignKey(c => c.Utente_Id)
                .OnDelete(DeleteBehavior.NoAction);

            // Define relação Consulta → Medico (muitos para um)
            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Medico)
                .WithMany()
                .HasForeignKey(c => c.Medico_Id)
                .OnDelete(DeleteBehavior.NoAction);

            // Define relação Consulta → Gabinete (muitos para um)
            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Gabinete)
                .WithMany()
                .HasForeignKey(c => c.Gabinete_Id)
                .OnDelete(DeleteBehavior.NoAction);
        }

        // Definições das tabelas que serão mapeadas no banco de dados.
        // Cada DbSet representa uma tabela correspondente no SQL Server.

        public DbSet<Hospital_Web.Models.Consulta> Consulta { get; set; } = default!;
        public DbSet<Hospital_Web.Models.FuncionarioLimpeza> FuncionarioLimpeza { get; set; } = default!;
        public DbSet<Hospital_Web.Models.Gabinete> Gabinete { get; set; } = default!;
        public DbSet<Hospital_Web.Models.Internamento> Internamento { get; set; } = default!;
        public DbSet<Hospital_Web.Models.LimpezaSala> LimpezaSala { get; set; } = default!;
        public DbSet<Hospital_Web.Models.Medico> Medico { get; set; } = default!;
        public DbSet<Hospital_Web.Models.Pessoa> Pessoa { get; set; } = default!;
        public DbSet<Hospital_Web.Models.QuartosInternagem> QuartosInternagem { get; set; } = default!;
        public DbSet<Hospital_Web.Models.Sala> Sala { get; set; } = default!;
        public DbSet<Hospital_Web.Models.Utente> Utente { get; set; } = default!;
        public DbSet<Hospital_Web.Models.Utilizador> Utilizador { get; set; } = default!;
    }
}
