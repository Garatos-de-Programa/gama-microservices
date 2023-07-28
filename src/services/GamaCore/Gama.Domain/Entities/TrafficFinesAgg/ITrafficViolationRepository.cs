using Gama.Domain.Interfaces.Repositories;

namespace Gama.Domain.Entities.TrafficFinesAgg
{
    public interface ITrafficViolationRepository : IRepository<TrafficViolation>
    {
        Task<TrafficViolation?> GetByCode(string code);
    }
}
