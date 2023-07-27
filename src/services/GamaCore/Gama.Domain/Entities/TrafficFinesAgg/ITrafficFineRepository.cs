using Gama.Domain.Models.TrafficFines;
using System.Linq.Expressions;

namespace Gama.Application.Contracts.Repositories
{
    public interface ITrafficFineRepository : IRepository<TrafficFine>
    {
        Task<IEnumerable<TrafficFine>> GetAsync(Expression<Func<TrafficFine, bool>> dateSearchQuery, int offset, int size);
    }
}
