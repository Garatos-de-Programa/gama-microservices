using Gama.Application.DataContracts.Commands;
using Gama.Domain.ValueTypes;

namespace Gama.Application.Contracts.UserManagement;

public interface ITokenService
{
    Task<Result<string>> Generate(TokenCreationCommand tokenCreationCommand);
}