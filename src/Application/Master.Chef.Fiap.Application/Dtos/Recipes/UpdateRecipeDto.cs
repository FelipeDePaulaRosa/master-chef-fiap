using System.ComponentModel.DataAnnotations;

namespace Master.Chef.Fiap.Application.Dtos.Recipes;

public class UpdateRecipeDto
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(500, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
    public string Summary { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public short DifficultLevel { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public List<UpdateIngredientDto> Ingredients { get; set; }
}

public class UpdateIngredientDto
{
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(250, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public double Quantity { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
    public string Unit { get; set; }
}