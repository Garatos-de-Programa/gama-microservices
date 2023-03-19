using Gama.Application.Contracts.Repositories;
using Gama.Domain.Entities;
using Gama.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Gama.Infrastructure.Repositories;

internal class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(GamaCoreDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByLoginAsync(string email, string username)
    {
        return  await FindAll().SingleOrDefaultAsync(f => f.Email == email || f.Username == username);
    }
}