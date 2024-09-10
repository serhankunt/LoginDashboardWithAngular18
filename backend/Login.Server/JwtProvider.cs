using Login.Server.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Login.Server;

public class JwtProvider
{
    public string CreateToken(AppUser user)
    {
        var claims = new Claim[]
        {
            new("UserId",user.Id.ToString()),
            new("UserName",user.UserName!.ToString())
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretKey123 MySecretKey123 MySecretKey123 MySecretKey123 MySecretKey123"));

        JwtSecurityToken securityToken = new(
            issuer: "Serhan Kunt",
            audience: "Deneme",
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512));

        JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
        return securityTokenHandler.WriteToken(securityToken);
    }
}
