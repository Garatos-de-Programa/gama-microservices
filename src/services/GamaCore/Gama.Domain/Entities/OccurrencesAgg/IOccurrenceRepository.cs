using Gama.Domain.Models.Occurrences;
using System.Linq.Expressions;

namespace Gama.Application.Contracts.Repositories
{
    public interface IOccurrenceRepository : IRepository<Occurrence>
    {
        Task<IEnumerable<Occurrence>> GetAsync(Expression<Func<Occurrence, bool>> search, int offset, int size);
    }
}
