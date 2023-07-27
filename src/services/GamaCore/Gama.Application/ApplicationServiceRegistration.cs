using Gama.Application.Contracts.Mappers;
using Gama.Application.Seedworks.Mappers;
using Gama.Application.UseCases.OccurrenceAgg.Implementations;
using Gama.Application.UseCases.OccurrenceAgg.Interfaces;
using Gama.Application.UseCases.TrafficFineAgg.Implementations;
using Gama.Application.UseCases.TrafficFineAgg.Interfaces;
using Gama.Application.UseCases.UserAgg.Implementations;
using Gama.Application.UseCases.UserAgg.Interfaces;
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
        services.AddScoped<ITrafficFineService, TrafficFineService>();
        services.AddScoped<IOccurrenceService, OccurrenceService>();
        services.AddScoped<ITrafficFineFileService, TrafficFineFileService>();
        services.AddAutoMapper(typeof(UnifiedMapperProfile));
        services.AddSingleton<IEntityMapper, AutoMapperMapper>();

        services.AddHttpContextAccessor();

        return services;
    }
}