using Master.Chef.Fiap.Domain.Contracts;

namespace Master.Chef.Fiap.Domain.Entities.Recipes;

public class Recipe : Entity, IAggregateRoot
{
    public Guid OwnerId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DifficultyLevel DifficultLevelEnum { get; set; }
    public IEnumerable<Ingredient> Ingredients { get; set; }

    private Recipe()
    {
    }

    public Recipe(
        Guid ownerId,
        string title,
        string description,
        DifficultyLevelEnum difficultLevelEnum,
        IEnumerable<Ingredient> ingredients)
    {
        OwnerId = ownerId;
        Title = title;
        Description = description;
        DifficultLevelEnum = new DifficultyLevel(difficultLevelEnum);
        Ingredients = ingredients;
    }
}