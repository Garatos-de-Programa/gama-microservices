namespace NationalGeographicMessager.Infrastructure.SocketConnection
{
    public interface ISocketConnection
    {
        Task WriteAsync(IEnumerable<string> connectionId, byte[] data);
    }
}
