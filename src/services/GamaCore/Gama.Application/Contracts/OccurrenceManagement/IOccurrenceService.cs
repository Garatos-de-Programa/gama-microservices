using Gama.Application.DataContracts.Queries.Common;
using Gama.Application.Seedworks.Pagination;
using Gama.Domain.Entities;
using Gama.Domain.ValueTypes;

namespace Gama.Application.Contracts.OccurrenceManagement
{
    public interface IOccurrenceService
    {
        Task<Result<Occurrence>> GetAsync(int id);
        Task<Result<Occurrence>> CreateAsync(Occurrence occurrence);
        Task<Result<OffsetPage<Occurrence>>> GetByDateSearchAsync(DateSearchQuery search);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
