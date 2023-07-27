using Gama.Application.Options;
using Gama.Application.UseCases.UserAgg.Implementations;
using Gama.Application.UseCases.UserAgg.Interfaces;
using Gama.Application.UseCases.UserAgg.Responses;
using Gama.Domain.Models.Users;
using Gama.Domain.ValueTypes;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

namespace Gama.Infrastructure.Authentication;

public class JwtTokenProvider : ITokenService
{
    private readonly JwtOptions _jwtOptions;
    
    public JwtTokenProvider(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public Result<AuthenticationResponse> Generate(User user)
    {
        var tokenDescriptor = new UserSecurityTokenDescriptor(_jwtOptions, user);
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var stringToken = tokenHandler.WriteToken(token);
        return new Result<AuthenticationResponse>(new AuthenticationResponse()
        {
            Token = stringToken,
            ExpiresIn = tokenDescriptor.Expires!.Value.Subtract(DateTime.MinValue).TotalSeconds
        });
    }
}