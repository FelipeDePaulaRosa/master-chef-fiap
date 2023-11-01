using Master.Chef.Fiap.Application.Dtos.Identities;
using Microsoft.AspNetCore.Identity;

namespace Master.Chef.Fiap.Application.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    
    public IdentityService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }


    public async Task<RegisterUserResponseDto> Register(RegisterUserDto dto)
    {
        var identityUser = new IdentityUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            EmailConfirmed = true
        };
        
        var result = await _userManager.CreateAsync(identityUser, dto.Password);

        if (!result.Succeeded)
            return new RegisterUserResponseDto(result.Errors.Select(e => e.Description));
            
        await _userManager.SetLockoutEnabledAsync(identityUser, false);
        
        return new RegisterUserResponseDto();
    }
}