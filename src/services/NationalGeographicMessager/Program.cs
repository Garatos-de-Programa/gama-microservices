using MassTransit;
using Microsoft.EntityFrameworkCore;
using NationalGeographicMessager.Domain.GeolocationAggregated;
using NationalGeographicMessager.Domain.NotificationMessageAggregated;
using NationalGeographicMessager.Domain.OcurrencesAggregated;
using NationalGeographicMessager.Infrastructure.DatabaseListener;
using NationalGeographicMessager.Infrastructure.Notifier;
using NationalGeographicMessager.Infrastructure.Repositories;
using NationalGeographicMessager.Infrastructure.SocketConnection;
using NationalGeographicWorker.Infrastructure.Persistence;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddMassTransit(c =>
 {
     c.AddConsumer<RabbitMqOccurrenceEventConsumer>();

     c.UsingRabbitMq((ctx, cfg) =>
     {
         cfg.Host(builder.Configuration.GetConnectionString("EventBusConnectionString"));
         cfg.ReceiveEndpoint("national-geographic-worker", c =>
         {
             c.ConfigureConsumeTopology = false;
             c.ConfigureConsumer<RabbitMqOccurrenceEventConsumer>(ctx);
             c.Bind("gama.api:events-exchange", s =>
             {
                 s.RoutingKey = "gama.api:event-occurrences";
                 s.ExchangeType = ExchangeType.Topic;
             });
             c.ClearSerialization();
             c.UseRawJsonSerializer();
         });
     });
 });

services.AddTransient<IOccurrenceRepository, EFCoreOccurrenceRepository>();
services.AddSingleton<IGeoLocationCalculator, GeoLocationCalculator>();
services.AddTransient<IMessageNotifier, RadiusBoundedSocketMessageNotifier>();
services.AddSingleton<ISocketConnectionManager, ThreadSafeSocketConnectionManager>();
services.AddTransient<ISocketConnection, SignableOccurrenceSocketConnection>();
services.AddSignalR();

var connectionString = builder.Configuration.GetConnectionString("NationalGeographicDbConnectionString");
services.AddDbContext<NationalGeographicDbContext>(options =>
    options.UseNpgsql(connectionString, x => x.UseNetTopologySuite())
    .UseSnakeCaseNamingConvention()
    );


var app = builder.Build();

app.MapHub<SignableOccurrenceSocketConnection>("/occurrences-hub");

app.UseHttpsRedirection();
app.Run();
