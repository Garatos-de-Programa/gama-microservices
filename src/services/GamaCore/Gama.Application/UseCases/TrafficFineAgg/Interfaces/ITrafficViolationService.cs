using Gama.Domain.Entities.TrafficFinesAgg;
using Gama.Domain.ValueTypes;

namespace Gama.Application.UseCases.TrafficFineAgg.Interfaces;

public interface ITrafficViolationService
{
    Task<Result<TrafficViolation>> GetAsync(int id);
    Task<Result<IEnumerable<TrafficViolation>>> GetTrafficsViolationsAsync();
    Task<Result<TrafficViolation>> CreateAsync(TrafficViolation trafficViolation);
    Task<Result<TrafficViolation>> UpdateAsync(TrafficViolation trafficViolation);
    Task<Result<bool>> DeleteAsync(int id);
}