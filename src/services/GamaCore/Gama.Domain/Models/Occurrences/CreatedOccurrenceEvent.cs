using Gama.Domain.Common;

namespace Gama.Domain.Models.Occurrences
{
    public class CreatedOccurrenceEvent : Event
    {
        public override string Name => "created:occurrence";

        public int OccurrenceId { get; set; }

        public int UserId { get; }

        public string? UserName { get; }

        public decimal Latitude { get; }

        public decimal Longitude { get; }

        public string? Location { get; }

        public string? OccurrenceName { get; }

        public bool Active { get; }

        public string? StatusName { get; }

        public string? OccurrenceTypeName { get; }

        public string? OccurrenceUrgencyLevelName { get; }

        public CreatedOccurrenceEvent(Occurrence occurrence, string userName)
        {
            OccurrenceId = occurrence.Id;
            UserId = occurrence.UserId;
            Latitude = occurrence.Latitude;
            Longitude = occurrence.Longitude;
            Location = occurrence.Location;
            OccurrenceName = occurrence.Name;
            Active = occurrence.Active;
            StatusName = ((OccurrenceUrgencyLevelType)occurrence?.OccurrenceStatusId).ToString();
            OccurrenceTypeName = ((OccurrenceUrgencyLevelType)occurrence?.OccurrenceTypeId).ToString();
            OccurrenceUrgencyLevelName = ((OccurrenceUrgencyLevelType)occurrence?.OccurrenceUrgencyLevelId).ToString();
            UserName = userName;
        }
    }
}
