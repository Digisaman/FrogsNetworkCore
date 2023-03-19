using System.Net;
using FrogsNetwork.Freelancing.Helpers;
using FrogsNetwork.Freelancing.Services;
using FrogsNetwork.Freelancing.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OrchardCore.Users.Models;
using OrchardCore.Users.Services;

namespace FrogsNetwork.Freelancing.Controllers;

[Authorize]
public class FreelancerProfileController : Controller
{

    private readonly IUserService _userService;
    private readonly ProfileService _profileService;
    private readonly IAuthenticationService _authenticationService;
    private readonly IAuthorizationService _authorizationService;

    public FreelancerProfileController(
        IUserService userService,
        ProfileService profileService,
        IAuthenticationService authenticationService,
        IAuthorizationService authorizationService)
    {
        _userService = userService;
        _profileService = profileService;
        _authenticationService = authenticationService;
        _authorizationService = authorizationService;
        ViewModel = new FreelancerProfileViewModel();
    }

    public FreelancerProfileViewModel ViewModel { get; set; }



    public async Task<IActionResult> Index()
    {
        try
        {
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.ManageFreelancerProfile))
            {
                return Unauthorized();
            }


            var user = _userService.GetAuthenticatedUserAsync(User).Result as User;

            this.ViewModel = _profileService.GetFreelancerProfile(user.UserId).Result;




            this.ViewModel.Nationalities = _profileService.GetNationalities().Result;
            this.ViewModel.Countries = _profileService.GetCountries().Result;
            this.ViewModel.Regions = new List<SelectListItem>().AsEnumerable();
            this.ViewModel.Cities = new List<SelectListItem>().AsEnumerable();
            if (ViewModel.CountryId != 0)
            {
                this.ViewModel.Regions = _profileService.GetRegions(this.ViewModel.CountryId).Result;
            }
            if (ViewModel.RegionId != 0)
            {
                this.ViewModel.Cities = _profileService.GetCities(this.ViewModel.RegionId).Result;
            }

            this.ViewModel.FreelancerNationalities = _profileService.GetFreelancerNationalities(this.ViewModel.Id).Result;



            if (string.IsNullOrEmpty(this.ViewModel.Lat) || string.IsNullOrEmpty(this.ViewModel.Long))
            {
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

                        //json - formatted string from maps api
                        string responseFromServer = reader.ReadToEnd();
                        //var jsonResponse = JsonConvert.DeserializeObject(responseFromServer);
                        dynamic jsonResponse = JsonConvert.DeserializeObject(responseFromServer);
                        if (jsonResponse.results.Count != 0)
                        {
                            this.ViewModel.Lat = jsonResponse.results[0].geometry.location.lat.ToString();
                            this.ViewModel.Long = jsonResponse.results[0].geometry.location.lng.ToString();
                        }
                    }
                    //response.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("GeoLocation Error", ex);
                }
                #endregion
            }



            this.ViewData.Add("ViewModel", this.ViewModel);
            return View(this.ViewModel);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    [HttpPost]
    public async Task<ActionResult> Index(FreelancerProfileViewModel viewModel)
    {
        try
        {
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.ManageFreelancerProfile))
            {
                return Unauthorized();
            }


            var user = _userService.GetAuthenticatedUserAsync(User).Result as User;
            this.ViewModel = _profileService.GetFreelancerProfile(user.UserId).Result;

            this.ViewModel.UpdateModel(viewModel);


            this.ViewModel.Countries = _profileService.GetCountries().Result;

            this.ViewModel.Nationalities = _profileService.GetNationalities().Result;

            //if (this.ViewModel.SelectedNationalityId != 0)
            //{
            //    _userService.AddFreelancernationality(new Models.FreelancerNationality
            //    {
            //        FreelancerId = this.ViewModel.Id,
            //        NationalityId = this.ViewModel.SelectedNationalityId.Value
            //    });
            //}

            this.ViewModel.FreelancerNationalities = _profileService.GetFreelancerNationalities(this.ViewModel.Id).Result;


            if (this.ViewModel.CountryId != 0)
            {
                this.ViewModel.Regions = _profileService.GetRegions(this.ViewModel.CountryId).Result;

                if (this.ViewModel.RegionId != 0)
                {
                    this.ViewModel.Cities = _profileService.GetCities(this.ViewModel.RegionId).Result;
                }
            }

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

            _profileService.EditFreelancer(this.ViewModel);
            this.ViewData.Add("ViewModel", this.ViewModel);


            //this.Redirect("~/Users/FreelancerExtended");
            return View(this.ViewModel);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [HttpPost]
    public ActionResult AddNationality(FreelancerProfileViewModel viewModel)
    {
        try
        {

            var user = _userService.GetAuthenticatedUserAsync(User).Result as User;
            this.ViewModel = _profileService.GetFreelancerProfile(user.UserId).Result;
            this.ViewModel.UpdateModel(viewModel);
            if (this.ViewModel.SelectedNationalityId.HasValue && this.ViewModel.SelectedNationalityId.Value != 0)
            {
                //this.ViewModel.SelectedNationalityId = selectedNationalityId.Value;
                _profileService.AddFreelancerNationality(new Models.FreelancerNationality
                {
                    FreelancerId = this.ViewModel.Id,
                    NationalityId = this.ViewModel.SelectedNationalityId.Value
                });
            }
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public ActionResult RemoveNationality(int id)
    {
        try
        {
            _profileService.RemoveFreelancerNationality(id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //#region Lookups
    //private IEnumerable<SelectListItem> GetNationalities()
    //{
    //    return _profileService.GetNationalities().Result.Select(c => new SelectListItem
    //    {
    //        Value = c.Id.ToString(),
    //        Text = c.Name
    //    }).ToList();
    //}

    //private List<SelectListItem> GetCountries()
    //{
    //    return _profileService.GetCountries().Result.Select(c => new SelectListItem
    //    {
    //        Value = c.Id.ToString(),
    //        Text = c.Name
    //    }).ToList();
    //}

    //private List<SelectListItem> GetRegionList(int countryId)
    //{
    //    return _profileService.GetRegions(countryId).Result.Select(c => new SelectListItem
    //    {
    //        Value = c.Id.ToString(),
    //        Text = c.Name
    //    }).ToList();
    //}

    //private List<SelectListItem> GetCityList(int regionId)
    //{
    //    return _profileService.GetCities(regionId).Result.Select(c => new SelectListItem
    //    {
    //        Value = c.Id.ToString(),
    //        Text = c.Name
    //    }).ToList();
    //}
    //#endregion
}
