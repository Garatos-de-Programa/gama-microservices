using Gama.Domain.Common;
using Gama.Domain.Entities.OccurrencesAgg.Models;

namespace Gama.Domain.Entities.OccurrencesAgg.Events
{
    internal class DeletedOccurrenceEvent : Event
    {
        public override string Name => "deleted:occurrence";

        public int OccurrenceId { get; }

        public DeletedOccurrenceEvent(Occurrence occurrence)
        {
            OccurrenceId = occurrence.Id;
        }

    }
}
