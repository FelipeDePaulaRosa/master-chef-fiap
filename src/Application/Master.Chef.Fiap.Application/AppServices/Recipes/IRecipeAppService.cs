using Master.Chef.Fiap.Application.Dtos.Recipes;

namespace Master.Chef.Fiap.Application.AppServices;

public interface IRecipeAppService
{
    Task<IEnumerable<GetAllRecipesDto>> GetAllRecipesAsync();
    Task<GetRecipeDto> GetRecipeByIdAsync(Guid id);
    Task<Guid> CreateRecipeAsync(CreateRecipeDto dto);
    Task DeleteRecipeAsync(Guid id);
}