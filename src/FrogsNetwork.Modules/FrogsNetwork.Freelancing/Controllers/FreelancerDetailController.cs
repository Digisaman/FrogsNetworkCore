using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FrogsNetwork.Freelancing.ViewModels;
using Lombiq.HelpfulLibraries.OrchardCore.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrchardCore.Users.Services;
using OrchardCore.Users;
using OrchardCore.Users.Models;
using FrogsNetwork.Freelancing.Services;
using FrogsNetwork.Freelancing.Helpers;
using OrchardCore.ContentManagement;
using FrogsNetwork.Freelancing.Models;

namespace FrogsNetwork.Freelancing.Controllers;

[Authorize]
public class FreelancerDetailController : Controller
{
    private readonly IUserService _userService;
    private readonly IContentManager _contentManager;
    private readonly IContentHandleManager _contentHandleManager;
    private readonly ProfileService _profileService;

    public FreelancerDetailController(
        IUserService userService,
        ProfileService profileService,
        IContentManager contentManager,
        IContentHandleManager contentHandleManager)
    {
        _userService = userService;
        _profileService = profileService;
        _contentManager = contentManager;
        _contentHandleManager = contentHandleManager;
    }

    public FreelancerDetailViewModel ViewModel { get; set; }

    public async Task<ActionResult> Index(int id)
    {
        this.ViewModel = new FreelancerDetailViewModel();
        if (id != 0)
        {


            var expertise = await _contentManager.GetTaxonomyTermsAsync(_contentHandleManager, nameof(Taxonomies.Expertise));

            var services = await _contentManager.GetTaxonomyTermsAsync(_contentHandleManager, nameof(Taxonomies.Services));

            this.ViewModel = _profileService.GetFreelancerDetail(_contentManager, _contentHandleManager, id).Result;
        }

        this.ViewData.Add("ViewModel", this.ViewModel);
        return View(this.ViewModel);
    }

    public async Task<RedirectToActionResult> NavigateBack()
    {
        var user = _userService.GetAuthenticatedUserAsync(User).Result as User;
        Roles role = _profileService.GetUserRole(user);
        if (role == Roles.Freelancer)
            return this.RedirectToAction("Index", "FreelancerExtended", new { area = "FrogsNetwork.Freelancing" });
        else if (role == Roles.Company)
            return this.RedirectToAction("Index", "FreelancerSearch", new { area = "FrogsNetwork.Freelancing" });
        else
            return this.RedirectToAction("Index");
    }


}
