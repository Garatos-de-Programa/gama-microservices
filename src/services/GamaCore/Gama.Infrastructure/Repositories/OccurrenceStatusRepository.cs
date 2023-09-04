using Gama.Domain.Entities.OccurrencesAgg.Models;
using Gama.Domain.Entities.OccurrencesAgg.Repositories;
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
