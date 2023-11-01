using Master.Chef.Fiap.Application.Dtos.Recipes;

namespace Master.Chef.Fiap.Application.AppServices;

public interface IRecipeAppService
{
    Task<GetAllRecipesDto> GetAllRecipesAsync();
    
}