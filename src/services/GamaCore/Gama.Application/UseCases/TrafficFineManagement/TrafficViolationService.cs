﻿using Gama.Application.Contracts.Repositories;
using Gama.Application.Contracts.TrafficFineManagement;
using Gama.Domain.Exceptions;
using Gama.Domain.Models.TrafficFines;
using Gama.Domain.ValueTypes;

namespace Gama.Application.UseCases.TrafficFineManagement
{
    internal class TrafficViolationService : ITrafficViolationService
    {
        private readonly ITrafficViolationRepository _trafficViolationRepository;

        public TrafficViolationService(ITrafficViolationRepository trafficViolationRepository)
        {
            _trafficViolationRepository = trafficViolationRepository;
        }

        public async Task<Result<TrafficViolation>> CreateAsync(TrafficViolation trafficViolation)
        {
            if (trafficViolation == null)
                return new Result<TrafficViolation>(new ValidationException(new ValidationError()
                {
                    PropertyName = "TrafficViolation",
                    ErrorMessage = "Você deve informar uma infração valida"
                }));

            var violation = await _trafficViolationRepository.GetByCode(trafficViolation?.Code ?? string.Empty)
                    .ConfigureAwait(false);

            if (violation is not null)
            {
                return new Result<TrafficViolation>(new ValidationException(new ValidationError()
                {
                    PropertyName = "TrafficViolation",
                    ErrorMessage = "Já existe uma infração com esse código"
                }));
            }

            trafficViolation.PrepareToInsert();

            await _trafficViolationRepository.InsertAsync(trafficViolation).ConfigureAwait(false);
            await _trafficViolationRepository.CommitAsync().ConfigureAwait(false);

            return trafficViolation;
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var trafficViolation = await _trafficViolationRepository.FindOneAsync(id).ConfigureAwait(false);

            if (trafficViolation == null)
                return new Result<bool>(new ValidationException(new ValidationError()
                {
                    PropertyName = "TrafficViolation",
                    ErrorMessage = "Infração não encontrada"
                }));

            trafficViolation.Delete();

            _trafficViolationRepository.Patch(trafficViolation);
            await _trafficViolationRepository.CommitAsync().ConfigureAwait(false);

            return true;
        }

        public async Task<Result<TrafficViolation>> GetAsync(int id)
        {
            var trafficViolation = await _trafficViolationRepository.FindOneAsync(id);
            if (trafficViolation is null)
            {
                return new Result<TrafficViolation>(new ValidationException(new ValidationError()
                {
                    PropertyName = "TrafficViolation",
                    ErrorMessage = "Infração não encontrada"
                }));
            }

            return trafficViolation;
        }

        public Task<Result<IEnumerable<TrafficViolation>>> GetTrafficsViolationsAsync()
        {
            return Task.FromResult(new Result<IEnumerable<TrafficViolation>>(_trafficViolationRepository.FindAll().Where(v => v.Active == true).ToList()));
        }

        public async Task<Result<TrafficViolation>> UpdateAsync(TrafficViolation trafficViolation)
        {
            if (trafficViolation == null)
                return new Result<TrafficViolation>(new ValidationException(new ValidationError()
                {
                    PropertyName = "TrafficViolation",
                    ErrorMessage = "Você deve informar uma infração valida"
                }));

            var violation = await _trafficViolationRepository.FindOneAsync(trafficViolation.Id)
                    .ConfigureAwait(false);

            if (violation is null)
            {
                return new Result<TrafficViolation>(new ValidationException(new ValidationError()
                {
                    PropertyName = "TrafficViolation",
                    ErrorMessage = "Infração não encontrada"
                }));
            }

            violation?.Update(trafficViolation);

            _trafficViolationRepository.Patch(violation);
            await _trafficViolationRepository.CommitAsync().ConfigureAwait(false);

            return trafficViolation;
        }
    }
}
