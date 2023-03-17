using Gama.Application.DataContracts.Commands;

namespace Gama.Application.Contracts.UserManagement;

public interface ITokenService
{
    Task<string> Generate(TokenCreationCommand tokenCreationCommand);
}