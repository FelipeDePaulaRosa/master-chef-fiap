using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Master.Chef.Fiap.Infrastructure.Contexts;

public class MasterChefIdentityDbContext : IdentityDbContext
{
    public MasterChefIdentityDbContext(DbContextOptions<MasterChefIdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("identity");
        base.OnModelCreating(builder);
    }
}