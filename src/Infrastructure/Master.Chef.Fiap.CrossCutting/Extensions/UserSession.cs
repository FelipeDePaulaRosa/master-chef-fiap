using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Master.Chef.Fiap.CrossCutting.Extensions;

public class UserSession : IUserSession
{
    private readonly IHttpContextAccessor _accessor;

    public UserSession(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }   
    
    public string UserName => GetUserName();
    public Guid UserId => GetUserId();
    public string Email => GetUserEmail();

    private string GetUserName()
    {
        var claims = GetClaimsIdentity();
        
        if(!claims.Any())
            return string.Empty;
        
        var userName = claims
            .Where(x => x.Type.Equals("userName"))?
            .FirstOrDefault();

        return userName?.Value ?? string.Empty;
    }
    
    private Guid GetUserId()
    {
        var claims = GetClaimsIdentity();
        
        if(!claims.Any())
            return Guid.Empty;
        
        var userId = claims
            .Where(x => x.Type.Equals("userId"))?
            .FirstOrDefault();

        return userId is null ? Guid.Empty : Guid.Parse(userId?.Value!);
    }
    
    private string GetUserEmail()
    {
        var claims = GetClaimsIdentity();
        
        if(!claims.Any())
            return string.Empty;
        
        var userEmail = claims
            .Where(x => x.Type.Equals("email"))?
            .FirstOrDefault();

        return userEmail?.Value ?? string.Empty;
    }
    
    private List<Claim> GetClaimsIdentity()
    {
        return _accessor.HttpContext?.User?.Claims.ToList() ?? new List<Claim>();
    }
}