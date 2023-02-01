using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrogsNetwork.Freelancing.ViewModels;
using Lombiq.HelpfulLibraries.OrchardCore.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.Users.Services;
using OrchardCore.Users;
using FrogsNetwork.Freelancing.Services;
using OrchardCore.Users.Models;
using FrogsNetwork.Freelancing.Helpers;

namespace FrogsNetwork.Freelancing.Controllers;

[Authorize]
public class CompanyProfileController : Controller
{

    private readonly IUserService _userService;
    private readonly ProfileService _profileService;
    //private readonly ITaxonomyService _taxonomyService;
    //private readonly IAuthenticationService _authenticationService;

    public CompanyProfileController(
        IUserService userService,
       ProfileService profileService)
    {
       
        _userService = userService;
        _profileService = profileService;
    }

    public CompanyProfileViewModel ViewModel { get; set; }


    
    public async Task<ActionResult> Index()
    {

        User user = _userService.GetAuthenticatedUserAsync(User).Result as User;
        this.ViewModel = _profileService.GetCompanyProfile(user.UserId).Result;
        this.ViewModel.Countries = _profileService.GetCountries().Result;
        if (ViewModel.CountryId != 0)
        {
            this.ViewModel.Regions = _profileService.GetRegions(this.ViewModel.CountryId).Result;
        }
        if (ViewModel.RegionId != 0)
        {
            this.ViewModel.Cities = _profileService.GetCities(this.ViewModel.RegionId).Result;
        }
        this.ViewData.Add("ViewModel", this.ViewModel);
        return View(this.ViewModel);
    }

    [HttpPost]
    public async Task<ActionResult> Index(CompanyProfileViewModel viewModel)
    {
        var user = _userService.GetAuthenticatedUserAsync(User).Result as User;
        this.ViewModel = _profileService.GetCompanyProfile(user.UserId).Result;

        this.ViewModel.UpdateModel(viewModel);
        //this.ViewModel = new CompanyProfileViewModel();
        if (!this.ViewModel.Countries.Any())
            this.ViewModel.Countries = _profileService.GetCountries().Result;

        if (ViewModel.CountryId != 0)
        {
            this.ViewModel.Regions = _profileService.GetRegions(this.ViewModel.CountryId).Result;
        }
        if (ViewModel.RegionId != 0)
        {
            this.ViewModel.Cities = _profileService.GetCities(this.ViewModel.RegionId).Result;
        }


        _profileService.EditCompany(this.ViewModel);
        this.ViewData.Add("ViewModel", this.ViewModel);
        return View(this.ViewModel);
    }


    //[Authorize]
    //[HttpPost]
    //public ActionResult UpdateProfile(int? countryId,
    //    int? regionId,
    //    int? cityId,
    //    string lat,
    //    string @long,
    //    string vat,
    //    string address,
    //    string postalCode,
    //    string companyTel,
    //    string website,
    //    string contactPersonPostion)
    //{

    //    var user = _userService.GetAuthenticatedUserAsync(User).Result as User;
    //    this.ViewModel = _profileService.GetCompanyProfile(user.UserId).Result;

    //    _profileService.EditCompany(this.ViewModel);
    //    return View(this.ViewModel);
    //}

    //[HttpPost]
    //public ActionResult GetRegions(string selectedCountryId)
    //{
    //    List<SelectListItem> regions = new List<SelectListItem>();
    //    if (!string.IsNullOrEmpty(selectedCountryId))
    //    {
    //        int countryId = Convert.ToInt32(selectedCountryId);
    //        regions = GetRegionList(countryId).ToList();
    //    }
    //    return Json(regions, JsonRequestBehavior.AllowGet);
    //}


    //[HttpPost]
    //public ActionResult GetCities(string selectedRegionId)
    //{
    //    List<SelectListItem> cities = new List<SelectListItem>();
    //    if (!string.IsNullOrEmpty(selectedRegionId))
    //    {
    //        int regionId = Convert.ToInt32(selectedRegionId);
    //        cities = GetCityList(regionId).ToList();
    //    }
    //    return Json(cities, JsonRequestBehavior.AllowGet);
    //}



    //private List<SelectListItem> GetCountries()
    //{
    //    return _userService.GetCountries().Select(c => new SelectListItem
    //    {
    //        Value = c.Id.ToString(),
    //        Text = c.Name
    //    }).ToList();
    //}

    //private List<SelectListItem> GetRegionList(int countryId)
    //{
    //    return _userService.GetRegions(countryId).Select(c => new SelectListItem
    //    {
    //        Value = c.Id.ToString(),
    //        Text = c.Name
    //    }).ToList();
    //}

    //private List<SelectListItem> GetCityList(int regionId)
    //{
    //    return _userService.GetCities(regionId).Select(c => new SelectListItem
    //    {
    //        Value = c.Id.ToString(),
    //        Text = c.Name
    //    }).ToList();
    //}

}
