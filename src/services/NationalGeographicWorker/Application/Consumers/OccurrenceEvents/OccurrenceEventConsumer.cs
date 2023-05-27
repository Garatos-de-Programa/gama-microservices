using MassTransit;
using Microsoft.EntityFrameworkCore;
using NationalGeographicWorker.Domain.Entities.Occurrences;
using NationalGeographicWorker.Infrastructure.Persistence;

namespace NationalGeographicWorker.Application.Consumers.OccurrenceEvents
{
    internal class OccurrenceEventConsumer : IConsumer<OccurrenceEvent>
    {
        private readonly ILogger<OccurrenceEventConsumer> _logger;
        private readonly NationalGeographicDbContext _nationalGeographicDbContext;

        public OccurrenceEventConsumer(
            ILogger<OccurrenceEventConsumer> logger,
            NationalGeographicDbContext context
            )
        {
            _logger = logger;
            _nationalGeographicDbContext = context;
        }

        public async Task Consume(ConsumeContext<OccurrenceEvent> context)
        {

            _logger.LogInformation("Recivied event: {Id}  Type: {Type}", context.Message.Id, context.Message.Name);

            Occurrence occurrence = new()
            {
                OccurrenceId = context.Message.OccurrenceId,
                OccurrenceName = context.Message.OccurrenceName,
                Active = context.Message.Active,
                Geolocation = new NetTopologySuite.Geometries.Point((double)context.Message.Latitude, (double)context.Message.Longitude)
                {
                    SRID = 4326
                },
                Location = context.Message.Location,
                OccurrenceTypeName = context.Message.OccurrenceTypeName,
                OccurrenceUrgencyLevelName = context.Message.OccurrenceUrgencyLevelName,
                StatusName = context.Message.StatusName,
                UserId = context.Message.UserId,
                UserName = context.Message.UserName
            };

            switch (context.Message.Name)
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
