namespace NationalGeographicMessager.Domain.OcurrencesAggregated
{
    internal class Occurrence
    {
        public int OccurrenceId { get; set; }

        public NetTopologySuite.Geometries.Point Geolocation { get; set; }

        public string? Location { get; set; }

        public string? OccurrenceName { get; set; }

        public string? StatusName { get; set; }

        public string? OccurrenceUrgencyLevelName { get; set; }

        public string? OccurrenceTypeName { get; set; }

        public int UserId { get; set; }

        public string? UserName { get; set; }

        public bool Active { get; set; }
    }
}
