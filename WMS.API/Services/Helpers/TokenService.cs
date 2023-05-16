using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using WMS.Data.DTO.Helpers;
using WMS.Data.Entity.Identity;

namespace WMS.API.Services.Helpers;

public class TokenService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;

    public TokenService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<TokenDto> BuildToken(ApplicationUser userInfo)
    {
        var claims = new List<Claim>
        {
            new("UserName", userInfo.UserName),
            new("Email", string.IsNullOrEmpty(userInfo.Email) ? "" : userInfo.Email)
        };

        var roles = await _userManager.GetRolesAsync(userInfo);
        claims.AddRange(roles.Select(role => new Claim("Roles", role)));
        // claims.Add(new Claim(type: "Roles", value: "User"));

        var identityUser = await _userManager.FindByNameAsync(userInfo.UserName);
        var claimsDb = await _userManager.GetClaimsAsync(identityUser);

        claims.AddRange(claimsDb);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.UtcNow.AddHours(24);

        var token = new JwtSecurityToken(
            null,
            null,
            claims,
            expires: expiration,
            signingCredentials: credentials);

        return new TokenDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            TokenExpire = expiration.ToUniversalTime().ToString("u")
        };
    }
}