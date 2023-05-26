using Gama.Application.DataContracts.Queries.Common;
using Gama.Domain.Models.TrafficFines;

namespace Gama.Application.Contracts.Repositories
{
    public interface ITrafficFineRepository : IRepository<TrafficFine>
    {
        Task<IEnumerable<TrafficFine>> GetAsync(DateSearchQuery dateSearchQuery, int offset, int size);
    }
}
