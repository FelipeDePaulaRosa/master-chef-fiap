using Microsoft.AspNetCore.Mvc;
using Master.Chef.Fiap.Application.AppServices;
using Master.Chef.Fiap.Application.Dtos.Recipes;

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
        
        var result = await _appService.GetRecipeByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipeDto dto)
    {
        var result = await _appService.CreateRecipeAsync(dto);
        return CreatedAtAction(nameof(GetRecipeById), new { id = result } ,result);
    }
}