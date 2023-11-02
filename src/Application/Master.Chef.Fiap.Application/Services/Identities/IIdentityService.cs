using Master.Chef.Fiap.Application.Dtos.Identities;
using Microsoft.AspNetCore.Identity;

namespace Master.Chef.Fiap.Application.Services;

public interface IIdentityService
{
    Task<RegisterUserResponseDto> Register(RegisterUserDto dto);
    Task<IdentityUser> GetIdentityUserByEmail(string email);
}