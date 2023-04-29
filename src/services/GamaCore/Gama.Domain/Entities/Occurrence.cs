namespace Gama.Domain.Entities
{
    public class Occurrence : AuditableEntity
    {
        public long Id { get; set; }

        public int UserId { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string? Location { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public byte OccurrenceStatusId { get; set; }

        public byte OccurrenceTypeId { get; set; }

        public byte OccurrenceUrgencyLevelId { get; set; }

        public bool Active { get; set; }
    }
}
