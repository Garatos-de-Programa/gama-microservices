using Gama.Domain.Entities.OccurrencesAgg;
using Gama.Infrastructure.Persistence;

namespace Gama.Infrastructure.Repositories
{
    internal class OccurrenceStatusRepository : Repository<OccurrenceStatus>, IOccurrenceStatusRepository
    {
        public OccurrenceStatusRepository(GamaCoreDbContext dbContext) : base(dbContext)
        {
        }
    }
}
