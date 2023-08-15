using Microsoft.AspNetCore.SignalR;
using NationalGeographicMessager.Domain.NotificationMessageAggregated;

namespace NationalGeographicMessager.Infrastructure.SocketConnection
{
    public interface ISocketConnection
    {
        Task WriteAsync(IEnumerable<ISingleClientProxy> connections, IMessage message);
    }
}
