using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hospital_Web.Models;

namespace Hospital_Web.Data
{
    public class Hospital_WebContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegistoClinico>()
                .HasOne(rc => rc.UtenteNavigation)
                .WithMany(u => u.RegistosClinicos)
                .HasForeignKey(rc => rc.Utente)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RegistoClinico>()
                .HasOne(rc => rc.MedicoNavigation)
                .WithMany(m => m.RegistosClinicos)
                .HasForeignKey(rc => rc.Medico)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RegistoClinico>()
                .HasOne(rc => rc.ConsultaNavigation)
                .WithMany(c => c.RegistosClinicos)
                .HasForeignKey(rc => rc.Episodio)
                .OnDelete(DeleteBehavior.NoAction);
        }
        public Hospital_WebContext (DbContextOptions<Hospital_WebContext> options)
            : base(options)
        {
        }

        public DbSet<Hospital_Web.Models.Utente> Utente { get; set; } = default!;
        public DbSet<Hospital_Web.Models.Consulta> Consulta { get; set; } = default!;
        public DbSet<Hospital_Web.Models.Funcionario> Funcionario { get; set; } = default!;
        public DbSet<Hospital_Web.Models.Gabinete> Gabinete { get; set; } = default!;
        public DbSet<Hospital_Web.Models.LimpezaSala> LimpezaSala { get; set; } = default!;
        public DbSet<Hospital_Web.Models.Medico> Medico { get; set; } = default!;
        public DbSet<Hospital_Web.Models.UtenteQuarto> UtenteQuarto { get; set; } = default!;
        public DbSet<Hospital_Web.Models.FuncionarioLimpeza> FuncionarioLimpeza { get; set; } = default!;
        public DbSet<Hospital_Web.Models.Sala> Sala { get; set; } = default!;
        public DbSet<Hospital_Web.Models.RegistoClinico> RegistoClinico { get; set; } = default!;
        public DbSet<Hospital_Web.Models.QuartoInternagem> QuartoInternagem { get; set; } = default!;
    }
}
