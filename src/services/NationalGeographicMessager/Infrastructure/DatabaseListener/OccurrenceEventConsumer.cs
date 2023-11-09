using MassTransit;
using Microsoft.EntityFrameworkCore;
using NationalGeographicMessager.Domain.NotificationMessageAggregated;
using NationalGeographicMessager.Domain.OcurrencesAggregated;
using NationalGeographicWorker.Infrastructure.Persistence;

namespace NationalGeographicMessager.Infrastructure.DatabaseListener
{
    internal class OccurrenceEventConsumer : IConsumer<OccurrenceEventMessage>
    {
        private readonly ILogger<OccurrenceEventConsumer> _logger;
        private readonly IMessageNotifier _messageNotifier;
        private readonly NationalGeographicDbContext _nationalGeographicDbContext;

        public OccurrenceEventConsumer(
            ILogger<OccurrenceEventConsumer> logger,
            IMessageNotifier messageNotifier,
            NationalGeographicDbContext nationalGeographicDbContext
            )
        {
            _logger = logger;
            _messageNotifier = messageNotifier;
            _nationalGeographicDbContext = nationalGeographicDbContext;
        }

        public async Task Consume(ConsumeContext<OccurrenceEventMessage> context)
        {
            _logger.LogInformation("Recivied occurrence: {Id}", context.Message.OccurrenceId);

            await Task.WhenAll(
                    _messageNotifier.NotifyAsync(context.Message.ToIncidentMessage()),
                    UpdateDatabase(context.Message)
                    );
        }

        internal async Task UpdateDatabase(OccurrenceEventMessage occurrenceEventMessage)
        {
            Occurrence occurrence = new()
            {
                OccurrenceId = occurrenceEventMessage.OccurrenceId,
                OccurrenceName = occurrenceEventMessage.OccurrenceName,
                Active = occurrenceEventMessage.Active,
                Geolocation = new NetTopologySuite.Geometries.Point((double)occurrenceEventMessage.Longitude, (double)occurrenceEventMessage.Latitude)
                {
                    SRID = 4326
                },
                Location = occurrenceEventMessage.Location,
                OccurrenceTypeName = occurrenceEventMessage.OccurrenceTypeName,
                OccurrenceUrgencyLevelName = occurrenceEventMessage.OccurrenceUrgencyLevelName,
                StatusName = occurrenceEventMessage.StatusName,
                UserId = occurrenceEventMessage.UserId,
                UserName = occurrenceEventMessage.UserName
            };

            switch (occurrenceEventMessage.Name)
            {
                case "created:occurrence":
                    await _nationalGeographicDbContext.Occurrences.AddAsync(occurrence);
                    await _nationalGeographicDbContext.SaveChangesAsync();
                    break;

                case "updated:occurrence":
                    _nationalGeographicDbContext.Attach(occurrence);
                    _nationalGeographicDbContext.Entry(occurrence).State = EntityState.Modified;
                    break;

                case "deleted:occurrence":
                    _nationalGeographicDbContext.Occurrences.Remove(occurrence);
                    break;
            }

            await _nationalGeographicDbContext.SaveChangesAsync();
        }
    }
}
