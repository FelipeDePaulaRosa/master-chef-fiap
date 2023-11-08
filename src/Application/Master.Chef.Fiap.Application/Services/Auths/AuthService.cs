using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Master.Chef.Fiap.Application.Dtos.Auths;

namespace Master.Chef.Fiap.Application.Services.Auths;

public class AuthService : IAuthService
{
    private readonly SignInManager<IdentityUser> _signInManager;
    
    public AuthService(SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }
    
    public async Task SignIn(LoginUserDto dto)
    {
        var signInResult = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, true);
        
        if(signInResult.IsLockedOut)
            throw new Exception("Usuário temporariamente bloqueado por tentativas inválidas.");
        else if(signInResult.IsNotAllowed)
            throw new Exception("Usuário ou senha incorretos.");
    }

    public TokenDto GenerateToken(IdentityUser identityUser)
    {
        var claims = new List<Claim>
        {
            new ("userName", identityUser.UserName!),
            new ("email", identityUser.Email!),
            new ("userId", identityUser.Id)
        };
        
        var key = new SymmetricSecurityKey("Api&Key&Master_Chef&Extreme&Secure!"u8.ToArray());
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: credentials,
            expires: DateTime.UtcNow.AddHours(8),
            issuer: "Master.Chef.Fiap",
            audience: "https://localhost:5036"
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        if (string.IsNullOrEmpty(tokenString)) 
            throw new Exception("Algo de errado ocorreu ao gerar o token de acesso.");

        List<Dictionary<string, string>> claimDictionary = new();
        claims.Where(x => x.Type != "name").ToList().ForEach(claim =>
        {
            Dictionary<string, string> dict = new();
            dict.Add(claim.Type, claim.Value);
            claimDictionary.Add(dict);
        });

        TokenDto tokenResponse = new()
        {
            Token = tokenString,
            ExpiresIn = TimeSpan.FromHours(8).TotalSeconds,
            Claims = claimDictionary
        };
        return tokenResponse;
    }
}