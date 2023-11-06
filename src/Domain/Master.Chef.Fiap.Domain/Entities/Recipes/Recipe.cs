using Master.Chef.Fiap.Domain.Contracts;

namespace Master.Chef.Fiap.Domain.Entities.Recipes;

public class Recipe : Entity, IAggregateRoot
{
    public Guid IdOwner { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DifficultyLevel DificultLevel { get; set; }
    public IEnumerable<string> Ingredients { get; set; }
    
}