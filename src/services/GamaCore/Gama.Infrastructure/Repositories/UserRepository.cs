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
                        .Include(u => u.UserRoles)
                        .Where(u => u.Id == int.Parse(id.ToString()))
                        .Select(u => new User()
                        {
                            Id = u.Id,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Username = u.Username,
                            Email = u.Email,
                            Password = u.Password,
                            DocumentNumber = u.DocumentNumber,
                            Active = u.Active,
                            UserRoles = u.UserRoles.Select(ur => new UserRoles
                            {
                                UserId = ur.UserId,
                                Role = _context.Set<Role>().FirstOrDefault(r => r.Id == ur.RoleId),
                                RoleId = ur.RoleId
                            }).ToList()
                        })
                        .FirstOrDefaultAsync();
    }

    public async Task<User?> GetAsync(string login)
    {
        return  await FindAll()
                    .Join(_context.Set<UserRoles>(), u => u.Id, ur => ur.UserId, (u, ur) => new { User = u, UserRole = ur })
                    .Join(_context.Set<Role>(), u => u.UserRole.RoleId, r => r.Id, (u, r) => new { u.User, Role = r })
                    .Where(f => f.User.Email == login || f.User.Username == login)
                    .Select(u => new User
                    {
                        Id = u.User.Id,
                        FirstName = u.User.FirstName,
                        LastName = u.User.LastName,
                        Username = u.User.Username,
                        Email = u.User.Email,
                        Password = u.User.Password,
                        DocumentNumber = u.User.DocumentNumber,
                        Active = u.User.Active,
                        UserRoles = u.Role.UserRoles.Where(ur => ur.UserId == u.User.Id).Select(ur => new UserRoles
                        {
                            UserId = ur.UserId,
                            Role = u.Role,
                            RoleId = ur.RoleId
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();
    }
    
    public async Task<User?> GetAsync(string email, string username)
    {
        return  await FindAll().FirstOrDefaultAsync(f => f.Email == email || f.Username == username);
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