using NationalGeographicMessager.Domain.GeolocationAggregated;
using System.Collections.Concurrent;

namespace NationalGeographicMessager.Infrastructure.SocketConnection
{
    public class ThreadSafeSocketConnectionManager : ISocketConnectionManager
    {
        private readonly ConcurrentDictionary<string, Point> _connections = new();
        private readonly IGeoLocationCalculator _geoLocationCalculator;

        public ThreadSafeSocketConnectionManager(IGeoLocationCalculator geoLocationCalculator)
        {
            _geoLocationCalculator = geoLocationCalculator;
        }

        public void AddSocketConnection(string connectionId, Point location)
        {
            _connections.TryAdd(connectionId, location);
        }

        public IEnumerable<string> GetConnectionsInsideTheBoundary(Point boundary)
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
                    yield return connection.Key;
                }
            }
        }

        public void RemoveSocketConnection(string connectionId)
        {
            _connections.TryRemove(connectionId, out var _);
        }
    }
}
