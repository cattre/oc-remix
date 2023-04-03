using Microsoft.Extensions.DependencyInjection;
using OCRemix.Items.Application.Queries;
using OCRemix.Items.Infrastructure.Queries;

namespace OCRemix.Items.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOCRemixItemsInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IItemQueries, ItemQueries>();

        return services;
    }
}
