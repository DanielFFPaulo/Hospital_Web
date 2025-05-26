using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hospital_Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Hospital_Web.Data
{
    public class Hospital_WebContext : IdentityDbContext<ApplicationUser>
    {
        public Hospital_WebContext (DbContextOptions<Hospital_WebContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Utente)
                .WithMany(u => u.Consultas)
                .HasForeignKey(c => c.Utente_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Medico)
                .WithMany()
                .HasForeignKey(c => c.Medico_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Gabinete)
                .WithMany()
                .HasForeignKey(c => c.Gabinete_Id)
                .OnDelete(DeleteBehavior.NoAction);
        }

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
        public DbSet<Administrador> Administrador { get; set; }
    }
}
