using Gama.Application.Contracts.Repositories;
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
        
        services.AddDbContext<GamaCoreDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddTransient<IUserRepository, UserRepository>();

        return services;
    }
}