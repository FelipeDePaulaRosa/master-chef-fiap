using Master.Chef.Fiap.Application.Dtos.Auths;

namespace Master.Chef.Fiap.Application.AppServices.Auths;

public interface IAuthAppService
{
    Task<TokenDto> Authenticate(LoginUserDto dto);
}