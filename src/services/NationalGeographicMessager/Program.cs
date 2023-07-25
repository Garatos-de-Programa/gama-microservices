using MassTransit;
using Microsoft.EntityFrameworkCore;
using NationalGeographicMessager.Infrastructure.DatabaseListener;
using NationalGeographicWorker.Infrastructure.Persistence;

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

var connectionString = builder.Configuration.GetConnectionString("NationalGeographicDbConnectionString");
services.AddDbContext<NationalGeographicDbContext>(options =>
    options.UseNpgsql(connectionString, x => x.UseNetTopologySuite())
    .UseSnakeCaseNamingConvention()
    );


var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.Run();
