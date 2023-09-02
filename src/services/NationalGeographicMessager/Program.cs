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

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddMassTransit(c =>
 {
     c.AddConsumer<OccurrenceEventConsumer>();

     c.UsingAmazonSqs((context, cfg) =>
     {
         cfg.Host("us-east-2", h =>
         {
             h.AccessKey("");
             h.SecretKey("");
         });

         cfg.ReceiveEndpoint("gama-api-occurrences.fifo", e =>
         {
             e.ConfigureConsumeTopology = false;
             e.ConfigureConsumer<OccurrenceEventConsumer>(context);
             e.ClearSerialization();
             e.UseRawJsonSerializer();
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
