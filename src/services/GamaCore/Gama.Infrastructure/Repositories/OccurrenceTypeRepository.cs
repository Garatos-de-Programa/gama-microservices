using Gama.Application.Contracts.Repositories;
using Gama.Domain.Entities;
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
