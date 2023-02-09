using System.Diagnostics;
using FrogsNetwork.Freelancing.Models;
using FrogsNetwork.Freelancing.ViewModels;
using GraphQL.Language.AST;
using LinqToDB;
using Lombiq.HelpfulLibraries.LinqToDb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using OrchardCore.ContentManagement;
using OrchardCore.Taxonomies.Models;
using OrchardCore.Users;
using OrchardCore.Users.Models;
using OrchardCore.Users.Services;
using ISession = YesSql.ISession;

namespace FrogsNetwork.Freelancing.Services;


public class ProfileService : IProfileService
{
    private readonly IUserService _userService;
    private readonly ISession _session;
    //private readonly IContentManager _contentManager;
    //private readonly IContentHandleManager _contentHandleManager;

    public ProfileService(
        IUserService userService,
        ISession session)
    //IContentManager contentManager,
    //IContentHandleManager contentHandleManager)
    {
        _userService = userService;
        _session = session;
        //_contentManager = contentManager;
        //_contentHandleManager = contentHandleManager;
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

    public async Task<IEnumerable<SelectListItem>> GetLanguages()
    {
        return _session.LinqQueryAsync(c =>
         c.GetTable<Language>()
         .OrderBy(c => c.Name)
         .Select(c => new SelectListItem
         {
             Value = c.Id.ToString(),
             Text = c.Name
         }).ToListAsync()).Result;
    }

    public IEnumerable<SelectListItem> GetLanguageLevels()
    {
        List<SelectListItem> list = new List<SelectListItem>();
        foreach (var value in Enum.GetValues(typeof(LanguageLevel)))
        {
            list.Add(new SelectListItem
            {
                Value = Convert.ToInt32(value).ToString(),
                Text = ((LanguageLevel)value).GetDisplayNameAttribute()
            });
        }
        return list;
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

    public async Task<List<FreelancerLanguageViewModel>> GetFreelancerLanguages(int freelancerId)
    {

        var result = _session.LinqQueryAsync(
            accessor =>
                (from freelancerLanguage in accessor.GetTable<FreelancerLanguage>()
                 join language in accessor.GetTable<Language>()
                 on freelancerLanguage.LanguageId equals language.Id
                 where freelancerLanguage.FreelancerId == freelancerId
                 orderby language.Name
                 select new FreelancerLanguageViewModel
                 {
                     FreelancerId = freelancerLanguage.FreelancerId,
                     Id = freelancerLanguage.Id,
                     LanguageId = language.Id,
                     LanguageTitle = language.Name,
                     LevelId = Convert.ToInt32(freelancerLanguage.Level),
                     LevelTitle = freelancerLanguage.Level.GetDisplayNameAttribute()
                 })
                .ToListAsync()).Result;
        return result;
    }

    public async Task<bool> AddFreelancerLanguage(FreelancerLanguage freelancerLanguage)
    {
        var insertedCount = _session.LinqTableQueryAsync<FreelancerLanguage, int>(table => table
            .InsertAsync(
                () => new FreelancerLanguage
                {
                    FreelancerId = freelancerLanguage.FreelancerId,
                    LanguageId = freelancerLanguage.LanguageId,
                    Level = freelancerLanguage.Level
                }));
        return (insertedCount.Result == 1);
    }

    public async Task<bool> RemoveFreelancerLanguage(int id)
    {

        var deletedCount = _session.LinqTableQueryAsync<FreelancerLanguage, int>(table => table
            .Where(record => record.Id == id)
            .DeleteAsync());

        return (deletedCount.Result == 1);
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

    public async Task<IEnumerable<TaxonomyItemViewModel>> GetTaxonomies(IContentManager contentManager,
        IContentHandleManager contentHandleManager, Taxonomies taxonomy, string parentItemId)
    {

        var terms = contentManager.GetTaxonomyTermsAsync(contentHandleManager, nameof(taxonomy)).Result;
        if (parentItemId == null)
        {
            return terms.Select(c => new TaxonomyItemViewModel
            {
                Id = c.ContentItemId,
                Name = c.DisplayText
            });
        }
        else
        {
            var term = terms.FirstOrDefault(c => c.ContentItemId == parentItemId);
            var termContent = term.Content.Terms as JArray;
            List<ContentItem> subTerms = termContent?.ToObject<List<ContentItem>>();
            return subTerms.Select(c => new TaxonomyItemViewModel
            {
                Id = c.ContentItemId,
                Name = c.DisplayText
            });

        }
        //var items = terms.First().Content.Terms as JArray;
        //List<ContentItem> subItem0 = items?.ToObject<List<ContentItem>>();


    }

    public async Task<IEnumerable<SelectListItem>> GetTaxonomies(IReadOnlyList<ContentItem> contentItems, string parentItemId = null)
    {


        if (parentItemId == null)
        {
            return contentItems.Select(c => new SelectListItem
            {
                Value = c.ContentItemId,
                Text = c.DisplayText
            });
        }
        else
        {
            var term = contentItems.FirstOrDefault(c => c.ContentItemId == parentItemId);
            var termContent = term.Content.Terms as JArray;
            List<ContentItem> subTerms = termContent?.ToObject<List<ContentItem>>();
            IEnumerable<SelectListItem> finalTerms = subTerms.Select(c => new SelectListItem
            {
                Value = c.ContentItemId,
                Text = $"{term.DisplayText} - {c.DisplayText}"
            });
            return finalTerms;

        }
        //var items = terms.First().Content.Terms as JArray;
        //List<ContentItem> subItem0 = items?.ToObject<List<ContentItem>>();


    }

    public async Task<string[]> GetFreelancerExpertiseIds(int freelancerId, int levelId)
    {
        return _session.LinqQueryAsync(c =>
         c.GetTable<FreelancerExpertise>()
         .Where(c => c.FreelancerId == freelancerId && c.LevelId == levelId)
        .Select(c => c.ExpertiseId).ToListAsync()).Result.ToArray();
    }

    public async Task<string[]> GetFreelancerServiceIds(int freelancerId, int levelId)
    {
        return _session.LinqQueryAsync(c =>
         c.GetTable<FreelancerService>()
         .Where(c => c.FreelancerId == freelancerId && c.LevelId == levelId)
        .Select(c => c.ServiceId).ToListAsync()).Result.ToArray();
    }


    public async Task<bool> SaveFreelancerExpertiseIds(int freelancerId, int levelId, string[] expertiseIds)
    {
        var deleteQuery = _session.LinqTableQueryAsync<FreelancerExpertise, int>(table => table
          .Where(c => c.FreelancerId == freelancerId && c.LevelId == levelId)
          .DeleteAsync());
        bool result = (deleteQuery.Result == 1);

        foreach (string expertiseId in expertiseIds)
        {
            var insertedCount = _session.LinqTableQueryAsync<FreelancerExpertise, int>(table => table
            .InsertAsync(
                () => new FreelancerExpertise
                {
                    FreelancerId = freelancerId,
                    ExpertiseId = expertiseId,
                    LevelId = levelId
                }));
            result = result && (insertedCount.Result == 1);
        }

        return result;
    }

    public async Task<bool> SaveFreelancerServiceIds(int freelancerId, int levelId, string[] serviceIds)
    {
        var deleteQuery = _session.LinqTableQueryAsync<FreelancerService, int>(table => table
          .Where(c => c.FreelancerId == freelancerId && c.LevelId == levelId)
          .DeleteAsync());

        bool result = (deleteQuery.Result == 1);

        foreach (string serviceId in serviceIds)
        {
            var insertedCount = _session.LinqTableQueryAsync<FreelancerService, int>(table => table
            .InsertAsync(
                () => new FreelancerService
                {
                    FreelancerId = freelancerId,
                    ServiceId = serviceId,
                    LevelId = levelId
                }));
            result = result && (insertedCount.Result == 1);
        }

        return result;
    }

    public async Task<IEnumerable<FreelancerCertificateViewModel>> GetFreelancerCertificate(int freelancerId)
    {
        return _session.LinqQueryAsync(c =>
        c.GetTable<FreelancerCertificate>()
        .Where(c => c.FreelancerId == freelancerId)
       .Select(c => new FreelancerCertificateViewModel
       {
           Certificate = c.Certificate,
           Description = c.Description,
           Id = c.Id,
           Organization = c.Organization
       }).ToListAsync()).Result;
    }

    public async Task<IEnumerable<FreelancerEducationViewModel>> GetFreelancerEducation(int freelancerId)
    {
        var result = _session.LinqQueryAsync(
           accessor =>
               (from freelancerEducation in accessor.GetTable<FreelancerEducation>()
                join country in accessor.GetTable<Country>()
                on freelancerEducation.CountryId equals country.Id
                where freelancerEducation.FreelancerId == freelancerId
                orderby freelancerEducation.EndYear descending
                select new FreelancerEducationViewModel
                {
                    School = freelancerEducation.School,
                    City = freelancerEducation.City,
                    CountryId = freelancerEducation.CountryId,
                    CountryName = country.Name,
                    Degree = freelancerEducation.Degree,
                    EndYear = freelancerEducation.EndYear,
                    Field = freelancerEducation.Field,
                    FreelancerId = freelancerEducation.FreelancerId,
                    Id = freelancerEducation.Id
                })
               .ToListAsync()).Result;
        return result;
    }

    public async Task<bool> AddFreelancerEducation(FreelancerEducation freelancerEducation)
    {
        var insertedCount = _session.LinqTableQueryAsync<FreelancerEducation, int>(table => table
            .InsertAsync(
                () => new FreelancerEducation
                {
                    City = freelancerEducation.City,
                    CountryId = freelancerEducation.CountryId,
                    Degree = freelancerEducation.Degree,
                    EndYear = freelancerEducation.EndYear,
                    Field = freelancerEducation.Field,
                    FreelancerId = freelancerEducation.FreelancerId,
                    School = freelancerEducation.School
                }));
        return (insertedCount.Result == 1);
    }

    public async Task<bool> AddFreelancerCertificate(FreelancerCertificate freelancerCertificate)
    {
        var insertedCount = _session.LinqTableQueryAsync<FreelancerCertificate, int>(table => table
            .InsertAsync(
                () => new FreelancerCertificate
                {
                    Certificate = freelancerCertificate.Certificate,
                    Description = freelancerCertificate.Description,
                    FreelancerId = freelancerCertificate.FreelancerId,
                    Organization = freelancerCertificate.Organization
                }));
        return (insertedCount.Result == 1);
    }

    public async Task<bool> RemoveFreelancerCertificate(int id)
    {
        var deletedCount = _session.LinqTableQueryAsync<FreelancerCertificate, int>(table => table
           .Where(record => record.Id == id)
           .DeleteAsync());

        return (deletedCount.Result == 1);
    }

    public async Task<bool> RemoveFreelancerEducation(int id)
    {
        var deletedCount = _session.LinqTableQueryAsync<FreelancerEducation, int>(table => table
          .Where(record => record.Id == id)
          .DeleteAsync());

        return (deletedCount.Result == 1);
    }

    public async Task<IEnumerable<SelectListItem>> GetExpertise(IContentManager contentManager, IContentHandleManager contentHandleManager, string parentId = null)
    {

        return GetTaxonomies(contentManager, contentHandleManager, Taxonomies.Expertise, parentId).Result.Select(c => new SelectListItem
        {
            Value = c.Id,
            Text = c.Name
        });
    }

    public async Task<IEnumerable<SelectListItem>> GetServices(IContentManager contentManager, IContentHandleManager contentHandleManager, string parentId = null)
    {
        return GetTaxonomies(contentManager, contentHandleManager, Taxonomies.Services, parentId).Result.Select(c => new SelectListItem
        {
            Value = c.Id,
            Text = c.Name
        });
    }







}
