using Gama.Application.Contracts.Repositories;
using Gama.Domain.Entities;
using Gama.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Gama.Infrastructure.Repositories
{
    internal class RolesRepository : Repository<Role>, IRolesRepository
    {
        public RolesRepository(GamaCoreDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Role>> GetByIdsAsync(IEnumerable<byte> ids)
        {
            return await FindAll().Where(r => ids.All(id => r.Id == id)).ToListAsync();
        }
    }
}
