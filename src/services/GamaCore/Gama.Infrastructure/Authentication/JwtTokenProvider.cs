using System.IdentityModel.Tokens.Jwt;
using Gama.Application.Contracts.UserManagement;
using Gama.Application.DataContracts;
using Gama.Application.Options;
using Gama.Domain.Entities;
using Microsoft.Extensions.Options;

namespace Gama.Infrastructure.Authentication;

public class JwtTokenProvider : ITokenService
{
    private readonly JwtOptions _jwtOptions;
    
    public JwtTokenProvider(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string Generate(User user)
    {
        var tokenDescriptor = new UserSecurityTokenDescriptor(_jwtOptions, user);
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var stringToken = tokenHandler.WriteToken(token);
        return stringToken;
    }
}