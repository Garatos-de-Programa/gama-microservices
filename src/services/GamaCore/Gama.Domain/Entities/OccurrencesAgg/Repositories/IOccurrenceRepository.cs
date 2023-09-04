using System.Linq.Expressions;
using Gama.Domain.Entities.OccurrencesAgg.Models;
using Gama.Domain.Interfaces.Repositories;

namespace Gama.Domain.Entities.OccurrencesAgg.Repositories
{
    public interface IOccurrenceRepository : IRepository<Occurrence>
    {
        Task<IEnumerable<Occurrence>> GetAsync(Expression<Func<Occurrence, bool>> search, int offset, int size);
    }
}
