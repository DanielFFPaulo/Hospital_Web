using Microsoft.EntityFrameworkCore;
using Hospital_Web.Data;
using Hospital_Web.Models;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Hospital_Web.Services;
using Hospital_Web.Hubs;
using Microsoft.AspNetCore.SignalR;


var builder = WebApplication.CreateBuilder(args);

// CONFIGURAcaO DE SERVIcOS 

// DbContext (SQL Server)
builder.Services.AddDbContext<Hospital_WebContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Hospital_WebContext")
        ?? throw new InvalidOperationException("\r\nString de conexao 'Hospital_WebContext' nao encontrada.")));

// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<Hospital_WebContext>()
    .AddDefaultTokenProviders();

// JWT + Cookies
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
})
.AddCookie("Cookies", options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
})
.AddJwtBearer("Bearer", options =>
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
        ClockSkew = TimeSpan.Zero
    };
});

// Servicos de aplicacao
builder.Services.AddScoped<TokenService>();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailSender, SmtpEmailSender>();

// MVC / Razor
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// SignalR
builder.Services.AddSignalR();

builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();


var app = builder.Build();

// ---------------- MIGRAcoES + SEED AUTOMaTICOS (inclui AZURE) ----------------
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<Hospital_WebContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        // 1) Aplica quaisquer migracoes pendentes
        context.Database.Migrate();

        // 2) Inicializa dados essenciais (roles, utilizador admin, etc.)
        string[] roles = { "Admin", "Medico", "FuncionarioLimpeza", "Utente" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Criar utilizador admin se nao existir
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
            {
                await userManager.AddToRoleAsync(newAdmin, "Admin");
            }
        }

    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao migrar ou iniciar o banco de dados.");
    }
}
// ---------------------------------------------------------------------------

// ---------------- PIPELINE HTTP ----------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // Em desenvolvimento mostra erros detalhados, inclusive de migracoes
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Rotas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<NotificationHub>("/notificationHub");

app.MapRazorPages();
app.MapControllers();


app.MapGet("/", context =>
{
    if (!context.User.Identity?.IsAuthenticated ?? true)
        context.Response.Redirect("/Identity/Account/Login");
    else
        context.Response.Redirect("/Home/Index");
    return Task.CompletedTask;
});

app.Run();