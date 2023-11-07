using Master.Chef.Fiap.Application.Dtos.Recipes;
using Master.Chef.Fiap.CrossCutting.Extensions;
using Master.Chef.Fiap.Domain.Entities.Recipes;
using Master.Chef.Fiap.Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Master.Chef.Fiap.Application.AppServices;

public class RecipeAppService : IRecipeAppService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IUserSession _userSession;
    
    public RecipeAppService(IRecipeRepository recipeRepository, IUserSession userSession)
    {
        _recipeRepository = recipeRepository;
        _userSession = userSession;
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
        var recipe = await _recipeRepository
            .GetQueryable<Recipe>()
            .AsNoTracking()
            .Include(x => x.Ingredients)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
        
        if(recipe is null)
            throw new Exception("Recipe not found");

        return GetRecipeDto.BuildRecipeDTO(recipe, _userSession.UserName);
    }

    public async Task<Guid> CreateRecipeAsync(CreateRecipeDto dto)
    {
        if (dto is null)
            throw new Exception("Dto cannot be null");
        
        var ingredients = dto.Ingredients.Select(x => new Ingredient(x.Name, x.Quantity, x.Unit));
        var recipe = new Recipe(dto.OwnerId, dto.Title, dto.Description, (DifficultyLevelEnum) dto.DifficultLevel, ingredients);

        var response = await _recipeRepository.Add(recipe);

        return response.Id;
    }
}