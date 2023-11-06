using Microsoft.EntityFrameworkCore;
using Master.Chef.Fiap.Domain.Entities.Recipes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Master.Chef.Fiap.Infrastructure.Map;

public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder
            .HasKey(x => x.Id);
        
        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(250)
            .HasColumnName("name");

        builder
            .Property(x => x.Quantity)
            .IsRequired()
            .HasColumnName("quantity");
        
        builder
            .Property(x => x.Unit)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("unit");
    }
}