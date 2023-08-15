using NationalGeographicMessager.Domain.NotificationMessageAggregated;
using System.Text.Json;

namespace NationalGeographicMessager.Domain.OcurrencesAggregated
{
    public class OccurrenceEventMessage
    {
        public int OccurrenceId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string? Location { get; set; }

        public string? OccurrenceName { get; set; }

        public string? StatusName { get; set; }

        public string? OccurrenceUrgencyLevelName { get; set; }

        public string? OccurrenceTypeName { get; set; }

        public int UserId { get; set; }

        public string? UserName { get; set; }

        public bool Active { get; set; }


        public IncidentMessage ToIncidentMessage()
        {
            return new IncidentMessage(Latitude, Longitude, this);
        }
    }
}
