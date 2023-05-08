namespace Gama.Application.DataContracts.Commands.OccurrenceManagement
{
    public class CreateOccurrenceCommand
    {
        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string? Location { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public byte OccurrenceStatusId { get; set; }

        public byte OccurrenceTypeId { get; set; }

        public byte OccurrenceUrgencyLevelId { get; set; }
    }
}
