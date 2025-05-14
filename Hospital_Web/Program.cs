using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Hospital_Web.Data;
using Hospital_Web.Models;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Hospital_Web.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Hospital_WebContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Hospital_WebContext") ?? throw new InvalidOperationException("Connection string 'Hospital_WebContext' not found.")));


//adicionado

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<Hospital_WebContext>()
    .AddDefaultTokenProviders();

//adicionado
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies"; // login normal
    options.DefaultChallengeScheme = "Bearer"; // APIs
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

builder.Services.AddScoped<TokenService>();



// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

//adicionado
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();