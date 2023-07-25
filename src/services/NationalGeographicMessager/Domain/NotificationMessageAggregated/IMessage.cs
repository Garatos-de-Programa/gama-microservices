
using NationalGeographicMessager.Domain.GeolocationAggregated;

namespace NationalGeographicMessager.Domain.NotificationMessageAggregated
{
    public interface IMessage
    {
        Point Point { get; }

        byte[] GetBytes();
    }
}
