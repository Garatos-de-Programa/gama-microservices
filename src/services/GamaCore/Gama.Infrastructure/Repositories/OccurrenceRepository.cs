using Gama.Application.Contracts.Repositories;
using Gama.Application.DataContracts.Queries.Common;
using Gama.Domain.Entities;
using Gama.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Gama.Infrastructure.Repositories
{
    internal class OccurrenceRepository : Repository<Occurrence>, IOccurrenceRepository
    {
        public OccurrenceRepository(GamaCoreDbContext dbContext) : base(dbContext)
        {
        }

        public override Task InsertAsync(Occurrence tObject)
        {
            

            return base.InsertAsync(tObject);
        }

        public async Task<IEnumerable<Occurrence>> GetAsync(DateSearchQuery search, int offset, int size)
        {
            return await FindAll()
                    .OrderBy(x => x.CreatedAt)
                    .Where(t => t.CreatedAt >= search.CreatedSince.ToUniversalTime() && t.CreatedAt <= search.CreatedUntil.ToUniversalTime())
                    .Skip(offset)
                    .Take(size)
                    .ToListAsync();
        }
    }
}
