using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OCRemix.Items.Application;
using OCRemix.Items.Infrastructure;
using OrchardCore.Modules;

namespace OCRemix.Items.Api;

public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Startup>());

        services.AddOCRemixItemsInfrastructure();
        services.AddOCRemixItemsApplication();
    }

    public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
    }
}
