using Amazon.S3;
using Gama.Application.UseCases.UserAgg.Interfaces;
using Gama.Domain.Entities.OccurrencesAgg.Repositories;
using Gama.Domain.Entities.TrafficFinesAgg;
using Gama.Domain.Entities.UsersAgg;
using Gama.Domain.Interfaces.EventBus;
using Gama.Domain.Interfaces.FileManagement;
using Gama.Infrastructure.Authentication;
using Gama.Infrastructure.EventBus;
using Gama.Infrastructure.FileManager;
using Gama.Infrastructure.Persistence;
using Gama.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gama.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("GamaCoreDbConnectionString");
        services.AddHttpClient();

        services.AddScoped<ITokenService, JwtTokenProvider>();

        services.AddDbContextPool<GamaCoreDbContext>(options =>
            options.UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention()
            );

        services.AddDefaultAWSOptions(configuration.GetAWSOptions());
        services.AddAWSService<IAmazonS3>();

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IRolesRepository, RolesRepository>();
        services.AddTransient<ITrafficViolationRepository, TrafficViolationRepository>();
        services.AddTransient<ITrafficFineRepository, TrafficFineRepository>();
        services.AddTransient<IOccurrenceRepository, OccurrenceRepository>();
        services.AddTransient<IOccurrenceUrgencyLevelRepository, OccurrenceUrgencyLevelRepository>();
        services.AddTransient<IOccurrenceTypeRepository, OccurrenceTypeRepository>();
        services.AddTransient<IOccurrenceStatusRepository, OccurrenceStatusRepository>();

        services.AddScoped<IFileManager, S3FileManager>();

        services.AddSingleton<IEventBusProducer, SQSEventBusProducer>();

        return services;
    }
}