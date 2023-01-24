using Fluid;
using FrogsNetwork.Freelancing.Controllers;
using FrogsNetwork.Freelancing.Handlers;
using FrogsNetwork.Freelancing.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.Data.Migration;
using OrchardCore.Environment.Shell;
using OrchardCore.Modules;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCore.Security.Permissions;
using OrchardCore.Users.Events;
using OrchardCore.Users.Services;

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
            services.AddScoped<IRegistrationFormEvents, UserRegistrationHandler>();
            services.AddScoped<ILoginFormEvent, UserLoginHandler>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<UserService>();
            services.AddTransient<ProfileService>();
            
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            var moduleRoutePrefix = "freelancing";
            var freelancerProfileControllerName = typeof(FreelancerProfileController).ControllerName();

            //routes.MapAreaControllerRoute(
            //    name: "Freelancing",
            //    areaName: "FrogsNetwork.Freelancing",
            //    pattern: "Home/Index",
            //    defaults: new { controller = "Home", action = "Index" }
            //);

            routes.MapAreaControllerRoute(
              name: "FreelancerProfileIndex",
              areaName: "FrogsNetwork.Freelancing",
              pattern: "FreelancerProfile/Index",
              defaults: new { controller = freelancerProfileControllerName, action = nameof(FreelancerProfileController.Index) }
          );

            builder.UseAuthorization();
        }
    }
}
