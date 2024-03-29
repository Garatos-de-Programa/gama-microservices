﻿using Gama.Application.UseCases.TrafficFineAgg.Interfaces;
using Gama.Application.UseCases.TrafficFineAgg.Queries;
using Gama.Application.UseCases.UserAgg.Interfaces;
using Gama.Domain.Entities.TrafficFinesAgg;
using Gama.Domain.Entities.UsersAgg;
using Gama.Domain.Exceptions;
using Gama.Domain.ValueTypes;

namespace Gama.Application.UseCases.TrafficFineAgg.Implementations
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
            var trafficFine = await _trafficFineRepository.FindOneAsync(id);

            if (trafficFine == null)
                return new Result<bool>(new ValidationException(new ValidationError()
                {
                    PropertyName = "TrafficFine",
                    ErrorMessage = "Multa não encontrada"
                }));

            var result = trafficFine.Compute();

            if (result.IsFaulted)
            {
                return result;
            }

            await _trafficFineRepository.Patch(trafficFine);
            await _trafficFineRepository.CommitAsync();

            return true;
        }

        public async Task<Result<TrafficFine>> CreateAsync(
            TrafficFine trafficFine 
            )
        {
            if (trafficFine == null)
                return new Result<TrafficFine>(new ValidationException(new ValidationError()
                {
                    PropertyName = "TrafficFine",
                    ErrorMessage = "Você deve informar uma multa valida"
                }));

            trafficFine.PrepareToInsert(_currentUserAccessor.GetUserId());

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

        public async Task<Result<OffsetPage<TrafficFine>>> GetByDateSearchAsync(TrafficFineListingQuery dateSearchQuery)
        {
            var search = new OffsetPage<TrafficFine>()
            {
                PageNumber = dateSearchQuery.PageNumber,
                Size = dateSearchQuery.Size
            };

            var user = _currentUserAccessor.GetUser();
            var isCop = user.IsRole(RolesName.Cop);
            var query = dateSearchQuery.ToExpression(user.Id, isCop);

            var trafficFines = await _trafficFineRepository.GetAsync(query, 
                search.Offset, 
                search.Size
            );
            var trafficFineCount = await _trafficFineRepository.Count(query);

            search.Results = trafficFines;
            search.Count = trafficFineCount;

            return search;
        }
    }
}
