using Master.Chef.Fiap.Application.Dtos.Identities;
using Master.Chef.Fiap.Application.Services;

namespace Master.Chef.Fiap.Application.AppServices.Identities;

public class IdentityAppService : IIdentityAppService
{
    private readonly IIdentityService _identityService;
    
    public IdentityAppService(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<RegisterUserResponseDto> Register(RegisterUserDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));
        
        return await _identityService.Register(dto);
    }
}