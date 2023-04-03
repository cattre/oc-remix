using System;
using OrchardCore.Email;
using OrchardCore.Environment.Shell.Configuration;

namespace OCRemix.Web
{
	public class Startup
	{
		private readonly IHostEnvironment _env;
		public Startup(IHostEnvironment env)
		{
			_env = env;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			var builder = services.AddOrchardCms().ConfigureServices((tenantServices, serviceProvider) =>
			{
				// Instead of IShellConfiguration you could fetch the configuration
				// values from an injected IConfiguration instance here too. While that
				// would also allow you to access standard ASP.NET Core configuration
				// keys it won't have support for all the hierarchical sources
				// detailed above.
				var shellConfiguration = serviceProvider.GetRequiredService<IShellConfiguration>();
			});

			services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Startup>());

			if (_env.IsDevelopment() || _env.EnvironmentName == "Preview")
			{
				builder.AddSetupFeatures("OrchardCore.AutoSetup");
			}
		}

		public void Configure(IApplicationBuilder app, IHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();
			app.UseOrchardCore();
		}
	}
}

