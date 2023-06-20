using Gama.Domain.Models.TrafficFines;
using Gama.Domain.ValueTypes;
using Microsoft.AspNetCore.Http;

namespace Gama.Application.Contracts.TrafficFineManagement
{
    public interface ITrafficFineFileService
    {
        Task<Result<TrafficFineFile>> UploadAsync(IFormFile trafficFineFile);
        Task<Stream?> GetAsync(int fileId);
    }
}
