using Master.Chef.Fiap.Application.Dtos.Identities;

namespace Master.Chef.Fiap.Application.AppServices.Identities;

public interface IIdentityAppService
{
    Task<RegisterUserResponseDto> Register(RegisterUserDto dto);
}