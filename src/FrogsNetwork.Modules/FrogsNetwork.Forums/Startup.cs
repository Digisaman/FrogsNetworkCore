using Fluid;
using FrogsNetwork.Forums.Drivers;
using FrogsNetwork.Forums.Indexes;
using FrogsNetwork.Forums.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using YesSql.Indexes;

namespace FrogsNetwork.Forums
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddContentPart<ForumPart>()
                .UseDisplayDriver<ForumPartDisplayDriver>();
            services.AddDataMigration<Migrations>();
            //services.AddSingleton<IIndexProvider, ForumPartIndexProvider>();
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaControllerRoute(
                name: "Home",
                areaName: "FrogsNetwork.Forums",
                pattern: "Forum/Index",
                defaults: new { controller = "Forum", action = "Index" }
            );

           

        }
    }
}
