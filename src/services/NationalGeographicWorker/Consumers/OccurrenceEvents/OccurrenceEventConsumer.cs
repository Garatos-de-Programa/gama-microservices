using MassTransit;

namespace NationalGeographicWorker.Consumers.OccurrenceEvents
{
    internal class OccurrenceEventConsumer : IConsumer<OccurrenceEvent>
    {
        private readonly ILogger<OccurrenceEventConsumer> _logger;

        public OccurrenceEventConsumer(ILogger<OccurrenceEventConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<OccurrenceEvent> context)
        {

            _logger.LogInformation("Recivied event ID", context.Message.Id);
            return Task.CompletedTask;
        }
    }
}
