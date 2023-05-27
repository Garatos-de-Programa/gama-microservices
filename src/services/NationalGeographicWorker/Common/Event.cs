namespace NationalGeographicWorker.Common
{
    internal abstract class Event
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Name { get; set; }
    }
}
