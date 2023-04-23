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

        if ( _httpContext.HttpContext.User.IsInRole("Administrator"))
        {
            builder.Remove(c => c.Position == "FreelancerDashboard");
            builder.Remove(c => c.Position == "CompanyDashboard");
            return Task.CompletedTask;
        }

        #region Freelancing
        //builder
        //     .Add(S["Freelancer Profile"], S["FreelancerProfile"], layers => layers
        //    .Action("Index", "FreelancerProfile", new { area = "FrogsNetwork.Freelancing" })
        //    .Permission(FrogsNetwork.Freelancing.Permissions.ManageFreelancerProfile)
        //    .AddClass("nav-link")
        //    .LocalNav());

        //builder
        //     .Add(S["Company Profile"], S["ComapanyProfile"], layers => layers
        //    .Action("Index", "CompanyProfile", new { area = "FrogsNetwork.Freelancing" })
        //     .Permission(FrogsNetwork.Freelancing.Permissions.ManageCompanyProfile)
        //     .AddClass("nav-link")
        //    .LocalNav());
        //builder
        //     .Add(S["Search Freelancers"], S["SearchFreelancers"], layers => layers
        //    .Action("Index", "FreelancerSearch", new { area = "FrogsNetwork.Freelancing" })
        //     .Permission(FrogsNetwork.Freelancing.Permissions.ManageCompanyProfile)
        //     .AddClass("nav-link")
        //    .LocalNav());
        #endregion

        builder
             .Add(S["Freelancer Dashboard"], S["FreelancerDashboard"], layers => layers
            .Action("Index", "FreelancerDashboard", new { area = "FrogsNetwork.Freelancing" })
            .Permission(FrogsNetwork.Freelancing.Permissions.ManageFreelancerProfile)
            .AddClass("nav-link")
            .LocalNav());

        builder
             .Add(S["Company Dashboard"], S["CompanyDashboard"], layers => layers
            .Action("Index", "CompanyDashboard", new { area = "FrogsNetwork.Freelancing" })
            .Permission(FrogsNetwork.Freelancing.Permissions.ManageCompanyProfile)
            .AddClass("nav-link")
            .LocalNav());

        //#region Forum
        //builder
        //     .Add(S["Forums"], S["Forums"], layers => layers
        //    .Action("Index", "Forum", new { area = "FrogsNetwork.Forum" })
        //    .Permission(FrogsNetwork.Forum.Permissions.ManageForum)
        //    .AddClass("nav-link")
        //    .LocalNav());
        //#endregion

        return Task.CompletedTask;
    }
}

