using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public static class JwtManager
{
    private static readonly string Secret = ConfigurationManager.AppSettings["JwtSettings:Key"];
    private static readonly string expireTokenInMinutes = ConfigurationManager.AppSettings["ExpireTokenInMinutes"];
    public static string GenerateToken(string userId, string username, string role)
    {
        var symmetricKey = Encoding.UTF8.GetBytes(Secret);
        var tokenHandler = new JwtSecurityTokenHandler();

        var claimsIdentity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        });

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = DateTime.UtcNow.AddMinutes(Util.GetInt(expireTokenInMinutes)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
