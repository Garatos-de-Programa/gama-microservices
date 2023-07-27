using Gama.Domain.Common;

namespace Gama.Domain.Models.Occurrences
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
