using Gama.Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace Gama.Domain.Entities.TrafficFinesAgg
{
    public interface ITrafficFineRepository : IRepository<TrafficFine>
    {
        Task<IEnumerable<TrafficFine>> GetAsync(Expression<Func<TrafficFine, bool>> dateSearchQuery, int offset, int size);
        Task<int> Count(Expression<Func<TrafficFine, bool>> dateSearchQuery);
    }
}
