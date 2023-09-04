using Gama.Domain.Entities.OccurrencesAgg.Events;
using Gama.Domain.Entities.OccurrencesAgg.Models;
using Gama.Domain.Entities.OccurrencesAgg.Repositories;
using Gama.Domain.Interfaces.EventBus;
using Gama.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
            await CommitAsync();
            await PublishMessage(tObject);
        }

        public override async Task Patch(Occurrence tObject)
        {
            await PublishMessage(tObject);
            await base.Patch(tObject);
        }

        public async override Task<Occurrence?> FindOneAsync<TId>(TId id)
        {
            return await _context.Set<Occurrence>()
                            .Include(o => o.OccurrenceType)
                            .Include(o => o.OccurrenceUrgencyLevel)
                            .Include(o => o.Status)
                            .FirstOrDefaultAsync(o => o.Id == int.Parse(id.ToString()!));
        }

        public async Task<IEnumerable<Occurrence>> GetAsync(Expression<Func<Occurrence, bool>> search, int offset, int size)
        {
            return await FindAll()
                    .Include(o => o.OccurrenceType)
                    .Include(o => o.Status)
                    .Include(o => o.OccurrenceUrgencyLevel)
                    .OrderBy(x => x.CreatedAt)
                    .Where(search)
                    .Skip(offset)
                    .Take(size)
                    .ToListAsync();
        }

        internal async Task PublishMessage(Occurrence tObject)
        {
            var @event = tObject.Events.First();

            if (@event != null)
            {
                if (@event is CreatedOccurrenceEvent createdEvent)
                {
                    createdEvent.OccurrenceId = tObject.Id;
                }

                await _eventBusProducer.Publish(@event, "gama-api-occurrences.fifo");
            }
        }
    }
}
