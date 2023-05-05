using Gama.Application.Contracts.Repositories;
using Gama.Application.DataContracts.Queries.Common;
using Gama.Domain.Entities;
using Gama.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Gama.Infrastructure.Repositories
{
    internal class TrafficFineRepository : Repository<TrafficFine>, ITrafficFineRepository
    {
        public TrafficFineRepository(GamaCoreDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<TrafficFine>> GetAsync(DateSearchQuery dateSearchQuery, int offset, int size)
        {
            return await FindAll()
                    .OrderBy(x => x.CreatedAt)
                    .Where(t => t.CreatedAt >= dateSearchQuery.CreatedSince.ToUniversalTime() && t.CreatedAt <= dateSearchQuery.CreatedUntil.ToUniversalTime())
                    .Skip(offset)
                    .Take(size)
                    .ToListAsync();
        }
    }
}
