namespace Master.Chef.Fiap.Application.Dtos.Identities;

public class RegisterUserResponseDto
{
    public bool Success { get; set; }
    public List<string> Errors { get; set; }
    
    public RegisterUserResponseDto()
    { 
        Errors = new List<string>();
        Success = true;
    }

    public RegisterUserResponseDto(IEnumerable<string> errors)
    {
        Errors = new List<string>();
        Errors.AddRange(errors);
        Success = false;
    }
}