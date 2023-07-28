using Gama.Domain.Entities.OccurrencesAgg;
using Gama.Infrastructure.Persistence;

namespace Gama.Infrastructure.Repositories
{
    internal class OccurrenceTypeRepository : Repository<OccurrenceType>, IOccurrenceTypeRepository
    {
        public OccurrenceTypeRepository(GamaCoreDbContext dbContext) : base(dbContext)
        {
        }
    }
}
