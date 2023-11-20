namespace Gama.Domain.Entities.OccurrencesAgg.Models
{
    public class OccurrenceType
    {
        public short Id { get; set; }

        public string? Name { get; set; }
    }

    public enum OccurrenceTypes
    {
        Roubo = 1,
        Incêndio,
        BuracoNaVia,
        Barulho,
        MausTratos
    }
}
