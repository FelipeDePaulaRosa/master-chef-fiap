using Master.Chef.Fiap.Application.Dtos.Identities;

namespace Master.Chef.Fiap.Application.Services;

public interface IIdentityService
{
    Task<RegisterUserResponseDto> Register(RegisterUserDto dto);
}