
using NationalGeographicMessager.Domain.GeolocationAggregated;

namespace NationalGeographicMessager.Domain.OcurrencesAggregated
{
    public interface IOccurrenceRepository
    {
        Task<IEnumerable<OccurrenceEventMessage>> GetAsync(Point occurrenceLocation);
    }
}
