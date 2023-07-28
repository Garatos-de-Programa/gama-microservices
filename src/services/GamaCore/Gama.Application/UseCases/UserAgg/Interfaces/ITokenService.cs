using Gama.Application.UseCases.UserAgg.Responses;
using Gama.Domain.Entities.UsersAgg;
using Gama.Domain.ValueTypes;

namespace Gama.Application.UseCases.UserAgg.Interfaces;

public interface ITokenService
{
    Result<AuthenticationResponse> Generate(User user);
}