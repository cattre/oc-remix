using Microsoft.Extensions.DependencyInjection;
using OCRemix.Users.Application.Queries;
using OCRemix.Users.Infrastructure.Queries;

namespace OCRemix.Users.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOCRemixUsersInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserQueries, UserQueries>();

        return services;
    }
}
