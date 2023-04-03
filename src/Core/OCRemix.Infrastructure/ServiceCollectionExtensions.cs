using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MediatR;
using MediatR.Registration;
using Microsoft.Extensions.DependencyInjection;

namespace OCRemix.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatRClasses(this IServiceCollection services, params Type[] handlerAssemblyMarkerTypes)
            => services.AddMediatRClasses(handlerAssemblyMarkerTypes.Select(t => t.GetTypeInfo().Assembly));

        public static IServiceCollection AddMediatRClasses(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            ServiceRegistrar.AddMediatRClasses(services, assemblies);

            return services;
        }

        public static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            // MediatR service helper adds IMediator and other registrations.
            // Each module should also register it's command handlers through the helper.
            ServiceRegistrar.AddRequiredServices(services, new MediatRServiceConfiguration());

            return services;
        }
    }
}
