using Master.Chef.Fiap.Application.AppServices;
using Master.Chef.Fiap.Application.Dtos.Recipes;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Master.Chef.Fiap.Web.Pages.Recipes;

public class RecipeList : PageModel
{
    private readonly IRecipeAppService _appService;
    public List<GetAllRecipesDto> Recipes { get; set; }

    public RecipeList(IRecipeAppService appService)
    {
        _appService = appService;
        Recipes = new List<GetAllRecipesDto>();
    }


    public async Task OnGet()
    {
        var recipes = await _appService.GetAllRecipesAsync();
        Recipes.AddRange(recipes);
        Recipes.AddRange(recipes);
        Recipes.AddRange(recipes);
    }
}