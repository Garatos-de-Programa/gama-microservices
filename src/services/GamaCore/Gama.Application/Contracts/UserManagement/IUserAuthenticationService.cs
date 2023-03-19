using Gama.Application.DataContracts.Commands.UserManagement;
using Gama.Application.DataContracts.Responses.UserManagement;
using Gama.Domain.ValueTypes;

namespace Gama.Application.Contracts.UserManagement;

public interface IUserAuthenticationService
{
    Task<Result<AuthenticationResponse>> Authenticate(AuthenticateCommand command);
}