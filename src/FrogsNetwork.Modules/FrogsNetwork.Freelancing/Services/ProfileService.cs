using FrogsNetwork.Freelancing.Models;
using FrogsNetwork.Freelancing.ViewModels;
using LinqToDB;
using Lombiq.HelpfulLibraries.LinqToDb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrchardCore.ContentManagement;
using OrchardCore.Users;
using OrchardCore.Users.Models;
using OrchardCore.Users.Services;
using ISession = YesSql.ISession;

namespace FrogsNetwork.Freelancing.Services;
public class ProfileService : IProfileService
{
    private readonly IUserService _userService;
    private readonly ISession _session;

    public ProfileService(
        IUserService userService,
        ISession session)
    {
        _userService = userService;
        _session = session;
    }

    public async Task<bool> AddUserProfile(IUser user)
    {

        var userInfo = _userService.GetUserAsync(user.UserName).Result as User;
        
       
        if (userInfo.RoleNames.Contains(nameof(Roles.Freelancer)))
        {
            bool result = AddFreelancerAsync(userInfo.UserId).Result;
            return result;
        }
        else if (userInfo.RoleNames.Contains(nameof(Roles.Company)))
        {
            bool result = AddCompanyAsync(userInfo.UserId).Result;
            return result;
        }
        else
        {
            //log
            return false;

        }
    }

    public Roles GetUserRole(IUser user)
    {
        var userInfo = _userService.GetUserAsync(user.UserName).Result as User;
        //UserInfo userInfo = await _userService.GetUserInfor(user);
        //{

        if (userInfo.RoleNames.Contains(nameof(Roles.Freelancer)))
        {
            return Roles.Freelancer;
        }
        else if (userInfo.RoleNames.Contains(nameof(Roles.Company)))
        {
            return Roles.Company;
        }
        return Roles.None;
    }


    private async Task<bool> AddFreelancerAsync(string userdId)
    {
        var result = _session.LinqQueryAsync(c =>
            c.GetTable<FreelancerUser>()
            .FirstOrDefaultAsync(c => c.UserId == userdId)).Result;

        if (result == null)
        {
            var insertedCount = _session.LinqTableQueryAsync<FreelancerUser, int>(table => table
            .InsertAsync(
                () => new FreelancerUser
                {
                    UserId = userdId
                }));
            return (insertedCount.Result == 1);
        }
        return true;
    }

    private async Task<bool> AddCompanyAsync(string userdId)
    {
        var result = _session.LinqQueryAsync(c =>
           c.GetTable<CompanyUser>()
           .FirstOrDefaultAsync(c => c.UserId == userdId)).Result;

        if (result == null)
        {
            var insertedCount = _session.LinqTableQueryAsync<CompanyUser, int>(table => table
            .InsertAsync(
                () => new CompanyUser
                {
                    UserId = userdId
                }));
            return (insertedCount.Result == 1);
        }
        return true;

    }

    public async Task<bool> EditFreelancer(FreelancerProfileViewModel viewModel)
    {
        FreelancerUser record = new FreelancerUser
        {
            Id = viewModel.Id,
            UserId = viewModel.UserId,
        };
        var modifiedCount = _session.LinqTableQueryAsync<FreelancerUser, int>(table => table
            .Where(record => record.Id == viewModel.Id)
            .Set(record => record.Address, viewModel.Address)
            .Set(record => record.BirthDate, viewModel.BirthDate)
            .Set(record => record.CityId, viewModel.CityId)
            .Set(record => record.CountryId, viewModel.CountryId)
            .Set(record => record.Lat, viewModel.Lat)
            .Set(record => record.Long, viewModel.Long)
            .Set(record => record.Mobile, viewModel.Mobile)
            .Set(record => record.PostalCode, viewModel.PostalCode)
            .Set(record => record.RegionId, viewModel.RegionId)
            .Set(record => record.Tel, viewModel.Tel)
            .Set(record => record.LastName, viewModel.LastName)
            .Set(record => record.FirstName, viewModel.FirstName)
            .Set(record => record.Website, viewModel.Website)
            .Set(record => record.VAT, viewModel.VAT)
            .UpdateAsync()).Result;
        return (modifiedCount == 1);




    }

