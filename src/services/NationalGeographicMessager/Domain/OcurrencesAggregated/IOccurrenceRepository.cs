
using NationalGeographicMessager.Domain.GeolocationAggregated;
using NationalGeographicMessager.Domain.NotificationMessageAggregated;

namespace NationalGeographicMessager.Domain.OcurrencesAggregated
{
    public interface IOccurrenceRepository
    {
        Task<IEnumerable<IncidentMessage>> GetAsync(Point occurrenceLocation);
    }
}
