using Gama.Domain.Entities.TrafficFinesAgg;
using Gama.Domain.ValueTypes;
using Microsoft.AspNetCore.Http;

namespace Gama.Application.UseCases.TrafficFineAgg.Interfaces
{
    public interface ITrafficFineFileService
    {
        Task<Result<TrafficFineFile>> UploadAsync(IFormFile trafficFineFile);
        Task<Stream?> GetAsync(int fileId);
    }
}
