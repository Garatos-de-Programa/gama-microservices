using System.Linq.Expressions;
using Gama.Domain.Interfaces.Repositories;

namespace Gama.Domain.Entities.TrafficFinesAgg
{
    public interface ITrafficFineRepository : IRepository<TrafficFine>
    {
        Task<IEnumerable<TrafficFine>> GetAsync(Expression<Func<TrafficFine, bool>> dateSearchQuery, int offset, int size);
    }
}
