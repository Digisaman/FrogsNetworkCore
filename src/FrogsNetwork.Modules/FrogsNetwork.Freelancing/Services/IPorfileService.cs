using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrogsNetwork.Freelancing.Models;
using FrogsNetwork.Freelancing.ViewModels;
using OrchardCore.Users;

namespace FrogsNetwork.Freelancing.Services;
public interface IProfileService
{
    Task<bool> AddFreelancerNationality(FreelancerNationality freelancerNationality);
    Task<bool> AddUserProfile(IUser user);
    Task<bool> EditCompany(CompanyProfileViewModel viewModel);
    Task<bool> EditFreelancer(FreelancerProfileViewModel viewModel);
    Task<IEnumerable<City>> GetCities(int regionId);
    Task<IEnumerable<Country>> GetCountries();
    Task<List<FreelancerNationalityViewModel>> GetFreelancerNationalities(int freelancerId);
    Task<FreelancerProfileViewModel> GetFreelancerProfile(string userId);
    Task<IEnumerable<Nationality>> GetNationalities();
    Task<IEnumerable<Region>> GetRegions(int countryId);
    Roles GetUserRole(IUser user);
    Task<bool> RemoveFreelancerNationality(int id);
}
