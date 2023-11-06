using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Master.Chef.Fiap.Application.Services;
using Master.Chef.Fiap.CrossCutting.Extensions;
using Master.Chef.Fiap.Infrastructure.Contexts;
using Master.Chef.Fiap.Application.AppServices;
using Master.Chef.Fiap.Application.Services.Auths;
using Master.Chef.Fiap.Application.AppServices.Auths;
using Master.Chef.Fiap.Application.AppServices.Identities;
using Master.Chef.Fiap.CrossCutting.Configurations;
using Master.Chef.Fiap.Domain.Entities.Recipes;
using Master.Chef.Fiap.Infrastructure.Repositories;
using Master.Chef.Fiap.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IRecipeAppService, RecipeAppService>();
builder.Services.AddScoped<IIdentityAppService, IdentityAppService>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthAppService, AuthAppService>();
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    //TODO: Fix Swagger JWT Authentication
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Insira o token JWT: Bearer {seu token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        // Scheme = "Bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                // Scheme = "oauth2",
                // Name = "Bearer",
                // In = ParameterLocation.Header,

            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();