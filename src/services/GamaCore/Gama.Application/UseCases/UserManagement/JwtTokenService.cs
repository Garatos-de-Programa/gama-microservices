using Gama.Application.Contracts.UserManagement;
using Gama.Application.DataContracts.Commands;

namespace Gama.Application.UseCases.UserManagement;

public class JwtTokenService : ITokenService
{
    public Task<string> Generate(TokenCreationCommand tokenCreationCommand)
    {
        throw new NotImplementedException();
    }
}