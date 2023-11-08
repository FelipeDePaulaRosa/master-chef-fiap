using System.ComponentModel.DataAnnotations.Schema;

namespace Master.Chef.Fiap.Domain.Entities.Recipes;

public class Ingredient : Entity
{
    public Guid RecipeId { get; private set; }
    public string Name { get; private set; }
    public double Quantity { get; private set; }
    public string Unit { get; private set; }
    
    [NotMapped]
    public Recipe Recipe { get; private set; }
    
    private Ingredient()
    {
    }

    public Ingredient(string name, double quantity, string unit)
    {
        Name = name;
        Quantity = quantity;
        Unit = unit;
    }
    
    public void Update(string name, double quantity, string unit)
    {
        Name = name;
        Quantity = quantity;
        Unit = unit;
    }
}