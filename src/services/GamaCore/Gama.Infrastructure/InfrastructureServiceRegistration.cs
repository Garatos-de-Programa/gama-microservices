using Gama.Application.Contracts.Repositories;
using Gama.Application.Contracts.UserManagement;
using Gama.Infrastructure.Authentication;
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

        return services;
    }
}