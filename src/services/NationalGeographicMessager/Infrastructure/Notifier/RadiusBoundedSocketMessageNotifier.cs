using NationalGeographicMessager.Domain.GeolocationAggregated;
using NationalGeographicMessager.Domain.NotificationMessageAggregated;
using NationalGeographicMessager.Infrastructure.SocketConnection;

namespace NationalGeographicMessager.Infrastructure.Notifier
{
    public class RadiusBoundedSocketMessageNotifier : IMessageNotifier
    {
        private readonly ISocketConnection _sockerConnection;
        private readonly IGeoLocationCalculator _geoLocationCalculator;

        public RadiusBoundedSocketMessageNotifier(
            ISocketConnection socketConnection,
            IGeoLocationCalculator geoLocationCalculator
            )
        {
            _sockerConnection = socketConnection;
            _geoLocationCalculator = geoLocationCalculator;
        }

        public Task NotifyAsync(IMessage message)
        {
            var connectionToNotify = GetConnectionsIdsInsideTheBound(message.Point);
            return _sockerConnection.WriteAsync(connectionToNotify, message.GetBytes());
        }

        internal IEnumerable<string> GetConnectionsIdsInsideTheBound(Point messageLocation)
        {
            foreach ( var connection in _sockerConnection.Connections )
            {
                var point = connection.Value;
                var isInsideRadius = _geoLocationCalculator.IsInsideRadius(
                        point.Latitude,
                        point.Longitude,
                        messageLocation.Latitude,
                        messageLocation.Longitude,
                        point.Radius
                    );

                if (isInsideRadius)
                {
                    yield return connection.Key;
                }
            }
        }
    }
}
