using Gama.Domain.Common;

namespace Gama.Domain.Entities.OccurrencesAgg
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
