using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoApp.Core.Src.Entities;
using TodoApp.Core.Src.Services;
using TodoApp.Shared.Src;

namespace TodoApp.Infra.Src.Services;

public class TokenService : ITokenService
{
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("uuid", user.Id.ToString())
                //new Claim(ClaimTypes.NameIdentifier, user.Contact.EmailAddress),
                //new Claim(ClaimTypes.Sid, user.Id.ToString()),
                //new Claim(ClaimTypes.Role, user.RoleType.Description)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
