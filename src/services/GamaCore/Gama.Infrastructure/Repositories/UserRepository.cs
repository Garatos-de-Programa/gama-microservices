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

    public async Task<User?> GetByLoginAsync(string login)
    {
        return  await FindAll()
                    .Include(u => u.UserRoles)
                    .Where(f => f.Email == login || f.Username == login)
                    .Select(u => new User
                    {
                        Id = u.Id,
                        Username = u.Username,
                        Email = u.Email,
                        Password = u.Password,
                        UserRoles = u.UserRoles.Select(ur => new UserRoles
                        {
                            UserId = ur.UserId,
                            Role = _context.Set<Role>().FirstOrDefault(r => r.Id == ur.RoleId)
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();
    }
    
    public async Task<User?> GetByLoginAsync(string email, string username)
    {
        return  await FindAll().FirstOrDefaultAsync(f => f.Email == email || f.Username == username);
    }
}