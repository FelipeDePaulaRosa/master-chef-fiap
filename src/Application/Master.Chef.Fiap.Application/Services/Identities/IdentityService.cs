using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Master.Chef.Fiap.Application.Dtos.Identities;

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

    public async Task<List<IdentityUser>> GetIdentityUsersByIds(IEnumerable<string> ids)
    {
        return await _userManager
            .Users
            .AsNoTracking()
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }

    public async Task<IdentityUser> GetIdentityUserById(Guid id)
    {
        return await _userManager.FindByIdAsync(id.ToString());
    }
}