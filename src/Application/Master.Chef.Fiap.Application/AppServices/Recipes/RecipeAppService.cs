using Master.Chef.Fiap.Application.Dtos.Recipes;
using Master.Chef.Fiap.Domain.Entities.Recipes;
using Master.Chef.Fiap.Infrastructure.Repositories.Contracts;

namespace Master.Chef.Fiap.Application.AppServices;

public class RecipeAppService : IRecipeAppService
{
    private readonly IRecipeRepository _recipeRepository;
    
    public RecipeAppService(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }
    
    public Task<GetAllRecipesDto> GetAllRecipesAsync()
    {
        var dto = new GetAllRecipesDto
        {
            Id = Guid.NewGuid(),
            Title = "Recipe Title",
            Description = "Recipe Description"
        };
        
        return Task.FromResult(dto);
    }

    public async Task<GetRecipeDto> GetRecipeByIdAsync(Guid id)
    {
        var recipe = await _recipeRepository.FindByIdOrDefault(id);
        
        if(recipe is null)
            throw new Exception("Recipe not found");
        
        return new GetRecipeDto
        {
            Id = recipe.Id,
            Description = recipe.Description,
            Title = recipe.Title
        };
    }

    public async Task<Guid> CreateRecipeAsync(CreateRecipeDto dto)
    {
        if (dto is null)
            throw new Exception("Dto cannot be null");
        
        
        //TODO: TO COLLECTION
        var ingredients = dto.Ingredients.Select(x => new Ingredient(x.Name, x.Quantity, x.Unit));
        var recipe = new Recipe(dto.OwnerId, dto.Title, dto.Description, (DifficultyLevelEnum) dto.DifficultLevel, ingredients);

        var response = await _recipeRepository.Add(recipe);

        return response.Id;
    }
}