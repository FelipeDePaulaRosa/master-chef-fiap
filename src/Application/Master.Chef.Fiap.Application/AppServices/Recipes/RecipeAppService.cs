using Master.Chef.Fiap.Application.Dtos.Recipes;

namespace Master.Chef.Fiap.Application.AppServices;

public class RecipeAppService : IRecipeAppService
{
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
}