using Fluid;
using FrogsNetwork.Freelancing.Controllers;
using FrogsNetwork.Freelancing.Handlers;
using FrogsNetwork.Freelancing.Services;
using GoogleApi.Extensions;
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
using OrchardCore.Navigation;
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
            services.AddScoped<INavigationProvider, MainMenu>();
            services.AddScoped<IPermissionProvider, Permissions>();
            services.AddDataMigration<Migrations>();
            services.AddScoped<IRegistrationFormEvents, UserRegistrationHandler>();
            services.AddScoped<ILoginFormEvent, UserLoginHandler>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<UserService>();
            services.AddTransient<ProfileService>();
            services.AddMvc()
            .AddSessionStateTempDataProvider();
            services.AddSession();
            services.AddGoogleApiClients();

        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            var moduleRoutePrefix = "freelancing";
            var freelancerProfileControllerName = typeof(FreelancerProfileController).ControllerName();
            var freelancerExtendedControllerName = typeof(FreelancerExtendedController).ControllerName();
            var companyProfileControllerName = typeof(CompanyProfileController).ControllerName();
            var freelancerSearchControllerName = typeof(FreelancerSearchController).ControllerName();
            var freelancerDetailControllerName = typeof(FreelancerDetailController).ControllerName();

            #region Routes
            #region FreeLancerProFile
            routes.MapAreaControllerRoute(
              name: $"{freelancerProfileControllerName}{nameof(FreelancerProfileController.Index)}",
              areaName: "FrogsNetwork.Freelancing",
              pattern: $"{freelancerProfileControllerName}/{nameof(FreelancerProfileController.Index)}",
              defaults: new { controller = freelancerProfileControllerName, action = nameof(FreelancerProfileController.Index) });

            routes.MapAreaControllerRoute(
            name: $"{freelancerProfileControllerName}{nameof(FreelancerProfileController.AddNationality)}",
            areaName: "FrogsNetwork.Freelancing",
            pattern: $"{freelancerProfileControllerName}/{nameof(FreelancerProfileController.AddNationality)}",
            defaults: new { controller = freelancerProfileControllerName, action = nameof(FreelancerProfileController.AddNationality) });

            routes.MapAreaControllerRoute(
              name: $"{freelancerProfileControllerName}{nameof(FreelancerProfileController.RemoveNationality)}",
              areaName: "FrogsNetwork.Freelancing",
              pattern: $"{freelancerProfileControllerName}/{nameof(FreelancerProfileController.RemoveNationality)}",
              defaults: new { controller = freelancerProfileControllerName, action = nameof(FreelancerProfileController.RemoveNationality) });
            #endregion

            routes.MapAreaControllerRoute(
             name: $"{companyProfileControllerName}{nameof(CompanyProfileController.Index)}",
             areaName: "FrogsNetwork.Freelancing",
             pattern: $"{companyProfileControllerName}/{nameof(CompanyProfileController.Index)}",
             defaults: new { controller = companyProfileControllerName, action = nameof(CompanyProfileController.Index) });

            routes.MapAreaControllerRoute(
             name: $"{freelancerSearchControllerName}{nameof(FreelancerSearchController.Index)}",
             areaName: "FrogsNetwork.Freelancing",
             pattern: $"{freelancerSearchControllerName}/{nameof(FreelancerSearchController.Index)}",
             defaults: new { controller = freelancerSearchControllerName, action = nameof(FreelancerSearchController.Index) });

            routes.MapAreaControllerRoute(
              name: $"{freelancerDetailControllerName}{nameof(FreelancerDetailController.Index)}",
              areaName: "FrogsNetwork.Freelancing",
              pattern: $"{freelancerDetailControllerName}/{nameof(FreelancerDetailController.Index)}",
              defaults: new { controller = freelancerDetailControllerName, action = nameof(FreelancerDetailController.Index) });

            #region
            routes.MapAreaControllerRoute(
             name: $"{freelancerExtendedControllerName}{nameof(FreelancerExtendedController.Index)}",
             areaName: "FrogsNetwork.Freelancing",
             pattern: $"{freelancerExtendedControllerName}/{nameof(FreelancerExtendedController.Index)}",
             defaults: new { controller = freelancerExtendedControllerName, action = nameof(FreelancerExtendedController.Index) });

            routes.MapAreaControllerRoute(
            name: $"{freelancerExtendedControllerName}{nameof(FreelancerExtendedController.AddCertificate)}",
            areaName: "FrogsNetwork.Freelancing",
            pattern: $"{freelancerExtendedControllerName}/{nameof(FreelancerExtendedController.AddCertificate)}",
            defaults: new { controller = freelancerExtendedControllerName, action = nameof(FreelancerExtendedController.AddCertificate) });

            routes.MapAreaControllerRoute(
           name: $"{freelancerExtendedControllerName}{nameof(FreelancerExtendedController.RemoveCertificate)}",
           areaName: "FrogsNetwork.Freelancing",
           pattern: $"{freelancerExtendedControllerName}/{nameof(FreelancerExtendedController.RemoveCertificate)}",
           defaults: new { controller = freelancerExtendedControllerName, action = nameof(FreelancerExtendedController.RemoveCertificate) });

            routes.MapAreaControllerRoute(
          name: $"{freelancerExtendedControllerName}{nameof(FreelancerExtendedController.AddEducation)}",
          areaName: "FrogsNetwork.Freelancing",
          pattern: $"{freelancerExtendedControllerName}/{nameof(FreelancerExtendedController.AddEducation)}",
          defaults: new { controller = freelancerExtendedControllerName, action = nameof(FreelancerExtendedController.AddEducation) });

            routes.MapAreaControllerRoute(
        name: $"{freelancerExtendedControllerName}{nameof(FreelancerExtendedController.RemoveEducation)}",
        areaName: "FrogsNetwork.Freelancing",
        pattern: $"{freelancerExtendedControllerName}/{nameof(FreelancerExtendedController.RemoveEducation)}",
        defaults: new { controller = freelancerExtendedControllerName, action = nameof(FreelancerExtendedController.RemoveEducation) });

            routes.MapAreaControllerRoute(
       name: $"{freelancerExtendedControllerName}{nameof(FreelancerExtendedController.AddLanguage)}",
       areaName: "FrogsNetwork.Freelancing",
       pattern: $"{freelancerExtendedControllerName}/{nameof(FreelancerExtendedController.AddLanguage)}",
       defaults: new { controller = freelancerExtendedControllerName, action = nameof(FreelancerExtendedController.AddLanguage) });


            routes.MapAreaControllerRoute(
     name: $"{freelancerExtendedControllerName}{nameof(FreelancerExtendedController.RemoveLanguage)}",
     areaName: "FrogsNetwork.Freelancing",
     pattern: $"{freelancerExtendedControllerName}/{nameof(FreelancerExtendedController.RemoveLanguage)}",
     defaults: new { controller = freelancerExtendedControllerName, action = nameof(FreelancerExtendedController.RemoveLanguage) });
            #endregion

            #endregion

            builder.UseAuthorization();

            builder.UseSession();
        }
    }
}
