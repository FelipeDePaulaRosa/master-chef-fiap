using Master.Chef.Fiap.Domain.Entities.Recipes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Master.Chef.Fiap.Infrastructure.Map;

public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder
            .HasKey(x => x.Id)
            .HasName("id");

        builder
            .Property(x => x.OwnerId)
            .IsRequired()
            .HasColumnName("owner_id");
        
        builder
            .Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("title");
        
        builder
            .Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(1000)
            .HasColumnName("description");
        
        builder
            .Property(x => x.Summary)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("summary");
        
        builder
            .OwnsOne(x => x.DifficultLevelEnum, difficulty =>
            {
                difficulty.Property(x => x.Level)
                    .HasColumnName("difficult_level")
                    .IsRequired();
            });

        builder
            .HasMany(x => x.Ingredients)
            .WithOne(x => x.Recipe)
            .HasForeignKey(x => x.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}