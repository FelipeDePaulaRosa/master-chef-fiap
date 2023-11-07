using System.ComponentModel.DataAnnotations;

namespace Master.Chef.Fiap.Application.Dtos.Recipes;

public class CreateRecipeDto
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public Guid OwnerId { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public short DifficultLevel { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public List<CreateIngredientDto> Ingredients { get; set; }
}

public class CreateIngredientDto
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(250, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public double Quantity { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
    public string Unit { get; set; }
}