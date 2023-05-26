namespace Gama.Domain.Common
{
    public abstract class Event
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public abstract string Name { get; }
    }
}
