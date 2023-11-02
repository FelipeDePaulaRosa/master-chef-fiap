using Master.Chef.Fiap.Application.Dtos.Auths;
using Master.Chef.Fiap.Application.Services;
using Master.Chef.Fiap.Application.Services.Auths;

namespace Master.Chef.Fiap.Application.AppServices.Auths;

public class AuthAppService : IAuthAppService
{
    private readonly IAuthService _authService;
    private readonly IIdentityService _identityService;
    
    public AuthAppService(IAuthService authService, IIdentityService identityService)
    {
        _authService = authService;
        _identityService = identityService;
    }
    
    public async Task<TokenDto> Authenticate(LoginUserDto dto)
    {
        await _authService.SignIn(dto);
        var identityUser = await _identityService.GetIdentityUserByEmail(dto.UserName);
        return _authService.GenerateToken(identityUser);
    }
}