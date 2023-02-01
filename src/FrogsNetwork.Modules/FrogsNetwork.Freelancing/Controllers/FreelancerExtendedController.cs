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
using OrchardCore.DisplayManagement.Manifest;
using FrogsNetwork.Freelancing.Models;
using OrchardCore.ContentManagement;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FrogsNetwork.Freelancing.Controllers;


[Authorize]
public class FreelancerExtendedController : Controller
{

    private readonly IUserService _userService;
    //private readonly IOrchardServices _orchardServices;
    private readonly IContentManager _contentManager;
    private readonly IContentHandleManager _contentHandleManager;
    private readonly ProfileService _profileService;
    

    //private readonly IAuthenticationService _authenticationService;

    public FreelancerExtendedController(
        IUserService userService,
        ProfileService profileService,
        //IOrchardServices orchardServices,
        //IAuthenticationService authenticationService,
        IContentManager contentManager,
        IContentHandleManager contentHandleManager
        )
    {
        _profileService= profileService;
        _contentManager = contentManager;
        _contentHandleManager = contentHandleManager;
        //_authenticationService = authenticationService;
        _userService = userService;
        //_orchardServices = orchardServices;
      
    }

    public FreelancerExpertiseViewModel ViewModel { get; set; }


   
    public async Task<ActionResult> Index()
    {
        try
        {
            var user = _userService.GetAuthenticatedUserAsync(User).Result as User;

            var expertise = await _contentManager.GetTaxonomyTermsAsync(_contentHandleManager, nameof(Taxonomies.Expertise));

            var services = await _contentManager.GetTaxonomyTermsAsync(_contentHandleManager, nameof(Taxonomies.Services));

            FreelancerProfileViewModel freelancerUser = _profileService.GetFreelancerProfile(user.UserId).Result;


            this.ViewModel = new FreelancerExpertiseViewModel
            {
                Id = freelancerUser.Id
            };
            this.ViewModel.ExpertiseFirstIds = _profileService.GetFreelancerExpertiseIds(freelancerUser.Id, 1).Result;
            this.ViewModel.ExpertiseSecondIds = _profileService.GetFreelancerExpertiseIds(freelancerUser.Id, 2).Result;
            this.ViewModel.ServicesFirstIds = _profileService.GetFreelancerServiceIds(freelancerUser.Id, 1).Result;
            this.ViewModel.ServicesSecondIds = _profileService.GetFreelancerServiceIds(freelancerUser.Id, 2).Result;

            #region Lookups
            this.ViewModel.ExpertiseFirst = _profileService.GetTaxonomies(expertise).Result;
            this.ViewModel.ServicesFirst = _profileService.GetTaxonomies(services).Result;
            

            this.ViewModel.Countries = _profileService.GetCountries().Result;

            this.ViewModel.Languages = _profileService.GetLanguages().Result;
            this.ViewModel.LanguageLevels = _profileService.GetLanguageLevels();
            ViewModel.FreelancerLanguages = _profileService.GetFreelancerLanguages(freelancerUser.Id).Result;
            this.ViewModel.FreelancerCertificates = _profileService.GetFreelancerCertificate(freelancerUser.Id).Result;
            this.ViewModel.FreelancerEducations = _profileService.GetFreelancerEducation(freelancerUser.Id).Result;
            #endregion


            if (this.ViewModel.ExpertiseFirstIds != null && this.ViewModel.ExpertiseFirstIds.Any())
            {
                //_ProfileService.SaveFreelancerExpertiseIds(this.ViewModel.Id, 1, this.ViewModel.ExpertiseFirstIds);

                List<SelectListItem> level2List = new List<SelectListItem>();
                foreach (var firstLevelId in ViewModel.ExpertiseFirstIds)
                {
                    SelectListItem firstLevelItem = this.ViewModel.ExpertiseFirst.FirstOrDefault(c => c.Value == firstLevelId);
                    level2List.AddRange(_profileService.GetTaxonomies(expertise, firstLevelId).Result);
                }
                this.ViewModel.ExpertiseSecond = level2List;
            }

            if (this.ViewModel.ServicesFirstIds != null && this.ViewModel.ServicesFirstIds.Any())
            {
                //_ProfileService.SaveFreelancerServiceIds(this.ViewModel.Id, 1, this.ViewModel.ServicesFirstIds);

                List<SelectListItem> level2List = new List<SelectListItem>();
                foreach (var firstLevelId in ViewModel.ServicesFirstIds)
                {
                    SelectListItem firstLevelItem = this.ViewModel.ServicesFirst.FirstOrDefault(c => c.Value == firstLevelId);
                    level2List.AddRange(_profileService.GetTaxonomies(services, firstLevelId).Result);
                }
                this.ViewModel.ServicesSecond = level2List;
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
    public async Task<ActionResult> Index(
        FreelancerExpertiseViewModel viewModel)
    {
        try
        {
            var user = _userService.GetAuthenticatedUserAsync(User).Result as User;

            var expertise = await _contentManager.GetTaxonomyTermsAsync(_contentHandleManager, nameof(Taxonomies.Expertise));

            var services = await _contentManager.GetTaxonomyTermsAsync(_contentHandleManager, nameof(Taxonomies.Services));

            FreelancerProfileViewModel freelancerUser = _profileService.GetFreelancerProfile(user.UserId).Result;

            this.ViewModel = new FreelancerExpertiseViewModel
            {
                Id = freelancerUser.Id
            };
            this.ViewModel.UpdateModel(viewModel);

            this.ViewModel.ExpertiseFirstIds = viewModel.ExpertiseFirstIds != null ? viewModel.ExpertiseFirstIds : _profileService.GetFreelancerExpertiseIds(freelancerUser.Id, 1).Result;
            this.ViewModel.ExpertiseSecondIds = viewModel.ExpertiseSecondIds != null ? viewModel.ExpertiseSecondIds : _profileService.GetFreelancerExpertiseIds(freelancerUser.Id, 2).Result;
            this.ViewModel.ServicesFirstIds = viewModel.ServicesFirstIds != null ? viewModel.ServicesFirstIds : _profileService.GetFreelancerServiceIds(freelancerUser.Id, 1).Result;
            this.ViewModel.ServicesSecondIds = viewModel.ServicesSecondIds != null ? viewModel.ServicesSecondIds : _profileService.GetFreelancerServiceIds(freelancerUser.Id, 2).Result;

            #region Lookups
            this.ViewModel.ExpertiseFirst = _profileService.GetTaxonomies(expertise).Result;
            this.ViewModel.ServicesFirst = _profileService.GetTaxonomies(services).Result;
            this.ViewModel.Countries = _profileService.GetCountries().Result;

            this.ViewModel.Languages = _profileService.GetLanguages().Result;
            this.ViewModel.LanguageLevels = _profileService.GetLanguageLevels();
            this.ViewModel.FreelancerLanguages = _profileService.GetFreelancerLanguages(freelancerUser.Id).Result;
            this.ViewModel.FreelancerCertificates = _profileService.GetFreelancerCertificate(freelancerUser.Id).Result;
            this.ViewModel.FreelancerEducations = _profileService.GetFreelancerEducation(freelancerUser.Id).Result;
            #endregion


            if (this.ViewModel.ExpertiseFirstIds != null && this.ViewModel.ExpertiseFirstIds.Any())
            {
                _profileService.SaveFreelancerExpertiseIds(this.ViewModel.Id, 1, this.ViewModel.ExpertiseFirstIds);
                List<SelectListItem> level2List = new List<SelectListItem>();
                foreach (var firstLevelId in this.ViewModel.ExpertiseFirstIds)
                {
                    SelectListItem firstLevelItem = this.ViewModel.ExpertiseFirst.FirstOrDefault(c => c.Value == firstLevelId.ToString());
                    level2List.AddRange(_profileService.GetTaxonomies(expertise, firstLevelId).Result);
                }
                this.ViewModel.ExpertiseSecond = level2List;

                if (this.ViewModel.ExpertiseSecondIds != null && this.ViewModel.ExpertiseSecondIds.Any())
                {
                    _profileService.SaveFreelancerExpertiseIds(this.ViewModel.Id, 2, this.ViewModel.ExpertiseSecondIds);

                    //List<SelectListItem> level3List = new List<SelectListItem>();
                    //foreach (var secondLevelId in ExpertiseSecondIds)
                    //{
                    //    level2List.AddRange(GetExpertise(secondLevelId));
                    //}
                }
            }


            if (this.ViewModel.ServicesFirstIds != null && this.ViewModel.ServicesFirstIds.Any())
            {
                _profileService.SaveFreelancerServiceIds(this.ViewModel.Id, 1, this.ViewModel.ServicesFirstIds);
                List<SelectListItem> level2List = new List<SelectListItem>();
                foreach (var firstLevelId in this.ViewModel.ServicesFirstIds)
                {
                    SelectListItem firstLevelItem = this.ViewModel.ServicesFirst.FirstOrDefault(c => c.Value == firstLevelId.ToString());
                    level2List.AddRange(_profileService.GetTaxonomies(services, firstLevelId).Result);
                }
                this.ViewModel.ServicesSecond = level2List;

                if (this.ViewModel.ServicesSecondIds != null && this.ViewModel.ServicesSecondIds.Any())
                {
                    _profileService.SaveFreelancerServiceIds(this.ViewModel.Id, 2, this.ViewModel.ServicesSecondIds);

                    //List<SelectListItem> level3List = new List<SelectListItem>();
                    //foreach (var secondLevelId in ServicesSecondIds)
                    //{
                    //    level2List.AddRange(GetServices(secondLevelId));
                    //}
                }
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
    public ActionResult AddEducation(FreelancerExpertiseViewModel viewModel)
    {
        try
        {
            var user = _userService.GetAuthenticatedUserAsync(User).Result as User;

            FreelancerProfileViewModel freelancerUser = _profileService.GetFreelancerProfile(user.UserId).Result;

            this.ViewModel = new FreelancerExpertiseViewModel
            {
                Id = freelancerUser.Id
            };
            this.ViewModel.UpdateModel(viewModel);

            if (!string.IsNullOrEmpty(this.ViewModel.EducationSchool) && !string.IsNullOrEmpty(this.ViewModel.EducationDegree))
            {
                _profileService.AddFreelancerEducation(new FreelancerEducation
                {
                    FreelancerId = freelancerUser.Id,
                    City = ViewModel.EducationCity,
                    CountryId = ViewModel.EducationCountryId,
                    Degree = ViewModel.EducationDegree,
                    EndYear = ViewModel.EducationEndYear,
                    Field = ViewModel.EducationField,
                    School = ViewModel.EducationSchool
                });
            }
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [HttpPost]
    public ActionResult AddCertificate(FreelancerExpertiseViewModel viewModel)
    {
        try
        {
            var user = _userService.GetAuthenticatedUserAsync(User).Result as User;

            FreelancerProfileViewModel freelancerUser = _profileService.GetFreelancerProfile(user.UserId).Result;

            this.ViewModel = new FreelancerExpertiseViewModel
            {
                Id = freelancerUser.Id
            };
            this.ViewModel.UpdateModel(viewModel);

            if (!string.IsNullOrEmpty(this.ViewModel.CertificateTitle) && !string.IsNullOrEmpty(this.ViewModel.CertificateOrganization))
            {
                _profileService.AddFreelancerCertificate(new FreelancerCertificate
                {
                    FreelancerId = freelancerUser.Id,
                    Certificate = this.ViewModel.CertificateTitle,
                    Organization = this.ViewModel.CertificateOrganization,
                    Description = this.ViewModel.CertificateDesctiption
                });
            }


            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [HttpPost]
    public ActionResult AddLanguage(FreelancerExpertiseViewModel viewModel)
    {
        try
        {
            var user = _userService.GetAuthenticatedUserAsync(User).Result as User;

            FreelancerProfileViewModel freelancerUser = _profileService.GetFreelancerProfile(user.UserId).Result;

            this.ViewModel = new FreelancerExpertiseViewModel
            {
                Id = freelancerUser.Id
            };
            this.ViewModel.UpdateModel(viewModel);

            if (this.ViewModel.SelectedLanguageId != 0 && this.ViewModel.SelectedLevelId != 0)
            {
                _profileService.AddFreelancerLanguage(new FreelancerLanguage
                {
                    FreelancerId = freelancerUser.Id,
                    LanguageId = this.ViewModel.SelectedLanguageId,
                    Level = Enum.Parse<LanguageLevel>( this.ViewModel.SelectedLevelId.ToString())
                });
            }

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public ActionResult RemoveCertificate(int id)
    {
        try
        {
            _profileService.RemoveFreelancerCertificate(id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public ActionResult RemoveLanguage(int id)
    {
        try
        {
            _profileService.RemoveFreelancerLanguage(id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public ActionResult RemoveEducation(int id)
    {
        try
        {
            _profileService.RemoveFreelancerEducation(id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<ActionResult> PreviewProfile()
    {
        try
        {
            var user = _userService.GetAuthenticatedUserAsync(User).Result as User;
            FreelancerProfileViewModel freelancerUser = _profileService.GetFreelancerProfile(user.UserId).Result;

            return this.Redirect(string.Format("/Users/FreelancerDetail/{0}", freelancerUser.Id));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

  
}
