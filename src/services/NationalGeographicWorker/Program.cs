using MassTransit;
using Microsoft.EntityFrameworkCore;
using NationalGeographicWorker.Application.Consumers.OccurrenceEvents;
using NationalGeographicWorker.Infrastructure.Persistence;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builderctx, services) =>
    {
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

        var connectionString = builderctx.Configuration.GetConnectionString("NationalGeographicDbConnectionString");
        services.AddDbContext<NationalGeographicDbContext>(options =>
            options.UseNpgsql(connectionString, x => x.UseNetTopologySuite())
            .UseSnakeCaseNamingConvention()
            );
    })
    .Build();



host.Run();
