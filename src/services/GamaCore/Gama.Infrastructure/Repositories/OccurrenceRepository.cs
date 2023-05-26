using Gama.Application.Contracts.Repositories;
using Gama.Application.DataContracts.Queries.Common;
using Gama.Domain.Models.Occurrences;
using Gama.Infrastructure.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Gama.Infrastructure.Repositories
{
    internal class OccurrenceRepository : Repository<Occurrence>, IOccurrenceRepository
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public OccurrenceRepository(GamaCoreDbContext dbContext, IPublishEndpoint publishEndpoint) : base(dbContext)
        {
            _publishEndpoint = publishEndpoint;
        }

        public override Task InsertAsync(Occurrence tObject)
        {
            _publishEndpoint.Publish(tObject.Events);
            return base.InsertAsync(tObject);
        }

        public override Task Patch(Occurrence tObject)
        {
            _publishEndpoint.Publish(tObject.Events);
            return base.Patch(tObject);
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
