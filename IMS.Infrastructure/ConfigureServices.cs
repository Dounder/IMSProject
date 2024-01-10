using IMS.Domain.Interfaces;
using IMS.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}