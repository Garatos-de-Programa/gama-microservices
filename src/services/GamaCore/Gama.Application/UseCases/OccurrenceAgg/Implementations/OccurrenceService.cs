using Gama.Application.Seedworks.Queries;
using Gama.Application.UseCases.OccurrenceAgg.Interfaces;
using Gama.Application.UseCases.OccurrenceAgg.Responses;
using Gama.Application.UseCases.UserAgg.Interfaces;
using Gama.Domain.Entities.OccurrencesAgg;
using Gama.Domain.Entities.TrafficFinesAgg;
using Gama.Domain.Exceptions;
using Gama.Domain.Interfaces.FileManagement;
using Gama.Domain.ValueTypes;
using Microsoft.AspNetCore.Http;

namespace Gama.Application.UseCases.OccurrenceAgg.Implementations
{
    internal class OccurrenceService : IOccurrenceService
    {
        private readonly IOccurrenceRepository _occurrenceRepository;
        private readonly ICurrentUserAccessor _currentUserAccessor;
        private readonly IOccurrenceUrgencyLevelRepository _occurrenceUrgencyLevelRepository;
        private readonly IOccurrenceTypeRepository _occurrenceTypeRepository;
        private readonly IOccurrenceStatusRepository _occurrenceStatusRepository;
        private readonly IFileManager _fileManager;

        public OccurrenceService(
            IOccurrenceRepository occurrenceRepository,
            ICurrentUserAccessor currentUserAccessor,
            IOccurrenceUrgencyLevelRepository occurrenceUrgencyLevelRepository,
            IOccurrenceTypeRepository occurrenceTypeRepository,
            IOccurrenceStatusRepository occurrenceStatusRepository,
            IFileManager fileManager
            )
        {
            _occurrenceRepository = occurrenceRepository;
            _currentUserAccessor = currentUserAccessor;
            _occurrenceTypeRepository = occurrenceTypeRepository;
            _occurrenceStatusRepository = occurrenceStatusRepository;
            _occurrenceUrgencyLevelRepository = occurrenceUrgencyLevelRepository;
            _fileManager = fileManager;
        }

        public async Task<Result<Occurrence>> CreateAsync(
            Occurrence occurrence,
            IFormFile occurrenceImageFile,
            CancellationToken cancellationToken
            )
        {
            if (occurrence == null)
                return new Result<Occurrence>(new ValidationException(new ValidationError()
                {
                    PropertyName = "Occurrence",
                    ErrorMessage = "Você deve informar uma ocorrencia valida"
                }));

            var imageUrl = await _fileManager.UploadAsync(new FileObject(occurrenceImageFile), cancellationToken);

            var user = _currentUserAccessor.GetUser();
            occurrence.PrepareToInsert(user, imageUrl);

            await _occurrenceRepository.InsertAsync(occurrence);

            return occurrence;
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var occurrence = await _occurrenceRepository.FindOneAsync(id);

            if (occurrence == null)
                return new Result<bool>(new ValidationException(new ValidationError()
                {
                    PropertyName = "Ocurrence",
                    ErrorMessage = "Ocorrencia não encontrada"
                }));

            var user = _currentUserAccessor.GetUser();

            var result = occurrence.Delete(user);

            if (result.IsFaulted)
            {
                return result;
            }

            await _occurrenceRepository.Patch(occurrence);
            await _occurrenceRepository.CommitAsync();

            return true;
        }

        public async Task<Result<Occurrence>> GetAsync(int id)
        {
            var occurrence = await _occurrenceRepository.FindOneAsync(id);
            if (occurrence is null)
            {
                return new Result<Occurrence>(new ValidationException(new ValidationError()
                {
                    PropertyName = "occurrence",
                    ErrorMessage = "Ocorrencia não encontrada"
                }));
            }

            return occurrence;
        }

        public async Task<Result<OffsetPage<Occurrence>>> GetByDateSearchAsync(DateSearchQuery search)
        {
            var offsetPage = new OffsetPage<Occurrence>()
            {
                PageNumber = search.PageNumber,
                Size = search.Size
            };

            var occurrence = await _occurrenceRepository.GetAsync(t => 
                                t.CreatedAt >= search.CreatedSince.ToUniversalTime() &&
                                t.CreatedAt <= search.CreatedUntil.ToUniversalTime(), 
                                offsetPage.Offset, 
                                search.Size
                                );

            offsetPage.Results = occurrence;

            return offsetPage;
        }

        public Task<Result<OccurrencePropertiesResponse>> GetOccurrencePropertiesAsync()
        {
            return Task.FromResult(new Result<OccurrencePropertiesResponse>(new OccurrencePropertiesResponse()
            {
                Types = _occurrenceTypeRepository.FindAll().Select(t => new GetOccurrenceTypeResponse() { Id = t.Id, Name = t.Name }),
                Status = _occurrenceStatusRepository.FindAll().Select(s => new GetOccurrenceStatusResponse() { Id = s.Id, Name = s.Name }),
                UrgencyLevels = _occurrenceUrgencyLevelRepository.FindAll().Select(u => new GetOccurrenceUrgencyLevelResponse() { Id = u.Id, Name = u.Name }),
            }));
        }
    }
}
