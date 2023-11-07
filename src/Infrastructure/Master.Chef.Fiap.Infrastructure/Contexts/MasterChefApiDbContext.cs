using Microsoft.EntityFrameworkCore;

namespace Master.Chef.Fiap.Infrastructure.Contexts;

public class MasterChefApiDbContext : DbContext
{
    public MasterChefApiDbContext(DbContextOptions<MasterChefApiDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("recipe");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MasterChefApiDbContext).Assembly);
    }
}