using Master.Chef.Fiap.Application.AppServices;
using Master.Chef.Fiap.Application.Dtos.Recipes;
using Microsoft.AspNetCore.Mvc;

namespace Master.Chef.Fiap.Api.Controllers;

public class RecipeController : MainController
{
    private readonly IRecipeAppService _appService;
    
    public RecipeController(IRecipeAppService appService)
    {
        _appService = appService;
    }
    
    [HttpGet]
    public async Task<ActionResult<GetAllRecipesDto>> GetAllRecipes()
    {
        var result = await _appService.GetAllRecipesAsync();
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<GetAllRecipesDto>> GetRecipeById(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest();
        
        var result = await _appService.GetAllRecipesAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRecipe()
    {
        var result = new GetAllRecipesDto
        {
            Id = Guid.NewGuid(),
            Title = "Recipe Title",
            Description = "Recipe Description"
        };
     
        return CreatedAtAction(nameof(GetRecipeById), new { id = result.Id } ,result);
    }
}