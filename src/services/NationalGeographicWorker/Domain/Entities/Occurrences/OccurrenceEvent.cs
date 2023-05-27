using NationalGeographicWorker.Domain.Common;

namespace NationalGeographicWorker.Domain.Entities.Occurrences
{
    internal class OccurrenceEvent : Event
    {
        public int OccurrenceId { get; set; }

        public int UserId { get; set; }

        public string? UserName { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string? Location { get; set; }

        public string? OccurrenceName { get; set; }

        public bool Active { get; set; }

        public string? StatusName { get; set; }

        public string? OccurrenceTypeName { get; set; }

        public string? OccurrenceUrgencyLevelName { get; set; }
    }
}
