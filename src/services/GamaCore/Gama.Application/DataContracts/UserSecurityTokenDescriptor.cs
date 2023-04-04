using System.Security.Claims;
using Gama.Application.Options;
using Gama.Domain.Entities;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Gama.Application.DataContracts;

public class UserSecurityTokenDescriptor : SecurityTokenDescriptor
{
    public UserSecurityTokenDescriptor(JwtOptions jwtOptions, User user)
    {
        Subject = new ClaimsIdentity(new[]
        {
            new Claim("Id", user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString()),
            new Claim("Role", user.Role.ToString())
        });
        Expires = DateTime.UtcNow.AddMinutes(1);
        Issuer = jwtOptions.Issuer;
        Audience = jwtOptions.Audience;
        SigningCredentials = new SigningCredentials
        (new SymmetricSecurityKey(jwtOptions.GetSecretKeyBytes()),
            SecurityAlgorithms.HmacSha256);
    }
}