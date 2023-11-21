using Master.Chef.Fiap.Application.AppServices;
using Master.Chef.Fiap.Application.Dtos.Recipes;
using Master.Chef.Fiap.CrossCutting.Extensions;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Master.Chef.Fiap.Web.Pages.Recipes;

public class MyRecipes : PageModel
{
    private readonly IRecipeAppService _appService;
    private readonly IUserSession _userSession;
    public IEnumerable<GetAllRecipesDto> Recipes { get; set; }

    public MyRecipes(IRecipeAppService appService, IUserSession userSession)
    {
        _appService = appService;
        _userSession = userSession;
        Recipes = new List<GetAllRecipesDto>();
    }
    
    
    public async Task OnGet()
    {
        Recipes = await _appService.GetRecipesByOwnerIdAsync(_userSession.UserId, _userSession.UserName);
    }
}