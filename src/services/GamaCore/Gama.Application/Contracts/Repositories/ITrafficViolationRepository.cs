using Gama.Domain.Entities;

namespace Gama.Application.Contracts.Repositories
{
    public interface ITrafficViolationRepository : IRepository<TrafficViolation>
    {
        Task<TrafficViolation?> GetByCode(string code);
    }
}
