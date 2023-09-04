using Gama.Domain.Entities.OccurrencesAgg.Models;
using Gama.Domain.Entities.OccurrencesAgg.Repositories;
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