    public async Task<bool> EditCompany(CompanyProfileViewModel viewModel)
    {
        CompanyUser record = new CompanyUser
        {
            Id = viewModel.Id,
            UserId = viewModel.UserId,
        };
        var modifiedCount = _session.LinqTableQueryAsync<CompanyUser, int>(table => table
            .Where(record => record.Id == viewModel.Id)
            .Set(record => record.Address, viewModel.Address)
            .Set(record => record.Activities, viewModel.Activities)
            .Set(record => record.CompanyName, viewModel.CompanyName)
            .Set(record => record.CityId, viewModel.CityId)
            .Set(record => record.CountryId, viewModel.CountryId)
            .Set(record => record.CompanyTel, viewModel.Tel)
            .Set(record => record.ContactPersonName, viewModel.ContactPersonName)
            .Set(record => record.ContactPersonPosition, viewModel.ContactPersonPosition)
            .Set(record => record.Lat, viewModel.Lat)
            .Set(record => record.Long, viewModel.Long)
            .Set(record => record.PostalCode, viewModel.PostalCode)
            .Set(record => record.RegionId, viewModel.RegionId)
            .Set(record => record.Website, viewModel.Website)
            .Set(record => record.VAT, viewModel.VAT)
            .UpdateAsync()).Result;
        return (modifiedCount == 1);
    }
    //public void EditCompany(CompanyProfileViewModel viewModel)
    //{
    //    CompanyUser record = _companyRepository.Get(viewModel.Id);
    //    record.Address = viewModel.Address;
    //    record.ContactPersonPosition = viewModel.ContactPersonPosition;
    //    record.ContactPersonName = viewModel.ContactPersonName;
    //    record.CityId = viewModel.CityId;
    //    record.CountryId = viewModel.CountryId;
    //    record.Lat = viewModel.Lat;
    //    record.Long = viewModel.Long;
    //    record.PostalCode = viewModel.PostalCode;
    //    record.RegionId = viewModel.RegionId;
    //    record.CompanyTel = viewModel.Tel;
    //    record.VAT = viewModel.VAT;
    //    record.Website = viewModel.Website;

    //    record.Activities = viewModel.Activities;
    //    record.CompanyName = viewModel.CompanyName;
    //    _companyRepository.Update(record);
    //}

    //public CompanyProfileViewModel GetCompany(int userId)
    //{
    //    CompanyUser user = _companyRepository.Table
    //       .FirstOrDefault(c => c.UserId == userId);
    //    if (user != null)
    //    {
    //        CompanyProfileViewModel viewModel = new CompanyProfileViewModel
    //        {
    //            Address = user.Address,
    //            CityId = user.CityId,
    //            ContactPersonPosition = user.ContactPersonPosition,
    //            CountryId = user.CountryId,
    //            Id = user.Id,
    //            Lat = user.Lat,
    //            Long = user.Long,
    //            PostalCode = user.PostalCode,
    //            RegionId = user.RegionId,
    //            Tel = user.CompanyTel,
    //            UserId = user.UserId,
    //            VAT = user.VAT,
    //            Website = user.Website,
    //            Activities = user.Activities,
    //            CompanyName = user.CompanyName,
    //            ContactPersonName = user.ContactPersonName


    //        };
    //        return viewModel;
    //    }
    //    return null;
    //}

    public async Task<FreelancerProfileViewModel> GetFreelancerProfile(string userId)
    {
        var user = _session.LinqQueryAsync(c =>
          c.GetTable<FreelancerUser>()
          .FirstOrDefaultAsync(c => c.UserId == userId)).Result;

        if (user != null)
        {
            FreelancerProfileViewModel viewModel = new FreelancerProfileViewModel
            {
                Address = user.Address,
                CityId = user.CityId,
                CountryId = user.CountryId,
                Id = user.Id,
                Lat = user.Lat,
                Long = user.Long,
                PostalCode = user.PostalCode,
                RegionId = user.RegionId,
                Tel = user.Tel,
                UserId = user.UserId,
                VAT = user.VAT,
                Mobile = user.Mobile,
                BirthDate = user.BirthDate,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Website = user.Website
            };
            return viewModel;
        }
        return null;
    }

    public async Task<CompanyProfileViewModel> GetCompanyProfile(string userId)
    {
        var user = _session.LinqQueryAsync(c =>
          c.GetTable<CompanyUser>()
          .FirstOrDefaultAsync(c => c.UserId == userId)).Result;

        if (user != null)
        {
            CompanyProfileViewModel viewModel = new CompanyProfileViewModel
            {
                Address = user.Address,
                CityId = user.CityId,
                CountryId = user.CountryId,
                Id = user.Id,
                Lat = user.Lat,
                Long = user.Long,
                PostalCode = user.PostalCode,
                RegionId = user.RegionId,
                UserId = user.UserId,
                VAT = user.VAT,
                Website = user.Website,
                Activities = user.Activities,
                CompanyName = user.CompanyName,
                ContactPersonName = user.ContactPersonName,
                ContactPersonPosition = user.ContactPersonPosition,
                Tel = user.CompanyTel

            };
            return viewModel;
        }
        return null;
    }

    //public FreelancerUser GetFreelancerBase(int userId)
    //{
    //    FreelancerUser user = _freelancerRepository.Table
    //      .FirstOrDefault(c => c.UserId == userId);

