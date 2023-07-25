using Microsoft.AspNetCore.SignalR;
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

        public async Task WriteAsync(IEnumerable<string> connectionId, byte[] data)
        {
            var tasks = new List<Task>();
            foreach (var id in connectionId)
            {
                tasks.Add(Clients.Client(id).SendAsync("ReceiveMessage", data));
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        public async Task Subscribe(double latitude, double longitude, double radius)
        {
            var occurrences = await _occurrenceRepository.GetAsync(new (latitude, longitude, radius)).ConfigureAwait(false);
            await Task.WhenAll(
                Clients.Caller.SendAsync("ReceiveOccurrences", occurrences),
                Groups.AddToGroupAsync(Context.ConnectionId, "occurrenceGroup")
                ).ConfigureAwait(false);
            _socketConnectionManager.AddSocketConnection(Context.ConnectionId, new(latitude, longitude, radius));
        }

        public async Task Unsubscribe()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "occurrenceGroup");
            _socketConnectionManager.RemoveSocketConnection(Context.ConnectionId);
        }
    }
}
