using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrogsNetwork.Freelancing.Models;
using FrogsNetwork.Freelancing.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrchardCore.ContentManagement;
using OrchardCore.Users;

namespace FrogsNetwork.Freelancing.Services;
public interface IProfileService
{
    Task<bool> AddFreelancerLanguage(FreelancerLanguage freelancerLanguage);
    Task<bool> AddFreelancerNationality(FreelancerNationality freelancerNationality);
    Task<bool> AddUserProfile(IUser user);
    Task<bool> EditCompany(CompanyProfileViewModel viewModel);
    Task<bool> EditFreelancer(FreelancerProfileViewModel viewModel);
    Task<IEnumerable<SelectListItem>> GetCities(int regionId);
    Task<CompanyProfileViewModel> GetCompanyProfile(string userId);
    Task<IEnumerable<SelectListItem>> GetCountries();
    Task<IEnumerable<SelectListItem>> GetExpertise(IContentManager contentManager,
        IContentHandleManager contentHamdleManager, string parentId);
    
    Task<FreelancerDetailViewModel> GetFreelancerDetail(IContentManager contentManager, IContentHandleManager contentHandleManager, int id);
    Task<string[]> GetFreelancerExpertiseIds(int freelancerId, int levelId);
    Task<List<FreelancerLanguageViewModel>> GetFreelancerLanguages(int freelancerId);
    Task<List<FreelancerNationalityViewModel>> GetFreelancerNationalities(int freelancerId);
    Task<FreelancerProfileViewModel> GetFreelancerProfile(string userId);
    Task<string[]> GetFreelancerServiceIds(int freelancerId, int levelId);
    IEnumerable<SelectListItem> GetLanguageLevels();
    Task<IEnumerable<SelectListItem>> GetNationalities();
    Task<IEnumerable<SelectListItem>> GetRegions(int countryId);
    Task<IEnumerable<SelectListItem>> GetServices(IContentManager contentManager,
        IContentHandleManager contentHamdleManager, string parentId);
    Task<IEnumerable<SelectListItem>> GetTaxonomies(IReadOnlyList<ContentItem> contentItems, string parentItemId);
    Task<IEnumerable<SelectListItem>> GetTaxonomies(IReadOnlyList<ContentItem> contentItems, string[] contectItemIds);

    //Task<IEnumerable<TaxonomyItemViewModel>> GetTaxonomies(Taxonomies taxonomy, string parentItemId);
    Roles GetUserRole(IUser user);
    Task<bool> RemoveFreelancerLanguage(int id);
    Task<bool> RemoveFreelancerNationality(int id);
    Task<IEnumerable<FreelancerResultViewModel>> SearchFreelancers(FreelancerSearchViewModel searchViewModel);
}
