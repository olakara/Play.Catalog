using Microsoft.Extensions.DependencyInjection;
using Play.Catalog.Application.Common.Interfaces;
using Play.Catalog.Infrastructure.Services;

namespace Play.Catalog.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddScoped<IDomainEventService, DomainEventService>();
        return services;
    }
}
