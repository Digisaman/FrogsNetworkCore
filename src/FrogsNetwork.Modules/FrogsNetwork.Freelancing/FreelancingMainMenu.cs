using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OrchardCore.ContentManagement;
using OrchardCore.Navigation;
using OrchardCore.Security.Services;
using OrchardCore.Users;

namespace FrogsNetwork.Freelancing;

public class MainMenu : INavigationProvider
{
    private readonly IStringLocalizer S;
    private readonly IHttpContextAccessor _httpContext;

    public MainMenu(IStringLocalizer<MainMenu> localizer,
        IHttpContextAccessor httpContext)
    {
        S = localizer;
        _httpContext = httpContext;
    }

    //public async Task BuildNavigation(string name, NavigationBuilder builder)
    //{
    //    //Only interact with the "main" navigation menu here.
    //    if (!String.Equals(name, "main", StringComparison.OrdinalIgnoreCase))
    //    {
    //        return;
    //    }

    //    builder
    //        .Add(S["Notifications"], S["Notifications"], layers => layers
    //            .Action("Index", "Template", new { area = "CRT.Client.OrchardModules.CommunicationTemplates", groupId = 1 })
    //            .LocalNav()
    //        );
    //}

    public Task BuildNavigationAsync(string name, NavigationBuilder builder)
    {
        if (!String.Equals(name, "main-menu", StringComparison.OrdinalIgnoreCase))
        {
            return Task.CompletedTask;
        }

        if (_httpContext.HttpContext.User.IsInRole("Freelancer"))
        {
            builder
                 .Add(S["Freelancer Profile"], S["FreelancerProfile"], layers => layers
                .Action("Index", "FreelancerProfile", new { area = "FrogsNetwork.Freelancing" })
                .LocalNav());
        }
        else if (_httpContext.HttpContext.User.IsInRole("Company"))
        {
            builder
                 .Add(S["Company Profile"], S["ComapanyProfile"], layers => layers
                .Action("Index", "CompanyProfile", new { area = "FrogsNetwork.Freelancing" })
                .LocalNav());
        }
        return Task.CompletedTask;
    }
}

