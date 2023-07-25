using Microsoft.AspNetCore.SignalR;
using NationalGeographicMessager.Domain.GeolocationAggregated;
using NationalGeographicMessager.Domain.OcurrencesAggregated;
using System.Net.Sockets;

namespace NationalGeographicMessager.Infrastructure.SocketConnection
{
    public class SignableOccurrenceSocketConnection : Hub, ISocketConnection 
    {
        private readonly IOccurrenceRepository _occurrenceRepository;

        public IDictionary<string, Point> Connections { get; }

        public SignableOccurrenceSocketConnection(IOccurrenceRepository occurrenceRepository)
        {
            _occurrenceRepository = occurrenceRepository;
            Connections = new Dictionary<string, Point>();
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
            await Clients.Caller.SendAsync("ReceiveOccurrences", occurrences);
            await Groups.AddToGroupAsync(Context.ConnectionId, "occurrenceGroup");
            Connections.Add(Context.ConnectionId, new(latitude, longitude, radius));
        }

        public async Task Unsubscribe()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "occurrenceGroup");
            Connections.Remove(Context.ConnectionId);
        }
    }
}
