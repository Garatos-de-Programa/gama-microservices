using Gama.Application.DataContracts.Queries.Common;
using Gama.Domain.Entities;
using Gama.Domain.ValueTypes;

namespace Gama.Application.Contracts.TrafficFineManagement;

public interface ITrafficFineService
{
    Task<Result<TrafficFine>> GetAsync(long id);
    Task<Result<IEnumerable<TrafficFine>>> GetByDateSearchAsync(DateSearchQuery dateSearchQuery);
    Task<Result<TrafficFine>> CreateAsync(TrafficFine trafficFine);
    Task<Result<TrafficFine>> UpdateAsync(TrafficFine trafficFine);
    Task<Result<bool>> DeleteAsync(long id);
}