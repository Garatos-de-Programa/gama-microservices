using Gama.Application.UseCases.TrafficFineAgg.Interfaces;
using Gama.Domain.Entities.TrafficFinesAgg;
using Gama.Domain.Exceptions;
using Gama.Domain.ValueTypes;

namespace Gama.Application.UseCases.TrafficFineAgg.Implementations
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
                    ;

            if (violation is not null)
            {
                return new Result<TrafficViolation>(new ValidationException(new ValidationError()
                {
                    PropertyName = "TrafficViolation",
                    ErrorMessage = "Já existe uma infração com esse código"
                }));
            }

            trafficViolation!.PrepareToInsert();

            await _trafficViolationRepository.InsertAsync(trafficViolation);
            await _trafficViolationRepository.CommitAsync();

            return trafficViolation;
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var trafficViolation = await _trafficViolationRepository.FindOneAsync(id);

            if (trafficViolation == null)
                return new Result<bool>(new ValidationException(new ValidationError()
                {
                    PropertyName = "TrafficViolation",
                    ErrorMessage = "Infração não encontrada"
                }));

            trafficViolation.Delete();

            await _trafficViolationRepository.Patch(trafficViolation);
            await _trafficViolationRepository.CommitAsync();

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
                    ;

            if (violation is null)
            {
                return new Result<TrafficViolation>(new ValidationException(new ValidationError()
                {
                    PropertyName = "TrafficViolation",
                    ErrorMessage = "Infração não encontrada"
                }));
            }

            violation?.Update(trafficViolation);

            await _trafficViolationRepository.Patch(violation!);
            await _trafficViolationRepository.CommitAsync();

            return trafficViolation;
        }
    }
}
