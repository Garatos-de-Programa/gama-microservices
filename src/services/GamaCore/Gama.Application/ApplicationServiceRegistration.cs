using Gama.Application.Contracts.UserManagement;
using Gama.Application.UseCases.UserManagement;
using Microsoft.Extensions.DependencyInjection;

namespace Gama.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, JwtTokenService>();
        services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();

        return services;
    }
}