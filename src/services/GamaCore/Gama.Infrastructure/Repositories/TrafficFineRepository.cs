using Gama.Application.Contracts.Repositories;
using Gama.Application.Seedworks.Queries;
using Gama.Domain.Models.TrafficFines;
using Gama.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Gama.Infrastructure.Repositories
{
    internal class TrafficFineRepository : Repository<TrafficFine>, ITrafficFineRepository
    {
        public TrafficFineRepository(GamaCoreDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<TrafficFine?> FindOneAsync<TId>(TId id)
        {
            return await _context.Set<TrafficFine>()
                .Include(i => i.TrafficFineTrafficViolations!)
                    .ThenInclude(t => t.TrafficViolation)
                .FirstOrDefaultAsync(t => t.Id == int.Parse(id.ToString()!));
        }

        public override Task InsertAsync(TrafficFine trafficFine)
        {
            var trafficFineViolations = trafficFine.TrafficFineTrafficViolations!
                    .Select(v => v.TrafficViolation?.Id).ToList();


            var trafficViolation = _context.Set<TrafficViolation>()
                    .Where(t => trafficFineViolations.Contains(t.Id));

            trafficFine.TrafficFineTrafficViolations = trafficViolation.Select(v =>
                     new TrafficFineTrafficViolation() { TrafficFine = trafficFine, TrafficViolation = v }
                    ).ToArray();

            return base.InsertAsync(trafficFine);
        }

        public async Task<IEnumerable<TrafficFine>> GetAsync(Expression<Func<TrafficFine, bool>> dateSearchQuery, int offset, int size)
        {
            return await FindAll()
                    .OrderBy(x => x.CreatedAt)
                    .Where(dateSearchQuery)
                    .Skip(offset)
                    .Take(size)
                    .ToListAsync();
        }
    }
}
