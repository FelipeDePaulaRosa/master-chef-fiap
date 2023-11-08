namespace Master.Chef.Fiap.Application.Dtos.Recipes;

public class GetAllRecipesDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public string OwnerName { get; set; }
    public short DifficultLevel { get; set; }
}