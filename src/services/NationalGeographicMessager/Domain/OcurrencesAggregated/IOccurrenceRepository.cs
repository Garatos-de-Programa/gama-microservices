
using NationalGeographicMessager.Domain.GeolocationAggregated;

namespace NationalGeographicMessager.Domain.OcurrencesAggregated
{
    public interface IOccurrenceRepository
    {
        Task<OccurrenceEventMessage[]> GetAsync(Point occurrenceLocation);
    }
}
