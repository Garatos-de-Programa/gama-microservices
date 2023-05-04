using Gama.Application.Contracts.Mappers;
using Gama.Application.Contracts.TrafficFineManagement;
using Gama.Application.Contracts.UserManagement;
using Gama.Application.Seedworks.Mappers;
using Gama.Application.UseCases.TrafficFineManagement;
using Gama.Application.UseCases.UserManagement;
using Microsoft.Extensions.DependencyInjection;

namespace Gama.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITrafficViolationService, TrafficViolationService>();
        services.AddScoped<ICurrentUserAccessor, HttpContextCurrentUserAcessor>();
        services.AddAutoMapper(typeof(UnifiedMapperProfile));
        services.AddSingleton<IEntityMapper, AutoMapperMapper>();

        services.AddHttpContextAccessor();

        return services;
    }
}