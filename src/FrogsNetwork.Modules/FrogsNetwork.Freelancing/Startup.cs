using Fluid;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.Data.Migration;
using OrchardCore.Environment.Shell;
using OrchardCore.Modules;
using OrchardCore.Security.Permissions;

namespace FrogsNetwork.Freelancing
{
    public class Startup : StartupBase
    {
        private AdminOptions _adminOptions;
        private string _tenantName;

        public Startup(IOptions<AdminOptions> adminOptions, ShellSettings shellSettings)
        {
            _adminOptions = adminOptions.Value;
            _tenantName = shellSettings.Name;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IPermissionProvider, Permissions>();

            services.AddDataMigration<Migrations>();
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaControllerRoute(
                name: "Freelancing",
                areaName: "FrogsNetwork.Freelancing",
                pattern: "Home/Index",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
