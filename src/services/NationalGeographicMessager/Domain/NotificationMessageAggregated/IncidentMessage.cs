using NationalGeographicMessager.Domain.GeolocationAggregated;
using System.Text;

namespace NationalGeographicMessager.Domain.NotificationMessageAggregated
{
    public class IncidentMessage : IMessage
    {
        public Point Point { get; }

        public string Message { get; }

        public IncidentMessage(double latitude, double longitude, string message)
        {
            Point = new Point()
            {
                Latitude = latitude,
                Longitude = longitude
            };
            Message = message;
        }

        public byte[] GetBytes()
        {
            return Encoding.UTF8.GetBytes(Message);
        }
    }
}
