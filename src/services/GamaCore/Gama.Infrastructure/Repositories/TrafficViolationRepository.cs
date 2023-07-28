using Gama.Domain.Entities.TrafficFinesAgg;
using Gama.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Gama.Infrastructure.Repositories
{
    internal class TrafficViolationRepository : Repository<TrafficViolation>, ITrafficViolationRepository
    {
        public TrafficViolationRepository(GamaCoreDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<TrafficViolation?> GetByCode(string code)
        {
            return await _context.Set<TrafficViolation>().FirstOrDefaultAsync(v => v.Code == code);
        }
    }
}
