using Gama.Domain.Entities.OccurrencesAgg.Models;
using Gama.Domain.Entities.OccurrencesAgg.Repositories;
using Gama.Infrastructure.Persistence;

namespace Gama.Infrastructure.Repositories
{
    internal class OccurrenceUrgencyLevelRepository : Repository<OccurrenceUrgencyLevel>, IOccurrenceUrgencyLevelRepository
    {
        public OccurrenceUrgencyLevelRepository(GamaCoreDbContext dbContext) : base(dbContext)
        {
        }
    }
}
