using MassTransit;
using Microsoft.EntityFrameworkCore;
using NationalGeographicWorker.Application.Consumers.OccurrenceEvents;
using NationalGeographicWorker.Infrastructure.Persistence;
using RabbitMQ.Client;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builderctx, services) =>
    {
        services.AddMassTransit(c =>
        {
            c.AddConsumer<OccurrenceEventConsumer>();

            c.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(builderctx.Configuration.GetConnectionString("EventBusConnectionString"));
                cfg.ReceiveEndpoint("national-geographic-worker", c =>
                {
                    c.ConfigureConsumeTopology = false;
                    c.ConfigureConsumer<OccurrenceEventConsumer>(ctx);
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

        var connectionString = builderctx.Configuration.GetConnectionString("NationalGeographicDbConnectionString");
        services.AddDbContext<NationalGeographicDbContext>(options =>
            options.UseNpgsql(connectionString, x => x.UseNetTopologySuite())
            .UseSnakeCaseNamingConvention()
            );
    })
    .Build();



host.Run();
