using Gama.Application.Contracts.Repositories;
using Gama.Domain.Entities;
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
