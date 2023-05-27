using Gama.Application.Contracts.EventBus;
using Gama.Application.Contracts.Repositories;
using Gama.Application.DataContracts.Queries.Common;
using Gama.Domain.Models.Occurrences;
using Gama.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Gama.Infrastructure.Repositories
{
    internal class OccurrenceRepository : Repository<Occurrence>, IOccurrenceRepository
    {
        private readonly IEventBusProducer _eventBusProducer;
        public OccurrenceRepository(GamaCoreDbContext dbContext, IEventBusProducer eventBusProducer) : base(dbContext)
        {
            _eventBusProducer = eventBusProducer;
        }

        public override async Task InsertAsync(Occurrence tObject)
        {
            await base.InsertAsync(tObject);
            await CommitAsync().ConfigureAwait(false);
            PublishMessage(tObject);
        }

        public override async Task Patch(Occurrence tObject)
        {
            PublishMessage(tObject);
            await base.Patch(tObject);
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

        internal void PublishMessage(Occurrence tObject)
        {
            var @event = tObject.Events.First();

            if (@event != null)
            {
                if (@event is CreatedOccurrenceEvent createdEvent)
                {
                    createdEvent.OccurrenceId = tObject.Id;
                }
                _eventBusProducer.Publish(@event, "gama.api:event-occurrences");
            }
        }
    }
}
