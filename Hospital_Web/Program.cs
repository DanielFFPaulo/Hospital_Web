using Microsoft.EntityFrameworkCore;
using Hospital_Web.Data;
using Hospital_Web.Models;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Hospital_Web.Services;
using Hospital_Web.Hubs;
using Microsoft.AspNetCore.SignalR;

// ------------------------------------------------------------
// Program.cs / Startup: configuração principal da aplicação
// ASP.NET Core + Identity + JWT + SignalR + EF Core
// ------------------------------------------------------------

var builder = WebApplication.CreateBuilder(args);

#region CONFIGURAÇÃO DE SERVIÇOS (DI)

// ---------------------------
// 1. DbContext  (SQL Server)
// ---------------------------
builder.Services.AddDbContext<Hospital_WebContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Hospital_WebContext")
        ?? throw new InvalidOperationException(
            "String de ligação 'Hospital_WebContext' não encontrada.")));

// ---------------------------
// 2. Identity (utilizadores + roles)
// ---------------------------
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        options.SignIn.RequireConfirmedAccount = false)   // login sem e-mail confirmado
    .AddEntityFrameworkStores<Hospital_WebContext>()
    .AddDefaultTokenProviders();

// ---------------------------
// 3. Autenticação – Cookies (UI) + JWT (API)
// ---------------------------
var jwtSettings = builder.Configuration.GetSection("Jwt");   // lê appsettings.json
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";          // esquema por defeito (navegador)
})
.AddCookie("Cookies", options =>               // login via página /Account/Login
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
})
.AddJwtBearer("Bearer", options =>             // suporte a “Authorization: Bearer <token>”
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero              // sem tolerância extra na expiração
    };
});

// ---------------------------
// 4. Serviços de aplicação
// ---------------------------
builder.Services.AddScoped<TokenService>();                              // gera JWT
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailSender, SmtpEmailSender>();          // envia emails

// ---------------------------
// 5. MVC / Razor Pages
// ---------------------------
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();              // detalhes EF em Dev

// ---------------------------
// 6. SignalR  (tempo-real)
// ---------------------------
builder.Services.AddSignalR();
builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();  // identifica users

#endregion

var app = builder.Build();

#region MIGRAÇÕES & SEED AUTOMÁTICOS
// Aplica migrações pendentes e cria dados iniciais (roles, admin)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<Hospital_WebContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        // 1) aplica todas as migrações pendentes
        context.Database.Migrate();

        // 2) cria roles essenciais
        string[] roles = { "Admin", "Medico", "FuncionarioLimpeza", "Utente" };
        foreach (var role in roles)
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

        // 3) garante a existência do utilizador admin
        string adminEmail = "admin@hospital.pt";
        string adminPassword = "Admin123!";
        var admin = await userManager.FindByEmailAsync(adminEmail);
        if (admin == null)
        {
            var newAdmin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(newAdmin, adminPassword);
            if (result.Succeeded)
                await userManager.AddToRoleAsync(newAdmin, "Admin");
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Erro ao migrar ou inicializar a base de dados.");
    }
}
#endregion

#region PIPELINE HTTP (middlewares)

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // página de erro genérica em produção
    app.UseHsts();
}
else
{
    app.UseMigrationsEndPoint();            // mostra erros detalhados + pendências EF
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();   // valida cookies/JWT
app.UseAuthorization();    // verifica [Authorize] nas controllers

// ---------- Rotas MVC ----------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ---------- End-point SignalR ----------
app.MapHub<NotificationHub>("/notificationHub");

// ---------- Razor Pages & API ----------
app.MapRazorPages();
app.MapControllers();

// ---------- Redireciona raiz conforme autenticado ----------
app.MapGet("/", context =>
{
    if (!context.User.Identity?.IsAuthenticated ?? true)
        context.Response.Redirect("/Identity/Account/Login");
    else
        context.Response.Redirect("/Home/Index");
    return Task.CompletedTask;
});


app.Run();
