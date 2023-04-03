using Microsoft.Extensions.DependencyInjection;
using OCRemix.Infrastructure;

namespace OCRemix.Items.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOCRemixItemsApplication(this IServiceCollection services)
        {
            services.AddMediatRClasses(typeof(ServiceCollectionExtensions));

            return services;
        }
    }
}
