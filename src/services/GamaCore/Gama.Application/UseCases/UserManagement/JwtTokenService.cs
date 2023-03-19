using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Gama.Application.Contracts.UserManagement;
using Gama.Application.DataContracts;
using Gama.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Gama.Application.UseCases.UserManagement;

public class JwtTokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    
    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Generate(User user)
    {
        var issuer = _configuration["AuthSettings:Issuer"];
        var audience = _configuration["AuthSettings:Audience"];
        var key = Encoding.ASCII.GetBytes
            (_configuration["AuthSettings:Key"]);

        var tokenDescriptor = new UserSecurityTokenDescriptor(issuer, audience, key, user);
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        var stringToken = tokenHandler.WriteToken(token);
        return stringToken;
    }
}