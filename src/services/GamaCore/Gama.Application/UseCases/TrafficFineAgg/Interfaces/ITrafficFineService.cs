using Gama.Application.Contracts.Repositories;
using Gama.Application.Seedworks.Pagination;
using Gama.Application.Seedworks.Queries;
using Gama.Domain.Models.TrafficFines;
using Gama.Domain.ValueTypes;

namespace Gama.Application.UseCases.TrafficFineAgg.Interfaces;

public interface ITrafficFineService
{
    Task<Result<TrafficFine>> GetAsync(int id);
    Task<Result<OffsetPage<TrafficFine>>> GetByDateSearchAsync(DateSearchQuery dateSearchQuery);
    Task<Result<TrafficFine>> CreateAsync(TrafficFine trafficFine);
    Task<Result<bool>> ComputeAsync(int id);
    Task<Result<bool>> DeleteAsync(int id);
}