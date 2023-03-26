using FrogsNetwork.Forum.Controllers;
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

namespace FrogsNetwork.Forum;
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
        //services.AddScoped<INavigationProvider, MainMenu>();
        services.AddScoped<IPermissionProvider, Permissions>();
        services.AddDataMigration<Migrations>();
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        //services.AddTransient<UserService>();
        //services.AddTransient<ProfileService>();
        services.AddMvc()
        .AddSessionStateTempDataProvider();
        services.AddSession();

    }

    public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        #region Routes
        var forumControllerName = typeof(ForumController).ControllerName();
        routes.MapAreaControllerRoute(
          name: $"{forumControllerName}{nameof(ForumController.Index)}",
          areaName: "FrogsNetwork.Forum",
          pattern: $"{forumControllerName}/{nameof(ForumController.Index)}",
          defaults: new { controller = forumControllerName, action = nameof(ForumController.Index) });
        #endregion


        builder.UseAuthorization();

        builder.UseSession();
    }
}
