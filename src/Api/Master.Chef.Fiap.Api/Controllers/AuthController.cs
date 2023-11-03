using Master.Chef.Fiap.Application.AppServices.Auths;
using Master.Chef.Fiap.Application.Dtos.Auths;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Master.Chef.Fiap.Api.Controllers;

public class AuthController : MainController
{
    private readonly IAuthAppService _appService;
    
    public AuthController(IAuthAppService appService)
    {
        _appService = appService;
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> Authenticate(LoginUserDto dto)
    {
        var result = await _appService.Authenticate(dto);
        return Ok(result);
    }
}