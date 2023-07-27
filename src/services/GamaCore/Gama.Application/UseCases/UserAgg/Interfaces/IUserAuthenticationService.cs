using Gama.Application.UseCases.UserAgg.Commands;
using Gama.Application.UseCases.UserAgg.Responses;
using Gama.Domain.ValueTypes;

namespace Gama.Application.UseCases.UserAgg.Interfaces;

public interface IUserAuthenticationService
{
    Task<Result<AuthenticationResponse>> AuthenticateAsync(AuthenticateCommand command);
}