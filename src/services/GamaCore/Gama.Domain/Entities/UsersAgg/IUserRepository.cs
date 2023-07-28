using Gama.Domain.Interfaces.Repositories;
using Gama.Domain.ValueTypes;

namespace Gama.Domain.Entities.UsersAgg;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetAsync(string login);
    Task<User?> GetAsync(string email, string username);
    Task<OffsetPage<User>> GetAsync(int pageSize, int pageNumber, string role);
}