using MassTransit;
using NationalGeographicMessager.Domain.NotificationMessageAggregated;
using NationalGeographicMessager.Domain.OcurrencesAggregated;

namespace NationalGeographicMessager.Infrastructure.DatabaseListener
{
    internal class RabbitMqOccurrenceEventConsumer : IConsumer<OccurrenceEventMessage>
    {
        private readonly ILogger<RabbitMqOccurrenceEventConsumer> _logger;
        private readonly IMessageNotifier _messageNotifier;

        public RabbitMqOccurrenceEventConsumer(
            ILogger<RabbitMqOccurrenceEventConsumer> logger,
            IMessageNotifier messageNotifier
            )
        {
            _logger = logger;
            _messageNotifier = messageNotifier;
        }

        public async Task Consume(ConsumeContext<OccurrenceEventMessage> context)
        {
            _logger.LogInformation("Recivied occurrence: {Id}", context.Message.OccurrenceId);

            await _messageNotifier.NotifyAsync(context.Message.ToIncidentMessage());
        }
    }
}
