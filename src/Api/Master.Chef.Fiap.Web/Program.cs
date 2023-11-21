using System.Text;
using Master.Chef.Fiap.Application.AppServices;
using Master.Chef.Fiap.Application.AppServices.Auths;
using Master.Chef.Fiap.Application.AppServices.Identities;
using Master.Chef.Fiap.Application.Services;
using Master.Chef.Fiap.Application.Services.Auths;
using Master.Chef.Fiap.CrossCutting.Configurations;
using Master.Chef.Fiap.CrossCutting.Extensions;
using Master.Chef.Fiap.Infrastructure.Contexts;
using Master.Chef.Fiap.Infrastructure.Repositories;
using Master.Chef.Fiap.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings.Development.json")
    .Build();

builder.Services.AddDbContext<MasterChefApiDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});

#region Identity Configuration

builder.Services.AddDbContext<MasterChefIdentityDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    {
        options.Password = new PasswordOptions()
        {
            RequireDigit = true,
            RequiredLength = 8,
            RequireLowercase = true,
            RequireUppercase = true,
            RequireNonAlphanumeric = true
        };

        options.SignIn.RequireConfirmedEmail = true;

    })
    .AddDefaultTokenProviders()
    .AddErrorDescriber<IdentityErrorExtension>()
    .AddEntityFrameworkStores<MasterChefIdentityDbContext>();
#endregion

#region JWT - Token Configuration
var appSettingsSection = configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);

builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opts =>
{
    opts.RequireHttpsMetadata = true;
    opts.SaveToken = true;
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = appSettings.Issuer,
        ValidAudience = appSettings.ValidIn,
        IssuerSigningKey = new SymmetricSecurityKey(key),
    };
});
#endregion

// Add services to the container.
builder.Services.AddControllers();

#region Dependency Injection
builder.Services.AddTransient<MasterChefApiDbContext>();
builder.Services.AddSingleton<IUserSession, UserSession>();

builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IRecipeAppService, RecipeAppService>();
builder.Services.AddScoped<IIdentityAppService, IdentityAppService>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthAppService, AuthAppService>();
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();