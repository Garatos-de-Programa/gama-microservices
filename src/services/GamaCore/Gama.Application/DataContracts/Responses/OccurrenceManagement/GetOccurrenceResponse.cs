using Gama.Domain.Entities;

namespace Gama.Application.DataContracts.Responses.OccurrenceManagement
{
    public class GetOccurrenceResponse
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string? Location { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool Active { get; set; }

        public string? OccurrenceUrgencyLevel { get; set; }

        public string? OccurrenceType { get; set; }

        public string? Status { get; set; }
    }
}
