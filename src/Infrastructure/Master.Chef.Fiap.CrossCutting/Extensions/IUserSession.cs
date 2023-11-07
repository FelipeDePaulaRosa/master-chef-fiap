namespace Master.Chef.Fiap.CrossCutting.Extensions;

public interface IUserSession
{
    public string UserName { get; }
    public Guid UserId { get; }
    public string Email { get; }
}