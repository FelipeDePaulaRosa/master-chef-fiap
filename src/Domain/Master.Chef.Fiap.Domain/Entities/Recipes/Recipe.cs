using Master.Chef.Fiap.Domain.Contracts;

namespace Master.Chef.Fiap.Domain.Entities.Recipes;

public class Recipe : AggregateRoot
{
    public Guid OwnerId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Summary { get; private set; }
    public DifficultyLevel DifficultLevelEnum { get; private set; }
    public ICollection<Ingredient> Ingredients { get; private set; }

    public Recipe()
    {
    }

    public Recipe(
        Guid ownerId,
        string title,
        string description,
        string summary,
        DifficultyLevelEnum difficultLevelEnum,
        IEnumerable<Ingredient> ingredients)
    {
        OwnerId = ownerId;
        Title = title;
        Description = description;
        Summary = summary;
        DifficultLevelEnum = new DifficultyLevel(difficultLevelEnum);
        Ingredients = ingredients.ToList();
    }
    
    public string GetDifficultLevelDescription()
    {
        return DifficultLevelEnum.GetDescription();
    }

    public short GetDifficultLevelId()
    {
        return (short) DifficultLevelEnum.Level;
    }
}