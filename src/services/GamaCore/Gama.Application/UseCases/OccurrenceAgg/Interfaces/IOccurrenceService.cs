using Gama.Application.Seedworks.Pagination;
using Gama.Application.Seedworks.Queries;
using Gama.Application.UseCases.OccurrenceAgg.Responses;
using Gama.Domain.Entities.OccurrencesAgg;
using Gama.Domain.ValueTypes;

namespace Gama.Application.UseCases.OccurrenceAgg.Interfaces
{
    public interface IOccurrenceService
    {
        Task<Result<Occurrence>> GetAsync(int id);
        Task<Result<Occurrence>> CreateAsync(Occurrence occurrence);
        Task<Result<OffsetPage<Occurrence>>> GetByDateSearchAsync(DateSearchQuery search);
        Task<Result<bool>> DeleteAsync(int id);
        Task<Result<OccurrencePropertiesResponse>> GetOccurrencePropertiesAsync();
    }
}
