using Microsoft.AspNetCore.Identity;
using Master.Chef.Fiap.Application.Dtos.Identities;

namespace Master.Chef.Fiap.Application.Services;

public interface IIdentityService
{
    Task<RegisterUserResponseDto> Register(RegisterUserDto dto);
    Task<IdentityUser> GetIdentityUserByEmail(string email);
    Task<List<IdentityUser>> GetIdentityUsersByIds(IEnumerable<string> ids);
    Task<IdentityUser> GetIdentityUserById(Guid recipeOwnerId);
}