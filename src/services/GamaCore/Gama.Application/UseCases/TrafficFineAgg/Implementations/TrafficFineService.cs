using Gama.Application.Seedworks.Queries;
using Gama.Application.UseCases.TrafficFineAgg.Interfaces;
using Gama.Application.UseCases.TrafficFineAgg.Queries;
using Gama.Application.UseCases.UserAgg.Interfaces;
using Gama.Domain.Entities.TrafficFinesAgg;
using Gama.Domain.Entities.UsersAgg;
using Gama.Domain.Exceptions;
using Gama.Domain.Interfaces.FileManagement;
using Gama.Domain.ValueTypes;
using Microsoft.AspNetCore.Http;

namespace Gama.Application.UseCases.TrafficFineAgg.Implementations
{
    internal class TrafficFineService : ITrafficFineService
    {
        private readonly ITrafficFineRepository _trafficFineRepository;
        private readonly ICurrentUserAccessor _currentUserAccessor;
        private readonly IFileManager _fileManager;

        public TrafficFineService(
            ITrafficFineRepository trafficFineRepository,
            ICurrentUserAccessor currentUserAccessor,
            IFileManager fileManager
            )
        {
            _currentUserAccessor = currentUserAccessor;
            _trafficFineRepository = trafficFineRepository;
            _fileManager = fileManager;
        }

        public async Task<Result<bool>> ComputeAsync(int id)
        {
            var trafficFine = await _trafficFineRepository.FindOneAsync(id);

            if (trafficFine == null)
                return new Result<bool>(new ValidationException(new ValidationError()
                {
                    PropertyName = "TrafficFine",
                    ErrorMessage = "Multa não encontrada"
                }));

            var user = _currentUserAccessor.GetUser();

            var result = trafficFine.Compute(user);

            if (result.IsFaulted)
            {
                return result;
            }

            await _trafficFineRepository.Patch(trafficFine);
            await _trafficFineRepository.CommitAsync();

            return true;
        }

        public async Task<Result<TrafficFine>> CreateAsync(
            TrafficFine trafficFine, 
            IFormFile infractionImage, 
            CancellationToken cancellationToken
            )
        {
            if (trafficFine == null)
                return new Result<TrafficFine>(new ValidationException(new ValidationError()
                {
                    PropertyName = "TrafficFine",
                    ErrorMessage = "Você deve informar uma multa valida"
                }));

            var imageUrl = await _fileManager.UploadAsync(new FileObject(infractionImage), cancellationToken);

            trafficFine.PrepareToInsert(imageUrl, _currentUserAccessor.GetUserId());

            await _trafficFineRepository.InsertAsync(trafficFine);
            await _trafficFineRepository.CommitAsync();

            return trafficFine;
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var trafficFine = await _trafficFineRepository.FindOneAsync(id);

            if (trafficFine == null)
                return new Result<bool>(new ValidationException(new ValidationError()
                {
                    PropertyName = "TrafficFine",
                    ErrorMessage = "Multa não encontrada"
                }));

            var user = _currentUserAccessor.GetUser();

            var result = trafficFine.Delete(user);

            if (result.IsFaulted)
            {
                return result;
            }

            await _trafficFineRepository.Patch(trafficFine);
            await _trafficFineRepository.CommitAsync();

            return true;
        }

        public async Task<Result<TrafficFine>> GetAsync(int id)
        {
            var trafficFine = await _trafficFineRepository.FindOneAsync(id);
            if (trafficFine is null)
            {
                return new Result<TrafficFine>(new ValidationException(new ValidationError()
                {
                    PropertyName = "TrafficFine",
                    ErrorMessage = "Multa não encontrada"
                }));
            }

            var user = _currentUserAccessor.GetUser();

            var notAllowed = !trafficFine.IsUserAllowedToHandle(user);

            if (notAllowed)
            {
                return new Result<TrafficFine>(new ValidationException(new ValidationError()
                {
                    PropertyName = "TrafficFine",
                    ErrorMessage = "Multa não encontrada"
                }));
            }

            return trafficFine;
        }

        public async Task<Result<OffsetPage<TrafficFine>>> GetByDateSearchAsync(DateSearchQuery dateSearchQuery)
        {
            var search = new OffsetPage<TrafficFine>()
            {
                PageNumber = dateSearchQuery.PageNumber,
                Size = dateSearchQuery.Size
            };

            var user = _currentUserAccessor.GetUser();
            var isCop = user.IsRole(RolesName.Cop);
            var query = new DatesearchTrafficFineQuery(isCop, dateSearchQuery, user.Id);
            var trafficFine = await _trafficFineRepository.GetAsync(
                t => query.Filter(t), 
                search.Offset, 
                search.Size
            );

            search.Results = trafficFine;

            return search;
        }
    }
}
