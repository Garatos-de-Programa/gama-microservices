using Gama.Application.Contracts.Repositories;
using Gama.Application.Contracts.TrafficFineManagement;
using Gama.Application.Contracts.UserManagement;
using Gama.Application.DataContracts.Queries.Common;
using Gama.Application.Seedworks.Pagination;
using Gama.Domain.Constants;
using Gama.Domain.Entities;
using Gama.Domain.Exceptions;
using Gama.Domain.ValueTypes;

namespace Gama.Application.UseCases.TrafficFineManagement
{
    internal class TrafficFineService : ITrafficFineService
    {
        private readonly ITrafficFineRepository _trafficFineRepository;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public TrafficFineService(
            ITrafficFineRepository trafficFineRepository,
            ICurrentUserAccessor currentUserAccessor
            )
        {
            _currentUserAccessor = currentUserAccessor;
            _trafficFineRepository = trafficFineRepository;
        }

        public async Task<Result<bool>> ComputeAsync(int id)
        {
            var trafficFine = await _trafficFineRepository.FindOneAsync(id).ConfigureAwait(false);

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

            _trafficFineRepository.Patch(trafficFine);
            await _trafficFineRepository.CommitAsync().ConfigureAwait(false);

            return true;
        }

        public async Task<Result<TrafficFine>> CreateAsync(TrafficFine trafficFine)
        {
            if (trafficFine == null)
                return new Result<TrafficFine>(new ValidationException(new ValidationError()
                {
                    PropertyName = "TrafficFine",
                    ErrorMessage = "Você deve informar uma multa valida"
                }));

            trafficFine.UserId = _currentUserAccessor.GetUserId();
            trafficFine.PrepareToInsert();

            await _trafficFineRepository.InsertAsync(trafficFine).ConfigureAwait(false);
            await _trafficFineRepository.CommitAsync().ConfigureAwait(false);

            return trafficFine;
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var trafficFine = await _trafficFineRepository.FindOneAsync(id).ConfigureAwait(false);

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

            _trafficFineRepository.Patch(trafficFine);
            await _trafficFineRepository.CommitAsync().ConfigureAwait(false);

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

            if(notAllowed)
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

            var trafficFine = await _trafficFineRepository.GetAsync(dateSearchQuery, search.Offset, search.Size);

            search.Results = trafficFine;

            return search;
        }
    }
}
