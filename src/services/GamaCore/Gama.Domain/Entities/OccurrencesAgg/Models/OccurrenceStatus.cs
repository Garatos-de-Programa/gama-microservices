using Gama.Domain.ValueTypes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gama.Domain.Entities.OccurrencesAgg.Models
{
    public class OccurrenceStatus
    {
        public short Id { get; set; }

        public string? Name { get; set; }

        [NotMapped]
        public int OccurrenceId { get; set; }

        public OccurrenceStatus()
        {
        }

        protected OccurrenceStatus(int occurrenceId)
        {
            OccurrenceId = occurrenceId;
        }

        public virtual Result<bool> UpdateStatus(Occurrence occurrence) { return true; }
    }
}
