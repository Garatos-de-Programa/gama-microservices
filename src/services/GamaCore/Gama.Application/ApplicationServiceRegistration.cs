using Gama.Application.Contracts.Mappers;
using Gama.Application.Contracts.UserManagement;
using Gama.Application.Seedworks.Mappers;
using Gama.Application.UseCases.UserManagement;
using Microsoft.Extensions.DependencyInjection;

namespace Gama.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddAutoMapper(typeof(UnifiedMapperProfile));
        services.AddSingleton<IEntityMapper, AutoMapperMapper>();

        return services;
    }
}