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
using OrchardCore.Users.Models;

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

    //public Task BuildNavigationAsync(string name, NavigationBuilder builder)
    //{
    //    if (!String.Equals(name, "main", StringComparison.OrdinalIgnoreCase))
    //    {
    //        return Task.CompletedTask;
    //    }
    //    if (_httpContext.HttpContext.User.Identity.IsAuthenticated)
    //    {
    //        if (_httpContext.HttpContext.User.IsInRole("Freelancer"))
    //        {
    //            builder
    //                 .Add(S["Freelancer Profile"], S["FreelancerProfile"], layers => layers
    //                .Action("Index", "FreelancerProfile")
    //                .LocalNav());
    //        }
    //        else if (_httpContext.HttpContext.User.IsInRole("Company"))
    //        {
    //            builder
    //                 .Add(S["CompanyProfile"], S["Comapany Profile"], layers => layers
    //                .Action("Index", "CompanyProfile")
    //                .LocalNav());
    //        }
    //    }

    //    return Task.CompletedTask;
    //}

    public Task BuildNavigationAsync(string name, NavigationBuilder builder)
    {
        if (!String.Equals(name, "main-menu", StringComparison.OrdinalIgnoreCase))
        {
            return Task.CompletedTask;
        }

        if (_httpContext.HttpContext.User.IsInRole("Administrator"))
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
//public class NavigationFilter : INavigationFilter
//{
//    private readonly IContentManager _contentManager;
//    private readonly IRoleService _roleService;
//    private readonly IAuthenticationService _authenticationService;

//    public NavigationFilter(
//        IContentManager contentManager,
//        IRoleService roleService,
//        IAuthenticationService authenticationService)
//    {
//        _contentManager = contentManager;
//        _roleService = roleService;
//        _authenticationService = authenticationService;
//    }

//    public IEnumerable<MenuItem> Filter(IEnumerable<MenuItem> items)
//    {
//        IUser user = _authenticationService.GetAuthenticatedUser();
//        List<MenuItem> filteredItems = new List<MenuItem>();
//        if (user != null)
//        {
//            #region Redirect Based On Role
//            string userType = "";
//            if (_roleService.IsUserInRole(user.Id, "Freelancer"))
//            {
//                userType = "Freelancer";
//            }
//            else if (_roleService.IsUserInRole(user.Id, "Company"))
//            {
//                userType = "Company";
//            }
//            else if (_roleService.IsUserInRole(user.Id, "Administrator"))
//            {
//                return items;
//            }
//            #endregion

//            foreach (var item in items)
//            {

//                if (userType == "Freelancer")
//                {
//                    switch (item.Url)
//                    {
//                        case "~/Users/FreelancerProfile":
//                            filteredItems.Add(item);
//                            break;
//                        case "~/Home":
//                            filteredItems.Add(item);
//                            break;
//                        case "~/forums":
//                            filteredItems.Add(item);
//                            break;

//                    }
//                }
//                else if (userType == "Company")
//                {
//                    switch (item.Url)
//                    {
//                        case "~/Users/CompanyProfile":
//                            filteredItems.Add(item);
//                            break;
//                        case "~/Users/FreelancerSearch":
//                            filteredItems.Add(item);
//                            break;
//                        case "~/Home":
//                            filteredItems.Add(item);
//                            break;
//                    }
//                }

//            }
//            return filteredItems;
//        }
//        else
//        {
//            foreach (var item in items)
//            {

//                switch (item.Url)
//                {
//                    //case "~/Users/Account/Logon":
//                    //    filteredItems.Add(item);
//                    //    break;
//                    case "~/Home":
//                        filteredItems.Add(item);
//                        break;
//                    case "~/forums":
//                        filteredItems.Add(item);
//                        break;
//                }
//            }
//        }
//        return filteredItems;
//    }
//}
