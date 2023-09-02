using Microsoft.AspNetCore.SignalR;
using NationalGeographicMessager.Domain.NotificationMessageAggregated;
using NationalGeographicMessager.Domain.OcurrencesAggregated;
using System.Net.Sockets;

namespace NationalGeographicMessager.Infrastructure.SocketConnection
{
    internal class SignableOccurrenceSocketConnection : Hub, ISocketConnection 
    {
        private readonly IOccurrenceRepository _occurrenceRepository;
        private readonly ISocketConnectionManager _socketConnectionManager;

        public SignableOccurrenceSocketConnection(
            IOccurrenceRepository occurrenceRepository, 
            ISocketConnectionManager socketConnectionManager
            )
        {
            _occurrenceRepository = occurrenceRepository;
            _socketConnectionManager = socketConnectionManager;

        }

        public async Task WriteAsync(IEnumerable<ISingleClientProxy> connections, IMessage message)
        {
            var tasks = new List<Task>();
            foreach (var connection in connections)
            {
                tasks.Add(connection.SendAsync("ReceiveMessage", message));
            }

            await Task.WhenAll(tasks);
        }

        public async Task Subscribe(double latitude, double longitude, double radius)
        {
            var occurrences = await _occurrenceRepository.GetAsync(new (latitude, longitude, radius));
            await Task.WhenAll(
                Clients.Caller.SendAsync("ReceiveMessage", occurrences),
                Groups.AddToGroupAsync(Context.ConnectionId, "occurrenceGroup")
                );
            _socketConnectionManager.AddSocketConnection(Context.ConnectionId, new(latitude, longitude, radius));
        }

        public async Task Unsubscribe()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "occurrenceGroup");
            _socketConnectionManager.RemoveSocketConnection(Context.ConnectionId);
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _socketConnectionManager.RemoveSocketConnection(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
