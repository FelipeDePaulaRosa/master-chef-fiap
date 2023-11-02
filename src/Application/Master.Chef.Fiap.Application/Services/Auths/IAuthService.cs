using Master.Chef.Fiap.Application.Dtos.Auths;
using Microsoft.AspNetCore.Identity;

namespace Master.Chef.Fiap.Application.Services.Auths;

public interface IAuthService
{
    Task SignIn(LoginUserDto dto);
    TokenDto GenerateToken(IdentityUser identityUser);
}