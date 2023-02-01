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
    Task<bool> AddFreelancerNationality(FreelancerNationality freelancerNationality);
    Task<bool> AddUserProfile(IUser user);
    Task<bool> EditCompany(CompanyProfileViewModel viewModel);
    Task<bool> EditFreelancer(FreelancerProfileViewModel viewModel);
    Task<IEnumerable<SelectListItem>> GetCities(int regionId);
    Task<CompanyProfileViewModel> GetCompanyProfile(string userId);
    Task<IEnumerable<SelectListItem>> GetCountries();
    Task<List<FreelancerNationalityViewModel>> GetFreelancerNationalities(int freelancerId);
    Task<FreelancerProfileViewModel> GetFreelancerProfile(string userId);
    Task<IEnumerable<SelectListItem>> GetNationalities();
    Task<IEnumerable<SelectListItem>> GetRegions(int countryId);
    Task<IDictionary<string, string>> GetTaxonomies(IContentManager contentManager, string taxonomyName);
    Roles GetUserRole(IUser user);
    Task<bool> RemoveFreelancerNationality(int id);
}
