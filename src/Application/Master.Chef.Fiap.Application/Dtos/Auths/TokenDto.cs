namespace Master.Chef.Fiap.Application.Dtos.Auths;

public class TokenDto
{
    public string Token { get; set; }
    public double ExpiresIn { get; set; }
    public List<Dictionary<string, string>> Claims { get; set; }
}