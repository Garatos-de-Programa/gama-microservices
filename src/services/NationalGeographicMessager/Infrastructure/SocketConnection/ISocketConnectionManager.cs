using Microsoft.AspNetCore.SignalR;
using NationalGeographicMessager.Domain.GeolocationAggregated;

namespace NationalGeographicMessager.Infrastructure.SocketConnection
{
    public interface ISocketConnectionManager
    {
        void AddSocketConnection(string connectionId, Point location);
        void RemoveSocketConnection(string connectionId);
        IEnumerable<ISingleClientProxy> GetConnectionsInsideTheBoundary(Point boundary);
    }
}
