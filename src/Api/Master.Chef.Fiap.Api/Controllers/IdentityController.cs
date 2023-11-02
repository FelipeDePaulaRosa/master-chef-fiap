using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Master.Chef.Fiap.Application.Dtos.Identities;
using Master.Chef.Fiap.Application.AppServices.Identities;

namespace Master.Chef.Fiap.Api.Controllers;

public class IdentityController : MainController
{
    private readonly IIdentityAppService _appService;
    
    public IdentityController(IIdentityAppService appService)
    {
        _appService = appService;
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<RegisterUserResponseDto>> Register(RegisterUserDto dto)
    {
        var result = await _appService.Register(dto);
        if (!result.Success)
            return BadRequest(result.Errors);
        
        return CreatedAtAction(nameof(Register), result);
    }
}