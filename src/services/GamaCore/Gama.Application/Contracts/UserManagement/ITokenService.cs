using Gama.Application.DataContracts.Responses.UserManagement;
using Gama.Domain.Models.Users;
using Gama.Domain.ValueTypes;

namespace Gama.Application.Contracts.UserManagement;

public interface ITokenService
{
    Result<AuthenticationResponse> Generate(User user);
}