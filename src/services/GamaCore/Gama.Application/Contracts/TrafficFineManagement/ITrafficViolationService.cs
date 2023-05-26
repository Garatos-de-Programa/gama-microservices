using Gama.Domain.Models.TrafficFines;
using Gama.Domain.ValueTypes;

namespace Gama.Application.Contracts.TrafficFineManagement;

public interface ITrafficViolationService
{
    Task<Result<TrafficViolation>> GetAsync(int id);
    Task<Result<IEnumerable<TrafficViolation>>> GetTrafficsViolationsAsync();
    Task<Result<TrafficViolation>> CreateAsync(TrafficViolation trafficViolation);
    Task<Result<TrafficViolation>> UpdateAsync(TrafficViolation trafficViolation);
    Task<Result<bool>> DeleteAsync(int id);
}