using Gama.Application.Seedworks.Pagination;
using Gama.Application.Seedworks.Queries;
using Gama.Domain.Entities.TrafficFinesAgg;
using Gama.Domain.ValueTypes;
using Microsoft.AspNetCore.Http;

namespace Gama.Application.UseCases.TrafficFineAgg.Interfaces;

public interface ITrafficFineService
{
    Task<Result<TrafficFine>> GetAsync(int id);
    Task<Result<OffsetPage<TrafficFine>>> GetByDateSearchAsync(DateSearchQuery dateSearchQuery);
    Task<Result<TrafficFine>> CreateAsync(TrafficFine trafficFine, IFormFile infractionImage, CancellationToken cancellationToken);
    Task<Result<bool>> ComputeAsync(int id);
    Task<Result<bool>> DeleteAsync(int id);
}