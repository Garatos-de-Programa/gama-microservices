using Gama.Application.UseCases.TrafficFineAgg.Interfaces;
using Gama.Domain.Entities.TrafficFinesAgg;
using Gama.Domain.Interfaces.FileManagement;
using Gama.Domain.ValueTypes;
using Microsoft.AspNetCore.Http;

namespace Gama.Application.UseCases.TrafficFineAgg.Implementations
{
    internal class TrafficFineFileService : ITrafficFineFileService
    {
        private readonly IFileManager _fileManager;
        private readonly ITrafficFineFileRepository _trafficFineFileRepository;

        public TrafficFineFileService(
            IFileManager fileManager
            )
        {
            _fileManager = fileManager;
        }

        public async Task<Stream?> GetAsync(int fileId)
        {
            var trafficFineFile = await _trafficFineFileRepository.FindOneAsync(fileId);

            if (trafficFineFile == null)
            {
                return null;
            }

            return await _fileManager.GetFileAsync(trafficFineFile.Path);
        }

        public async Task<Result<TrafficFineFile>> UploadAsync(IFormFile trafficFineFile)
        {
            var filePath = await _fileManager.UploadAsync(trafficFineFile.OpenReadStream());
            var trafficFile = new TrafficFineFile()
            {
                Path = filePath,
            };

            await _trafficFineFileRepository.InsertAsync(trafficFile).ConfigureAwait(false);

            return trafficFile;
        }
    }
}
