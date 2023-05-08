using Gama.Application.Contracts.Repositories;
using Gama.Domain.Entities;
using Gama.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Gama.Infrastructure.Repositories;

internal class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(GamaCoreDbContext context) : base(context)
    {
    }

    public override async Task<User?> FindOneAsync<TId>(TId id)
    {
        return await FindAll()
            .Include(u => u.Roles)
                .ThenInclude(ur => ur.Role)
                    .FirstOrDefaultAsync(u => u.Id == int.Parse(id.ToString()));
    }

    public async Task<User?> GetAsync(string login)
    {
        return await FindAll()
            .Include(u => u.Roles)
                .ThenInclude(ur => ur.Role)
                    .FirstOrDefaultAsync(f => f.Email == login || f.Username == login);
    }
    
    public async Task<User?> GetAsync(string email, string username)
    {
        return  await FindAll().FirstOrDefaultAsync(f => f.Email == email || f.Username == username);
    }

    public override async Task InsertAsync(User user)
    {
        var roles = _context.Set<Role>().Where(r => user.Roles.Select(s => s.Role.Id).Contains(r.Id)).ToList();

        user.Roles = new List<UserRoles>();

        foreach (var role in roles)
        {
            user.Roles.Add(new UserRoles { Role = role, User = user });
        }

        await base.InsertAsync(user);
    }

    public async Task<IEnumerable<User>> GetAsync(int pageSize, int offset, string role)
    {
        return await FindAll()
            .Join(_context.Set<UserRoles>(), u => u.Id, ur => ur.UserId, (u, ur) => new { User = u, UserRole = ur })
            .Join(_context.Set<Role>(), u => u.UserRole.RoleId, r => r.Id, (u, r) => new { u.User, RoleName = r.Name })
            .Where(ur => ur.RoleName == role)
            .Select(ur => ur.User)
            .Skip(offset)
            .Take(pageSize)
            .ToListAsync();
    }
}