using Gama.Domain.Models.TrafficFines;

namespace Gama.Application.Contracts.Repositories
{
    public interface ITrafficViolationRepository : IRepository<TrafficViolation>
    {
        Task<TrafficViolation?> GetByCode(string code);
    }
}
