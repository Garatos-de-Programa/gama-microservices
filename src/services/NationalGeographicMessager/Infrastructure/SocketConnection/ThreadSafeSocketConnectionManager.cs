using Microsoft.AspNetCore.SignalR;
using NationalGeographicMessager.Domain.GeolocationAggregated;
using System.Collections.Concurrent;

namespace NationalGeographicMessager.Infrastructure.SocketConnection
{
    internal class ThreadSafeSocketConnectionManager : ISocketConnectionManager
    {
        private readonly ConcurrentDictionary<string, Point> _connections = new();
        private readonly IGeoLocationCalculator _geoLocationCalculator;
        private readonly IHubContext<SignableOccurrenceSocketConnection> _hubContext;

        public ThreadSafeSocketConnectionManager(
            IGeoLocationCalculator geoLocationCalculator,
            IHubContext<SignableOccurrenceSocketConnection> hubContext
            )
        {
            _geoLocationCalculator = geoLocationCalculator;
            _hubContext = hubContext;
        }

        public void AddSocketConnection(string connectionId, Point location)
        {
            _connections.TryAdd(connectionId, location);
        }

        public IEnumerable<ISingleClientProxy> GetConnectionsInsideTheBoundary(Point boundary)
        {
            foreach (var connection in _connections)
            {
                var point = connection.Value;
                var isInsideRadius = _geoLocationCalculator.IsInsideRadius(
                        point.Latitude,
                        point.Longitude,
                        boundary.Latitude,
                        boundary.Longitude,
                        point.Radius
                    );

                if (isInsideRadius)
                {
                    yield return _hubContext.Clients.Client(connection.Key);
                }
            }
        }

        public void RemoveSocketConnection(string connectionId)
        {
            _connections.TryRemove(connectionId, out var _);
        }
    }
}
