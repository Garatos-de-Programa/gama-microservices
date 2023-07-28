namespace Gama.Domain.Entities.OccurrencesAgg
{
    public class OccurrenceUrgencyLevel
    {
        public short Id { get; set; }

        public string? Name { get; set; }
    }

    public enum OccurrenceUrgencyLevelType
    {
        None = 1,
    }
}