    //    return user;
    //}

    public async Task<IEnumerable<SelectListItem>> GetNationalities()
    {
        return _session.LinqQueryAsync(c =>
         c.GetTable<Nationality>()
         .OrderBy(c => c.Name)
         .Select(c => new SelectListItem
         {
             Value = c.Id.ToString(),
             Text = c.Name
         }).ToListAsync()).Result;
    }

    public async Task<IEnumerable<SelectListItem>> GetCountries()
    {
        return _session.LinqQueryAsync(c =>
       c.GetTable<Country>()
      .OrderBy(c => c.Name)
         .Select(c => new SelectListItem
         {
             Value = c.Id.ToString(),
             Text = c.Name
         }).ToListAsync()).Result;
    }

    public async Task<IEnumerable<SelectListItem>> GetRegions(int countryId)
    {
        return _session.LinqQueryAsync(c =>
       c.GetTable<Region>()
       .Where(c => c.CountryId == countryId)
       .OrderBy(c => c.Name)
         .Select(c => new SelectListItem
         {
             Value = c.Id.ToString(),
             Text = c.Name
         }).ToListAsync()).Result;
    }


    public async Task<IEnumerable<SelectListItem>> GetCities(int regionId)
    {
        return _session.LinqQueryAsync(c =>
         c.GetTable<City>()
         .Where(c => c.RegionId == regionId)
         .OrderBy(c => c.Name)
         .Select(c => new SelectListItem
         {
             Value = c.Id.ToString(),
             Text = c.Name
         }).ToListAsync()).Result;
    }

    public async Task<List<FreelancerNationalityViewModel>> GetFreelancerNationalities(int freelancerId)
    {
        //var result = _session.LinqQueryAsync(c =>
        // c.GetTable<FreelancerNationality>()
        // .Where( c => c.FreelancerId == freelancerId)
        // .ToListAsync()).Result;

        var result = _session.LinqQueryAsync(
            accessor =>
                (from freelancerNationality in accessor.GetTable<FreelancerNationality>()
                 join nationality in accessor.GetTable<Nationality>()
                 on freelancerNationality.NationalityId equals nationality.Id
                 where freelancerNationality.FreelancerId == freelancerId
                 orderby nationality.Name
                 select new FreelancerNationalityViewModel
                 {
                     FreelancerId = freelancerNationality.FreelancerId,
                     Id = freelancerNationality.Id,
                     NationalityId = nationality.Id,
                     NationalityTitle = nationality.Name
                 })
                .ToListAsync()).Result;
        return result;
    }

    public async Task<bool> AddFreelancerNationality(FreelancerNationality freelancerNationality)
    {
        var insertedCount = _session.LinqTableQueryAsync<FreelancerNationality, int>(table => table
            .InsertAsync(
                () => new FreelancerNationality
                {
                    FreelancerId = freelancerNationality.FreelancerId,
                    NationalityId = freelancerNationality.NationalityId
                }));
        return (insertedCount.Result == 1);
    }

    public async Task<bool> RemoveFreelancerNationality(int id)
    {

        var deletedCount = _session.LinqTableQueryAsync<FreelancerNationality, int>(table => table
            .Where(record => record.Id == id)
            .DeleteAsync());

        return (deletedCount.Result == 1);
    }

    public async Task<IDictionary<string, string>> GetTaxonomies(IContentManager contentManager, string taxonomyName)
    {

        IDictionary<string, string> terms = await contentManager.GetTaxonomyTermsDisplayTextsAsync(taxonomyName);
        return terms;
    }



    //public IEnumerable<TaxonomyItemViewModel> GetExpertise(int parentId = 0)
    //{

    //    if (parentId == 0)
    //    {
    //        TaxonomyPart masterTaxonomyPart = _taxonomyService.GetTaxonomyByName("Expertise");
    //        IQueryable<TermPart> parts = masterTaxonomyPart.Terms.AsQueryable();

    //        parts = parts.Where(c => c.Path == "/" && c.TaxonomyId == masterTaxonomyPart.Id);
    //        return parts.Select(c => new TaxonomyItemViewModel
    //        {
    //            Id = c.Id,
    //            Name = c.Name
    //        });
    //    }
    //    else
    //    {
    //        TaxonomyPart masterTaxonomyPart = _taxonomyService.GetTaxonomyByName("Expertise");
    //        IQueryable<TermPart> parts = masterTaxonomyPart.Terms.AsQueryable();
    //        string path = string.Format("/{0}/", parentId);
    //        parts = parts.Where(c => c.Path.EndsWith(path) && c.TaxonomyId == masterTaxonomyPart.Id);
    //        return parts.Select(c => new TaxonomyItemViewModel
    //        {
    //            Id = c.Id,
    //            Name = c.Name
    //        });
    //    }
    //}
}
