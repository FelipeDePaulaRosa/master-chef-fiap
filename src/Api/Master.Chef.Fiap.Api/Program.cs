using Master.Chef.Fiap.Application.Services;
using Master.Chef.Fiap.Application.AppServices;
using Master.Chef.Fiap.Application.AppServices.Identities;
using Master.Chef.Fiap.CrossCutting.Extensions;
using Master.Chef.Fiap.Infrastructure.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings.Development.json")
    .Build();

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

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IRecipeAppService, RecipeAppService>();
builder.Services.AddScoped<IIdentityAppService, IdentityAppService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();