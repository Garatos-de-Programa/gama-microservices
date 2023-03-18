using Gama.Application.DataContracts.Commands;
using Gama.Domain.Entities;

namespace Gama.Application.Contracts.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByLogin(TokenCreationCommand tokenCreationCommand);
}