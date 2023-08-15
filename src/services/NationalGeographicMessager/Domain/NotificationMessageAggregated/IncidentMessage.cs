using NationalGeographicMessager.Domain.GeolocationAggregated;
using NationalGeographicMessager.Domain.OcurrencesAggregated;
using System.Text;
using System.Text.Json;

namespace NationalGeographicMessager.Domain.NotificationMessageAggregated
{
    public class IncidentMessage : IMessage
    {
        public Point Point { get; }

        public OccurrenceEventMessage Message { get; }

        public IncidentMessage(double latitude, double longitude, OccurrenceEventMessage message)
        {
            Point = new Point()
            {
                Latitude = latitude,
                Longitude = longitude
            };
            Message = message;
        }

        public IncidentMessage(NetTopologySuite.Geometries.Point geolocation, OccurrenceEventMessage occurrenceEventMessage)
        {
            Point = new Point()
            {
                Latitude = geolocation.Y,
                Longitude = geolocation.X
            };
            Message = occurrenceEventMessage;
        }

        public byte[] GetBytes()
        {
            var messageString = JsonSerializer.Serialize(Message);
            return Encoding.UTF8.GetBytes(messageString);
        }
    }
}
