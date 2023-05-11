namespace Gama.Application.DataContracts.Responses.OccurrenceManagement
{
    public class SearchOcurrenceResponse
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? OccurrenceUrgencyLevel { get; set; }

        public string? OccurrenceType { get; set; }

        public string? Status { get; set; }

        public bool Active { get; set; }
    }
}
