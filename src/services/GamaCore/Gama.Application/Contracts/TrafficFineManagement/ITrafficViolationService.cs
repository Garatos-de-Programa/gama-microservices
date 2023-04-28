using Gama.Domain.Entities;
using Gama.Domain.ValueTypes;

namespace Gama.Application.Contracts.TrafficFineManagement;

public interface ITrafficViolationService
{
    Task<Result<TrafficViolation>> GetAsync(short id);
    Task<Result<IEnumerable<TrafficViolation>>> GetTrafficsViolationsAsync();
    Task<Result<TrafficViolation>> CreateAsync(TrafficViolation trafficViolation);
    Task<Result<TrafficViolation>> UpdateAsync(TrafficViolation trafficViolation);
    Task<Result<bool>> DeleteAsync(short id);
}