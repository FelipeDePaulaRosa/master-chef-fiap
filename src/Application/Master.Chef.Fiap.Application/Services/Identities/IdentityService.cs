using Master.Chef.Fiap.Application.Dtos.Identities;
using Microsoft.AspNetCore.Identity;

namespace Master.Chef.Fiap.Application.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<IdentityUser> _userManager;
    
    public IdentityService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<RegisterUserResponseDto> Register(RegisterUserDto dto)
    {
        var identityUser = new IdentityUser
        {
            UserName = dto.UserName,
            Email = dto.Email,
            EmailConfirmed = true
        };
        
        var result = await _userManager.CreateAsync(identityUser, dto.Password);

        if (!result.Succeeded)
            return new RegisterUserResponseDto(result.Errors.Select(e => e.Description));
            
        await _userManager.SetLockoutEnabledAsync(identityUser, false);
        return new RegisterUserResponseDto();
    }
    
    public async Task<IdentityUser> GetIdentityUserByEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        if(user == null)
            throw new Exception("Usuário não encontrado.");
        
        return user;
    }
}