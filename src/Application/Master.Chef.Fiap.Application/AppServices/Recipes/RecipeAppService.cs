﻿using Microsoft.EntityFrameworkCore;
using Master.Chef.Fiap.Application.Services;
using Master.Chef.Fiap.CrossCutting.Extensions;
using Master.Chef.Fiap.Domain.Entities.Recipes;
using Master.Chef.Fiap.Application.Dtos.Recipes;
using Master.Chef.Fiap.Infrastructure.Repositories.Contracts;

namespace Master.Chef.Fiap.Application.AppServices;

public class RecipeAppService : IRecipeAppService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IIdentityService _identityService;
    private readonly IUserSession _userSession;
    
    public RecipeAppService(IRecipeRepository recipeRepository, IIdentityService identityService, IUserSession userSession)
    {
        _recipeRepository = recipeRepository;
        _identityService = identityService;
        _userSession = userSession;
    }
    
    public async Task<IEnumerable<GetAllRecipesDto>> GetAllRecipesAsync()
    {
        var recipes = await _recipeRepository.GetAll();
        
        if (recipes is null || !recipes.Any())
            return new List<GetAllRecipesDto>();
        
        var idsOfUsers = recipes.Select(x => x.OwnerId.ToString()).Distinct();
        var users = await _identityService.GetIdentityUsersByIds(idsOfUsers);
        
        var recipesDtos = recipes.Select(recipe => new GetAllRecipesDto
        {
            Id = recipe.Id,
            Title = recipe.Title,
            Summary = recipe.Summary,
            OwnerName = users.FirstOrDefault(x => x.Id == recipe.OwnerId.ToString())!.UserName!,
            DifficultLevel = recipe.GetDifficultLevelId()
        });
        
        return recipesDtos;
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

        var user = await _identityService.GetIdentityUserById(recipe.OwnerId);

        return GetRecipeDto.BuildRecipeDTO(recipe, user.UserName);
    }

    public async Task<Guid> CreateRecipeAsync(CreateRecipeDto dto)
    {
        if (dto is null)
            throw new Exception("Dto cannot be null");
        
        var ingredients = dto.Ingredients.Select(x => new Ingredient(x.Name, x.Quantity, x.Unit));
        var recipe = new Recipe(dto.OwnerId, dto.Title, dto.Description, dto.Summary, (DifficultyLevelEnum) dto.DifficultLevel, ingredients);

        var response = await _recipeRepository.Add(recipe);

        return response.Id;
    }

    public async Task DeleteRecipeAsync(Guid id)
    {
        var recipe = await _recipeRepository
            .GetQueryable<Recipe>()
            .Include(x => x.Ingredients)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
        
        if(recipe is null)
            throw new Exception("Recipe not found");
        
        await _recipeRepository.Delete(recipe);
    }

    public async Task UpdateRecipeAsync(Guid id, UpdateRecipeDto dto)
    {
        var recipe = await _recipeRepository
            .GetQueryable<Recipe>()
            .Include(x => x.Ingredients)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if(recipe is null)  
            throw new Exception("Recipe not found");
        
        if (_userSession.UserId != recipe.OwnerId)
            throw new Exception("You are not the owner of this recipe");

        recipe.Update(dto.Title, dto.Description, dto.Summary, (DifficultyLevelEnum) dto.DifficultLevel);
        
        dto.Ingredients.ForEach(ingredientDto => recipe.UpdateIngredient(
            ingredientDto.Id, 
            ingredientDto.Name, 
            ingredientDto.Quantity, 
            ingredientDto.Unit)
        );
        
        recipe.DeleteIngredients(dto.Ingredients.Select(x => x.Id));
        
        await _recipeRepository.Update(recipe);
    }
}