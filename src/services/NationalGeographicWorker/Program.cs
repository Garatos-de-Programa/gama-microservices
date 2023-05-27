using MassTransit;
using MassTransit.Serialization;
using NationalGeographicWorker.Consumers.OccurrenceEvents;
using RabbitMQ.Client;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        services.AddMassTransit(c =>
        {
            c.AddConsumer<OccurrenceEventConsumer>();

            c.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("amqp://guest:guest@localhost:5672");
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
    })
    .Build();



host.Run();
