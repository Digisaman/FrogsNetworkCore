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
using FrogsNetwork.Freelancing.Services;
using OrchardCore.ContentManagement;
using OrchardCore.Users.Models;
using FrogsNetwork.Freelancing.Models;
using FrogsNetwork.Freelancing.Helpers;

namespace FrogsNetwork.Freelancing.Controllers;

[Authorize]
public class FreelancerSearchController : Controller
{

    private readonly IUserService _userService;
    private readonly IContentManager _contentManager;
    private readonly IContentHandleManager _contentHandleManager;
    private readonly ProfileService _profileService;

    public FreelancerSearchController(
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

    public FreelancerSearchViewModel ViewModel { get; set; }

    public async Task<ActionResult> Index()
    {
        var user = _userService.GetAuthenticatedUserAsync(User).Result as User;

        var expertise = await _contentManager.GetTaxonomyTermsAsync(_contentHandleManager, nameof(Taxonomies.Expertise));

        var services = await _contentManager.GetTaxonomyTermsAsync(_contentHandleManager, nameof(Taxonomies.Services));


        this.ViewModel = new FreelancerSearchViewModel();
        this.ViewModel.ExpertiseFirst = _profileService.GetTaxonomies(expertise).Result;
        this.ViewModel.ServicesFirst = _profileService.GetTaxonomies(services).Result;


        this.ViewModel.Countries = _profileService.GetCountries().Result;

        this.ViewData.Add("ViewModel", this.ViewModel);
        return View(this.ViewModel);
    }

    [HttpPost]
    public async Task<ActionResult> Index(FreelancerSearchViewModel viewModel)
    {
        var user = _userService.GetAuthenticatedUserAsync(User).Result as User;

        var expertise = await _contentManager.GetTaxonomyTermsAsync(_contentHandleManager, nameof(Taxonomies.Expertise));

        var services = await _contentManager.GetTaxonomyTermsAsync(_contentHandleManager, nameof(Taxonomies.Services));

        //this.ViewModel = new FreelancerSearchViewModel();
        //this.ViewModel.UpdateModel(viewModel);

        this.ViewModel = viewModel;

        //this.ViewModel.ExpertiseFirstIds = viewModel.ExpertiseFirstIds != null ? viewModel.ExpertiseFirstIds : _profileService.GetFreelancerExpertiseIds(freelancerUser.Id, 1).Result;
        //this.ViewModel.ExpertiseSecondIds = viewModel.ExpertiseSecondIds != null ? viewModel.ExpertiseSecondIds : _profileService.GetFreelancerExpertiseIds(freelancerUser.Id, 2).Result;
        //this.ViewModel.ServicesFirstIds = viewModel.ServicesFirstIds != null ? viewModel.ServicesFirstIds : _profileService.GetFreelancerServiceIds(freelancerUser.Id, 1).Result;
        //this.ViewModel.ServicesSecondIds = viewModel.ServicesSecondIds != null ? viewModel.ServicesSecondIds : _profileService.GetFreelancerServiceIds(freelancerUser.Id, 2).Result;


        #region Lookups
        this.ViewModel.ExpertiseFirst = _profileService.GetTaxonomies(expertise).Result;
        this.ViewModel.ServicesFirst = _profileService.GetTaxonomies(services).Result;
        this.ViewModel.Countries = _profileService.GetCountries().Result;

        #endregion
        #region Location
        //if (!this.ViewModel.Countries.Any())
        //    this.ViewModel.Countries = GetCountries();

        if (this.ViewModel.CountryId.HasValue)
        {
           
            this.ViewModel.Regions = _profileService.GetRegions(this.ViewModel.CountryId.Value).Result;

            if (this.ViewModel.RegionId.HasValue)
            {
                this.ViewModel.Cities = _profileService.GetCities(this.ViewModel.RegionId.Value).Result;
            }
        }
        #endregion

        #region Expertise
        if (this.ViewModel.ExpertiseFirstIds != null && this.ViewModel.ExpertiseFirstIds.Any())
        {
            List<SelectListItem> level2List = new List<SelectListItem>();
            foreach (var firstLevelId in this.ViewModel.ExpertiseFirstIds)
            {
                SelectListItem firstLevelItem = this.ViewModel.ExpertiseFirst.FirstOrDefault(c => c.Value == firstLevelId.ToString());
                level2List.AddRange(_profileService.GetTaxonomies(expertise, firstLevelId).Result);
            }
            this.ViewModel.ExpertiseSecond = level2List;
        }
        #endregion

        #region Services
        if (this.ViewModel.ServicesFirstIds != null && this.ViewModel.ServicesFirstIds.Any())
        {
            List<SelectListItem> level2List = new List<SelectListItem>();
            foreach (var firstLevelId in this.ViewModel.ServicesFirstIds)
            {
                SelectListItem firstLevelItem = this.ViewModel.ServicesFirst.FirstOrDefault(c => c.Value == firstLevelId.ToString());
                level2List.AddRange(_profileService.GetTaxonomies(services, firstLevelId).Result);
            }
            this.ViewModel.ServicesSecond = level2List;

        }
        #endregion


        #region GeoLocation
        try
        {
            string address = "";
            if (!string.IsNullOrEmpty(this.ViewModel.Address))
            {
                address = this.ViewModel.Address.Replace(" ", "+");

                address += "+";

                if (this.ViewModel.CityId != 0)
                {
                    SelectListItem selectListItem = this.ViewModel.Cities.FirstOrDefault(c => c.Value == this.ViewModel.CityId.ToString());
                    if (selectListItem != null)
                        address += string.Format("{0}+", selectListItem.Text);
                }

                if (this.ViewModel.RegionId != 0)
                {
                    SelectListItem selectListItem = this.ViewModel.Regions.FirstOrDefault(c => c.Value == this.ViewModel.RegionId.ToString());
                    if (selectListItem != null)
                        address += string.Format("{0}+", selectListItem.Text);
                }

                if (this.ViewModel.CountryId != 0)
                {
                    SelectListItem selectListItem = this.ViewModel.Countries.FirstOrDefault(c => c.Value == this.ViewModel.CountryId.ToString());
                    if (selectListItem != null)
                        address += string.Format("{0}+", selectListItem.Text);
                }



                string url = string.Format("{0}?address={1}&key={2}", "https://maps.googleapis.com/maps/api/geocode/json", address, "AIzaSyAW1SgI7RCtbjx3t5yUIfjiDTW6fvn50OA");

                WebRequest request = WebRequest.Create(url);

                WebResponse response = request.GetResponse();

                Stream data = response.GetResponseStream();

                StreamReader reader = new StreamReader(data);

                // json-formatted string from maps api
                string responseFromServer = reader.ReadToEnd();
                //var jsonResponse = JsonConvert.DeserializeObject(responseFromServer);
                dynamic jsonResponse = JsonConvert.DeserializeObject(responseFromServer);
                if (jsonResponse.results.Count != 0)
                {
                    this.ViewModel.Lat = jsonResponse.results[0].geometry.location.lat.ToString();
                    this.ViewModel.Long = jsonResponse.results[0].geometry.location.lng.ToString();
                }

                response.Close();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("GeoLocation Error", ex);
        }
        #endregion

        this.ViewModel.Freelancers = SearchFreelancers(this.ViewModel);

        this.ViewData.Add("ViewModel", this.ViewModel);
        return View(this.ViewModel);
    }

    private IEnumerable<FreelancerResultViewModel> SearchFreelancers(FreelancerSearchViewModel searchViewModel)
    {
        List<FreelancerResultViewModel> list = new List<FreelancerResultViewModel>();
        if (searchViewModel != null && searchViewModel.AnyFilterSelected())
            list.AddRange(_profileService.SearchFreelancers(_contentManager, _contentHandleManager, searchViewModel).Result);

        return list;
    }



}
