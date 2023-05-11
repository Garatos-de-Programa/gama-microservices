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

        public async override Task<Occurrence?> FindOneAsync<TId>(TId id)
        {
            return await _context.Set<Occurrence>()
                            .Include(o => o.OccurrenceType)
                            .Include(o => o.OccurrenceUrgencyLevel)
                            .Include(o => o.Status)
                            .FirstOrDefaultAsync(o => o.Id == int.Parse(id.ToString()));
        }

        public async Task<IEnumerable<Occurrence>> GetAsync(DateSearchQuery search, int offset, int size)
        {
            return await FindAll()
                    .Include(o => o.OccurrenceType)
                    .Include(o => o.Status)
                    .Include(o => o.OccurrenceUrgencyLevel)
                    .OrderBy(x => x.CreatedAt)
                    .Where(t => t.CreatedAt >= search.CreatedSince.ToUniversalTime() && t.CreatedAt <= search.CreatedUntil.ToUniversalTime())
                    .Skip(offset)
                    .Take(size)
                    .ToListAsync();
        }
    }
}
