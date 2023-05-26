using Gama.Application.Contracts.Repositories;
using Gama.Domain.Models.Occurrences;
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
