using NationalGeographicMessager.Domain.NotificationMessageAggregated;
using NationalGeographicMessager.Infrastructure.SocketConnection;

namespace NationalGeographicMessager.Infrastructure.Notifier
{
    internal class RadiusBoundedSocketMessageNotifier : IMessageNotifier
    {
        private readonly ISocketConnection _sockerConnection;
        private readonly ISocketConnectionManager _socketConnectionManager;
        

        public RadiusBoundedSocketMessageNotifier(
            ISocketConnection socketConnection,
            ISocketConnectionManager socketConnectionManager
            )
        {
            _sockerConnection = socketConnection;
            _socketConnectionManager = socketConnectionManager;
        }

        public Task NotifyAsync(IMessage message)
        {   
            var connectionsToNotify = _socketConnectionManager.GetConnectionsInsideTheBoundary(message.Point);
            return _sockerConnection.WriteAsync(connectionsToNotify, message);
        }
    }
}
