using Gama.Application.Contracts.OccurrenceManagement;
using Gama.Application.Contracts.Repositories;
using Gama.Application.Contracts.UserManagement;
using Gama.Application.DataContracts.Queries.Common;
using Gama.Application.DataContracts.Responses.OccurrenceManagement;
using Gama.Application.Seedworks.Pagination;
using Gama.Domain.Exceptions;
using Gama.Domain.Models.Occurrences;
using Gama.Domain.ValueTypes;

namespace Gama.Application.UseCases.OccurrenceManagement
{
    internal class OccurrenceService : IOccurrenceService
    {
        private readonly IOccurrenceRepository _occurrenceRepository;
        private readonly ICurrentUserAccessor _currentUserAccessor;
        private readonly IOccurrenceUrgencyLevelRepository _occurrenceUrgencyLevelRepository;
        private readonly IOccurrenceTypeRepository _occurrenceTypeRepository;
        private readonly IOccurrenceStatusRepository _occurrenceStatusRepository;

        public OccurrenceService(
            IOccurrenceRepository occurrenceRepository, 
            ICurrentUserAccessor currentUserAccessor,
            IOccurrenceUrgencyLevelRepository occurrenceUrgencyLevelRepository,
            IOccurrenceTypeRepository occurrenceTypeRepository,
            IOccurrenceStatusRepository occurrenceStatusRepository
            )
        {
            _occurrenceRepository = occurrenceRepository;
            _currentUserAccessor = currentUserAccessor;
            _occurrenceTypeRepository = occurrenceTypeRepository;
            _occurrenceStatusRepository = occurrenceStatusRepository;
            _occurrenceUrgencyLevelRepository = occurrenceUrgencyLevelRepository;
        }

        public async Task<Result<Occurrence>> CreateAsync(Occurrence occurrence)
        {
            if (occurrence == null)
                return new Result<Occurrence>(new ValidationException(new ValidationError()
                {
                    PropertyName = "Occurrence",
                    ErrorMessage = "Você deve informar uma ocorrencia valida"
                }));

            occurrence.UserId = _currentUserAccessor.GetUserId();
            occurrence.PrepareToInsert();

            await _occurrenceRepository.InsertAsync(occurrence).ConfigureAwait(false);
            await _occurrenceRepository.CommitAsync().ConfigureAwait(false);

            return occurrence;
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var occurrence = await _occurrenceRepository.FindOneAsync(id).ConfigureAwait(false);

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

            _occurrenceRepository.Patch(occurrence);
            await _occurrenceRepository.CommitAsync().ConfigureAwait(false);

            return true;
        }

        public async Task<Result<Occurrence>> GetAsync(int id)
        {
            var occurrence = await _occurrenceRepository.FindOneAsync(id).ConfigureAwait(false);
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

            var occurrence = await _occurrenceRepository.GetAsync(search, offsetPage.Offset, search.Size).ConfigureAwait(false);

            offsetPage.Results = occurrence;

            return offsetPage;
        }

        public Task<Result<OccurrencePropertiesResponse>> GetOccurrencePropertiesAsync()
        {
            return Task.FromResult(new Result<OccurrencePropertiesResponse>(new OccurrencePropertiesResponse()
            {
                Types = _occurrenceTypeRepository.FindAll().Select(t => new GetOccurrenceTypeResponse() { Id = t.Id, Name = t.Name }),
                Status = _occurrenceStatusRepository.FindAll().Select(s => new GetOccurrenceStatusResponse() { Id = s.Id, Name = s.Name }),
                UrgencyLevels = _occurrenceUrgencyLevelRepository.FindAll().Select(u =>  new GetOccurrenceUrgencyLevelResponse() { Id = u.Id, Name = u.Name }),
            }));
        }
    }
}
