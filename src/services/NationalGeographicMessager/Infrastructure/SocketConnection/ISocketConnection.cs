using NationalGeographicMessager.Domain.GeolocationAggregated;

namespace NationalGeographicMessager.Infrastructure.SocketConnection
{
    public interface ISocketConnection
    {
        IDictionary<string, Point> Connections { get; }

        Task WriteAsync(IEnumerable<string> connectionId, byte[] data);
    }
}
