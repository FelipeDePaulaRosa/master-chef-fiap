using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public async Task<ActionResult<GetAllRecipesDto>> GetAllRecipes()
    {
        var result = await _appService.GetAllRecipesAsync();
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<GetAllRecipesDto>> GetRecipeById(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest("Id cannot be empty.");
        
        var result = await _appService.GetRecipeByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipeDto dto)
    {
        var result = await _appService.CreateRecipeAsync(dto);
        return CreatedAtAction(nameof(GetRecipeById), new { id = result } ,result);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteRecipe(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest("Id cannot be empty.");
        
        await _appService.DeleteRecipeAsync(id);
        return Ok();
    }
}