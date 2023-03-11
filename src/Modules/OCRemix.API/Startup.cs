using System.Web;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NSwag;
using NSwag.Generation.Processors.Security;
using OrchardCore.Environment.Shell;
using OrchardCore.Environment.Shell.Scope;
using OrchardCore.Modules;
using OrchardCore.Users;


namespace OCRemix.API
{
    public class Startup : StartupBase
    {
        private readonly string _tenantName;

        public Startup(ShellSettings shellSettings)
        {
            _tenantName = shellSettings.Name;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                var userOptions = ShellScope.Services.GetRequiredService<IOptions<UserOptions>>();

                options.Cookie.Name = "ocremixauth_" + HttpUtility.UrlEncode(_tenantName);

                // Don't set the cookie builder 'Path' so that it uses the 'IAuthenticationFeature' value
                // set by the pipeline and comming from the request 'PathBase' which already ends with the
                // tenant prefix but may also start by a path related e.g to a virtual folder.

                options.LoginPath = "/" + userOptions.Value.LoginPath;
                options.LogoutPath = "/" + userOptions.Value.LogoffPath;
                options.AccessDeniedPath = "/Error/403";
            });


            // services.AddMvc().AddNewtonsoftJson(opts =>
            // {
            //     opts.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            // });

            services.AddProblemDetails(options =>
            {
                //this must be set to rethrow all in order to roll back session
                options.RethrowAll();
                options.IsProblem = (context) =>
                {
                    // Don't return problem details for 300 redirect codes (interferes with auth).
                    if (context.Response.StatusCode < 200 || context.Response.StatusCode > 400)
                    {
                        var statusCodePagesFeature = context.Features.Get<IStatusCodePagesFeature>();
                        if (statusCodePagesFeature != null && statusCodePagesFeature.Enabled)
                        {
                            return true;
                        }
                    }

                    return false;
                };

                options.IncludeExceptionDetails = (context, ex) =>
                {
                    var exceptionHandlerFeature = new ExceptionHandlerFeature()
                    {
                        Error = ex,
                        Path = context.Request.Path,
                    };
                    context.Features.Set<IExceptionHandlerFeature>(exceptionHandlerFeature);

                    var env = context.RequestServices.GetRequiredService<IHostEnvironment>();
                    return env.IsDevelopment() && false;
                };


                // Func<IHaveAProblem, ProblemDetails> problemDetailsFactory = (IHaveAProblem ex) =>
                // {
                //     var pd = ex.GetProblemDetails();
                //
                //     if (pd is MountbaseValidationProblemDetails vpd)
                //     {
                //         var ret = new Microsoft.AspNetCore.Mvc.ValidationProblemDetails { Title = vpd.Title, Status = vpd.StatusCode, Detail = vpd.Details };
                //
                //         foreach (var error in vpd.Errors)
                //         {
                //             ret.Errors.Add(error.Key, error.Value);
                //         }
                //
                //         return ret;
                //     }
                //     else
                //     {
                //         return new Microsoft.AspNetCore.Mvc.ProblemDetails { Title = pd.Title, Status = pd.StatusCode, Detail = pd.Details };
                //     }
                // };
                //
                // options.Map<MountbaseException>(problemDetailsFactory);
                // //options.Map<MountbaseNotFoundException>(problemDetailsFactory);
                // options.Map<MountbaseArgumentException>(problemDetailsFactory);
                // options.Map<MountbaseArgumentNullException>(problemDetailsFactory);
                // options.Map<MountbaseInvalidOperationException>(problemDetailsFactory);
                // options.Map<MountbaseValidationException>(problemDetailsFactory);

            });

            services.AddOpenApiDocument(document =>
            {
                document.GenerateXmlObjects = false;
                document.AddSecurity("bearer", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.OAuth2,
                    Description = "OpenID Connect",
                    Flow = OpenApiOAuth2Flow.AccessCode,
                    Flows = new OpenApiOAuthFlows()
                    {
                        AuthorizationCode = new OpenApiOAuthFlow()
                        {
                            Scopes = new Dictionary<string, string>
                                 {
                                    { "openid", "OpenID" }
                                 },
                            AuthorizationUrl = "/connect/authorize",
                            TokenUrl = "/connect/token"
                        },
                    }
                });
                document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("bearer"));

            });

            // services.Configure<RouteOptions>(routeOptions =>
            // {
            //     routeOptions.ConstraintMap.TryAdd("compactguid", typeof(CompactGuidConstraint));
            // });
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            builder.UseOpenApi();
            builder.UseSwaggerUi3();

            builder.UseWhen(
                  context => context.Request.Path.StartsWithSegments(new Microsoft.AspNetCore.Http.PathString("/api")),
                  b => b.UseProblemDetails()
                  );
        }
    }
}
