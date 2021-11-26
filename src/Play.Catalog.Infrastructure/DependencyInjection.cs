using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Play.Catalog.Application.Common.Interfaces;
using Play.Catalog.Infrastructure.Persistence;
using Play.Catalog.Infrastructure.Services;
using Play.Catalog.Infrastructure.Settings;

namespace Play.Catalog.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var dbSettings = configuration.GetSection(nameof(MongoSettings)).Get<MongoSettings>();
        var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

        services.AddSingleton(serviceProvider =>
        {
            var mongoClient = new MongoClient(dbSettings.ConnectionString);
            return mongoClient.GetDatabase(serviceSettings.ServiceName);
        });

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddScoped<IDomainEventService, DomainEventService>();
        return services;
    }
}
