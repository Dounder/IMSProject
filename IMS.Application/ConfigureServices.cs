using FluentValidation;
using IMS.Application.Common.Behaviors;
using IMS.Application.UseCases.Auth.Services;
using IMS.Application.UseCases.Users;
using IMS.Application.UseCases.Users.Services;
using IMS.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ConfigureServices));

        services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        });

        services.AddScoped<UserService>();
        services.AddScoped<RoleService>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}