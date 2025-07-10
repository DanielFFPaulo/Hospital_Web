using Microsoft.EntityFrameworkCore;
using Hospital_Web.Data;
using Microsoft.AspNetCore.Identity;

namespace Hospital_Web.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new Hospital_WebContext(
            serviceProvider.GetRequiredService<DbContextOptions<Hospital_WebContext>>()))
        {
            // Dados de entidades (Utente, Medico, etc.)
            if (!context.Utente.Any()) { /* context.Utente.AddRange(...); */ }
            if (!context.Medico.Any()) { /* context.Medico.AddRange(...); */ }
            if (!context.FuncionarioLimpeza.Any()) { /* context.FuncionarioLimpeza.AddRange(...); */ }
            if (!context.Sala.Any()) { /* context.Sala.AddRange(...); */ }
            if (!context.Gabinete.Any()) { /* context.Gabinete.AddRange(...); */ }
            if (!context.QuartosInternagem.Any()) { /* context.QuartosInternagem.AddRange(...); */ }

            context.SaveChanges();

            if (!context.LimpezaSala.Any())
            {
                var sala = context.Sala.OrderByDescending(s => s.ID).FirstOrDefault();
                var func = context.FuncionarioLimpeza.OrderByDescending(f => f.N_Processo).FirstOrDefault();
                if (sala != null && func != null) { /* context.LimpezaSala.Add(...); */ }
            }

            if (!context.Consulta.Any())
            {
                var utente = context.Utente.OrderByDescending(u => u.N_Processo).FirstOrDefault();
                var medico = context.Medico.OrderByDescending(m => m.N_Processo).FirstOrDefault();
                var gabinete = context.Gabinete.OrderByDescending(g => g.ID).FirstOrDefault();
                if (utente != null && medico != null && gabinete != null) { /* context.Consulta.Add(...); */ }
            }

            context.SaveChanges();

            if (!context.Internamento.Any())
            {
                var quarto = context.QuartosInternagem.OrderByDescending(q => q.ID).FirstOrDefault();
                var consulta = context.Consulta.OrderByDescending(c => c.Episodio).FirstOrDefault();
                var utente = consulta?.Utente ?? context.Utente.OrderByDescending(c => c.N_Processo).FirstOrDefault();
                if (quarto != null && utente != null) { /* context.Internamento.Add(...); */ }
            }

            context.SaveChanges();

            // CRIAÇÃO/RECRIAÇÃO DO ADMIN
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {


                var roles = new[] { "Admin", "Utente", "FuncionarioLimpeza", "Medico" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }

                var adminEmail = "admin@hospital.com";
                var existingAdmin = await userManager.FindByEmailAsync(adminEmail);

                // Apaga utilizador antigo (se existir)
                if (existingAdmin != null)
                    await userManager.DeleteAsync(existingAdmin);

                // Cria novo admin
                var user = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    DeveAlterarSenha = true
                };

                var result = await userManager.CreateAsync(user, "Hosp@123456");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }).GetAwaiter().GetResult();
        }
    }
}
