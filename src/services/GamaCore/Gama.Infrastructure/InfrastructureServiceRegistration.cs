using Gama.Application.Contracts.EventBus;
using Gama.Application.Contracts.FileManagement;
using Gama.Application.Contracts.Repositories;
using Gama.Application.UseCases.UserAgg.Interfaces;
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

        services.AddScoped<ITokenService, JwtTokenProvider>();

        services.AddDbContext<GamaCoreDbContext>(options =>
            options.UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention()
            );

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IRolesRepository, RolesRepository>();
        services.AddTransient<ITrafficViolationRepository, TrafficViolationRepository>();
        services.AddTransient<ITrafficFineRepository, TrafficFineRepository>();
        services.AddTransient<IOccurrenceRepository, OccurrenceRepository>();
        services.AddTransient<IOccurrenceUrgencyLevelRepository, OccurrenceUrgencyLevelRepository>();
        services.AddTransient<IOccurrenceTypeRepository, OccurrenceTypeRepository>();
        services.AddTransient<IOccurrenceStatusRepository, OccurrenceStatusRepository>();

        services.AddScoped<IFileManager, LocalFileManager>();

        services.AddSingleton<IEventBusProducer, RabbitMqEventBusProducer>();

        return services;
    }
}