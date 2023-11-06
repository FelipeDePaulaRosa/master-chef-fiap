using Master.Chef.Fiap.Domain.Contracts;

namespace Master.Chef.Fiap.Domain.Entities.Recipes;

public class Recipe : AggregateRoot
{
    public Guid OwnerId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DifficultyLevel DifficultLevelEnum { get; private set; }
    public ICollection<Ingredient> Ingredients { get; private set; }

    public Recipe()
    {
    }

    public Recipe(
        Guid ownerId,
        string title,
        string description,
        DifficultyLevelEnum difficultLevelEnum,
        ICollection<Ingredient> ingredients)
    {
        OwnerId = ownerId;
        Title = title;
        Description = description;
        DifficultLevelEnum = new DifficultyLevel(difficultLevelEnum);
        Ingredients = ingredients;
    }
}