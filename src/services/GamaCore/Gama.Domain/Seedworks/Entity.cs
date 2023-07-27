using System.ComponentModel.DataAnnotations.Schema;

namespace Gama.Domain.Common
{
    public abstract class Entity
    {
        [NotMapped]
        public List<Event> Events { get; } = new();

        protected void AddEvent(Event @event)
        {
            Events.Add(@event);
        }
    }
}
