using Master.Chef.Fiap.Application.AppServices.Auths;
using Master.Chef.Fiap.Application.Dtos.Auths;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Master.Chef.Fiap.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IAuthAppService _appService;

    [BindProperty]
    public string Username { get; set; }

    [BindProperty]
    public string Password { get; set; }
    
    public IndexModel(ILogger<IndexModel> logger, IAuthAppService appService)
    {
        _logger = logger;
        _appService = appService;
    }
    
    public async Task<IActionResult> OnPost()
    {
        var loginDto = new LoginUserDto { UserName = this.Username, Password = this.Password };
        var token = await _appService.Authenticate(loginDto);
        
        if (token.Token is not null)
            return RedirectToPage("/Recipes/RecipeList");

        ModelState.AddModelError(string.Empty, "Invalid username or password");
        return Page();
    }
}