using System.ComponentModel.DataAnnotations.Schema;

namespace Master.Chef.Fiap.Domain.Entities.Recipes;

public class Ingredient : Entity
{
    public Guid RecipeId { get; set; }
    public string Name { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
    
    [NotMapped]
    public Recipe Recipe { get; set; }
}