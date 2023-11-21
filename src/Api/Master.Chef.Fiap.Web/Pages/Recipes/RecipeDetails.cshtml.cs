using Master.Chef.Fiap.Application.AppServices;
using Master.Chef.Fiap.Application.Dtos.Recipes;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Master.Chef.Fiap.Web.Pages.Recipes;

public class RecipeDetails : PageModel
{
    private readonly IRecipeAppService _appService;
    public GetRecipeDto Recipe { get; set; }
    
    public RecipeDetails(IRecipeAppService appService)
    {
        _appService = appService;
    }
    
    public async Task OnGet(Guid id)
    {
        Recipe = await _appService.GetRecipeByIdAsync(id);
    }
}