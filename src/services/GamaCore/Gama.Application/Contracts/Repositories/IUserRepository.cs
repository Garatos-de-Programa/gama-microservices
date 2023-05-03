using Gama.Application.DataContracts.Commands;
using Gama.Domain.Entities;

namespace Gama.Application.Contracts.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetAsync(string login);
    Task<User?> GetAsync(string email, string username);
    Task<IEnumerable<User>> GetAsync(int pageSize, int offset);
}