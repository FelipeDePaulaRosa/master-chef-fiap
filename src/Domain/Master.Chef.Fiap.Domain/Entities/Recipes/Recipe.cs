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

    public void Update(string title, string description, string summary, DifficultyLevelEnum difficultLevel)
    {
        Title = title;
        Description = description;
        Summary = summary;
        DifficultLevelEnum = new DifficultyLevel(difficultLevel);   
    }

    public void UpdateIngredient(Guid ingredientDtoId, string ingredientDtoName, double ingredientDtoQuantity, string ingredientDtoUnit)
    {
        if (ingredientDtoId == Guid.Empty)
        {
            AddIngredient(ingredientDtoName, ingredientDtoQuantity, ingredientDtoUnit);
            return;
        }
        
        var ingredient = Ingredients.FirstOrDefault(x => x.Id == ingredientDtoId);
        
        if (ingredient is null)
            throw new Exception("Ingredient not found");
        
        ingredient.Update(ingredientDtoName, ingredientDtoQuantity, ingredientDtoUnit);
    }
    
    private void AddIngredient(string ingredientDtoName, double ingredientDtoQuantity, string ingredientDtoUnit)
    {
        var ingredient = new Ingredient(ingredientDtoName, ingredientDtoQuantity, ingredientDtoUnit);
        Ingredients.Add(ingredient);
    }

    public void DeleteIngredients(IEnumerable<Guid> idIngredients)
    {
        var ingredients = Ingredients.Where(x => !idIngredients.Contains(x.Id)).ToList();
        ingredients.ForEach(x => Ingredients.Remove(x));
    }
}