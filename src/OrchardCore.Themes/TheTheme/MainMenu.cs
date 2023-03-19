using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using OrchardCore.ContentManagement;
using OrchardCore.Navigation;
using OrchardCore.Security.Services;
using OrchardCore.Users;
using OrchardCore.Users.Models;

namespace OrchardCore.Themes.TheTheme;

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

    public Task BuildNavigationAsync(string name, NavigationBuilder builder)
    {
        if (!String.Equals(name, "main-menu", StringComparison.OrdinalIgnoreCase))
        {
            return Task.CompletedTask;
        }


        builder
             .Add(S["Freelancer Profile"], S["FreelancerProfile"], layers => layers
            .Action("Index", "FreelancerProfile", new { area = "FrogsNetwork.Freelancing" })
            .Permission(FrogsNetwork.Freelancing.Permissions.ManageFreelancerProfile)
            .LocalNav());

        builder
             .Add(S["Company Profile"], S["ComapanyProfile"], layers => layers
            .Action("Index", "CompanyProfile", new { area = "FrogsNetwork.Freelancing" })
             .Permission(FrogsNetwork.Freelancing.Permissions.ManageCompanyProfile)
            .LocalNav());

        return Task.CompletedTask;
    }
}

