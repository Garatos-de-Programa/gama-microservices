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
                            Address = new UserAddress
                            {
                                Id = u.Address.Id,
                                Street = u.Address.Street,
                                City = u.Address.City,
                                State = u.Address.State,
                                ZipCode = u.Address.ZipCode,
                                District = u.Address.District,
                                UserId = u.Address.UserId,
                            },
                            UserRoles = u.UserRoles.Select(ur => new UserRoles
                            {
                                UserId = ur.UserId,
                                Role = _context.Set<Role>().FirstOrDefault(r => r.Id == ur.RoleId),
                                RoleId = ur.RoleId
                            }).ToList()
                        })
                        .FirstOrDefaultAsync();
    }

    public async Task<User?> GetByLoginAsync(string login)
    {
        return  await FindAll()
                    .Include(u => u.UserRoles)
                    .Where(f => f.Email == login || f.Username == login)
                    .Select(u => new User
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
    
    public async Task<User?> GetByLoginAsync(string email, string username)
    {
        return  await FindAll().FirstOrDefaultAsync(f => f.Email == email || f.Username == username);
    }
}