﻿namespace Master.Chef.Fiap.Application.Dtos.Recipes;

public class GetRecipeDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}