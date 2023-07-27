namespace Gama.Application.UseCases.OccurrenceAgg.Responses
{
    public class OccurrencePropertiesResponse
    {
        public IEnumerable<GetOccurrenceUrgencyLevelResponse>? UrgencyLevels { get; set; }

        public IEnumerable<GetOccurrenceTypeResponse>? Types { get; set; }

        public IEnumerable<GetOccurrenceStatusResponse>? Status { get; set; }
    }
}
