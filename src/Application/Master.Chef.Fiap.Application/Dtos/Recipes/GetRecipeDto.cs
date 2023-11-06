using Master.Chef.Fiap.CrossCutting.Extensions;
using Master.Chef.Fiap.Domain.Entities.Recipes;

namespace Master.Chef.Fiap.Application.Dtos.Recipes;

public class GetRecipeDto
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public string OwnerName { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string DifficultLevel { get; set; }
    public short DifficultLevelId { get; set; }
    public IEnumerable<GetIngredientDto> Ingredients { get; set; }

    public static GetRecipeDto BuildRecipeDTO(Recipe recipe, string userName)
    {
        var ingredients = BuildIngredientDTOs(recipe.Ingredients);
        return new GetRecipeDto
        {
            Id = recipe.Id,
            OwnerId = recipe.OwnerId,
            OwnerName = userName,
            Title = recipe.Title,
            Description = recipe.Description,
            DifficultLevel = recipe.GetDifficultLevelDescription(),
            DifficultLevelId = recipe.GetDifficultLevelId(),
            Ingredients = ingredients
        };
    }
    
    public static IEnumerable<GetIngredientDto> BuildIngredientDTOs(IEnumerable<Ingredient> ingredients)
    {
        var getIngredientDTOs = new List<GetIngredientDto>();
        foreach (var ingredient in ingredients)
        {
            var getIngredientDto = new GetIngredientDto
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Quantity = ingredient.Quantity,
                Unit = ingredient.Unit
            };
            getIngredientDTOs.Add(getIngredientDto);
        }
        return getIngredientDTOs;
    }
}

public class GetIngredientDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Quantity { get; set; }
    public string Unit { get; set; }
}

