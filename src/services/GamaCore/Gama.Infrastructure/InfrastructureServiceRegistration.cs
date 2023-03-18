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
        services.AddDbContext<GamaCoreDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("GamaCoreDbConnectionString")));

        services.AddTransient<IUserRepository, UserRepository>();

        return services;
    }
}