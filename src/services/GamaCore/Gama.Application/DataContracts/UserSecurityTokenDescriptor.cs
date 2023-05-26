using System.Security.Claims;
using Gama.Application.Options;
using Gama.Domain.Models.Users;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Gama.Application.DataContracts;

public class UserSecurityTokenDescriptor : SecurityTokenDescriptor
{
    public UserSecurityTokenDescriptor(JwtOptions jwtOptions, User user)
    {
        var claims = new List<Claim>()
        {
            new Claim("Id", user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString()),
        };

        foreach (var role in user?.Roles ?? new List<UserRoles>())
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Role.Name.ToString()));
        }

        Subject = new ClaimsIdentity(claims);
        Expires = DateTime.UtcNow.AddHours(2);
        Issuer = jwtOptions.Issuer;
        Audience = jwtOptions.Audience;
        SigningCredentials = new SigningCredentials
        (new SymmetricSecurityKey(jwtOptions.GetSecretKeyBytes()),
            SecurityAlgorithms.HmacSha256);
    }
}