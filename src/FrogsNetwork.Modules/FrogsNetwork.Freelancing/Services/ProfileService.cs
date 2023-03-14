using System.Diagnostics;
using System.Linq;
using FrogsNetwork.Freelancing.Models;
using FrogsNetwork.Freelancing.ViewModels;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Maps.Common;
using GoogleApi.Entities.Maps.DistanceMatrix.Request;
using GoogleApi;
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
using YesSql;
using ISession = YesSql.ISession;
using OrchardCore.Users.Indexes;

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

    public async Task<IEnumerable<SelectListItem>> GetTaxonomies(IReadOnlyList<ContentItem> contentItems, string[] contectItemIds)
    {
        return contentItems.Where(c => contectItemIds.Contains(c.ContentItemId))
            .Select(c => new SelectListItem
            {
                Value = c.ContentItemId,
                Text = c.DisplayText
            });
    }

    public async Task<string[]> GetFreelancerExpertiseIds(int freelancerId, int levelId = 0)
    {
        return _session.LinqQueryAsync(c =>
         c.GetTable<FreelancerExpertise>()
         .Where(c => c.FreelancerId == freelancerId &&
             (levelId == 0 || levelId != 0 && c.LevelId == levelId))
        .Select(c => c.ExpertiseId).ToListAsync()).Result.ToArray();
    }

    public async Task<string[]> GetFreelancerServiceIds(int freelancerId, int levelId = 0)
    {
        return _session.LinqQueryAsync(c =>
         c.GetTable<FreelancerService>()
         .Where(c => c.FreelancerId == freelancerId &&
             (levelId == 0 || levelId != 0 && c.LevelId == levelId))
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




    public async Task<IEnumerable<FreelancerResultViewModel>> SearchFreelancers(IContentManager contentManager, IContentHandleManager contentHandleManager, FreelancerSearchViewModel searchViewModel)
    {
        if (!searchViewModel.CountryId.HasValue)
            searchViewModel.CountryId = 0;
        if (!searchViewModel.RegionId.HasValue)
            searchViewModel.RegionId = 0;
        if (!searchViewModel.CityId.HasValue)
            searchViewModel.CityId = 0;

        if (searchViewModel.ExpertiseFirstIds == null)
            searchViewModel.ExpertiseFirstIds = new string[0];
        if (searchViewModel.ExpertiseSecondIds == null)
            searchViewModel.ExpertiseSecondIds = new string[0];
        if (searchViewModel.ServicesFirstIds == null)
            searchViewModel.ServicesFirstIds = new string[0];
        if (searchViewModel.ServicesSecondIds == null)
            searchViewModel.ServicesSecondIds = new string[0];

        #region Expertise

        List<string> expertiseIds = new List<string>();
        if (searchViewModel.ExpertiseFirstIds.Any() && !searchViewModel.ExpertiseSecondIds.Any())
        {
            expertiseIds.AddRange(searchViewModel.ExpertiseFirstIds.Select(c => c.ToString()));
        }
        else
        {
            if (searchViewModel.ExpertiseFirstIds.Any())
            {
                foreach (var id in searchViewModel.ExpertiseFirstIds)
                {
                    string[] selectedLevelIds = GetExpertise(contentManager, contentHandleManager, id).Result.Select(c => c.Value).ToArray();
                    if (selectedLevelIds != null && searchViewModel.ExpertiseSecondIds.Any())
                    {
                        if (!selectedLevelIds.Intersect(searchViewModel.ExpertiseSecondIds).Any())
                            expertiseIds.Add(id.ToString());
                    }
                }
            }

            if (searchViewModel.ExpertiseSecondIds.Any())
            {
                expertiseIds.AddRange(searchViewModel.ExpertiseSecondIds.Select(c => c.ToString()));
            }
        }
        if (expertiseIds.Any())
        {
            expertiseIds = expertiseIds.Distinct().ToList();
        }
        #endregion

        #region Services
        List<string> servicesIds = new List<string>();
        if (searchViewModel.ServicesFirstIds.Any() && !searchViewModel.ServicesSecondIds.Any())
        {
            servicesIds.AddRange(searchViewModel.ExpertiseFirstIds.Select(c => c.ToString()));
        }
        else
        {
            if (searchViewModel.ServicesFirstIds.Any())
            {
                foreach (var id in searchViewModel.ServicesFirstIds)
                {
                    string[] selectedLevelIds = GetServices(contentManager, contentHandleManager, id).Result.Select(c => c.Value).ToArray();
                    if (selectedLevelIds != null && searchViewModel.ServicesSecondIds != null)
                    {
                        if (!selectedLevelIds.Intersect(searchViewModel.ServicesSecondIds).Any())
                            servicesIds.Add(id.ToString());
                    }
                }
            }

            if (searchViewModel.ServicesSecondIds.Any())
            {
                servicesIds.AddRange(searchViewModel.ServicesSecondIds.Select(c => c.ToString()));
            }
        }
        if (servicesIds.Any())
        {
            servicesIds = servicesIds.Distinct().ToList();
        }
        #endregion

        var result = _session.LinqQueryAsync(
           c => (
               from freelancer in c.GetTable<FreelancerUser>().Where(c =>
                   (searchViewModel.CountryId == 0 || (searchViewModel.CountryId != 0 && c.CountryId == searchViewModel.CountryId)) &&
                   (searchViewModel.RegionId == 0 || (searchViewModel.RegionId != 0 && c.RegionId == searchViewModel.RegionId)) &&
                   (searchViewModel.CityId == 0 || (searchViewModel.CityId != 0 && c.CityId == searchViewModel.CityId)))
               join country in c.GetTable<Country>()
               on freelancer.CountryId equals country.Id
               join region in c.GetTable<Region>()
               on freelancer.RegionId equals region.Id
               join city in c.GetTable<City>()
               on freelancer.CityId equals city.Id

               select new FreelancerResultViewModel
               {
                   FirstName = freelancer.FirstName,
                   LastName = freelancer.LastName,
                   Lat = freelancer.Lat,
                   Long = freelancer.Long,
                   Id = freelancer.Id,
                   UserId = freelancer.UserId,
                   Country = country.Name,
                   Region = region.Name,
                   City = city.Name
               })
               .ToListAsync()).Result;



        if (!result.Any())
            return result;

        List<FreelancerResultViewModel> filteredResults = new List<FreelancerResultViewModel>();
        if (expertiseIds.Any() || servicesIds.Any())
        {
            foreach (var item in result)
            {  
                var currentExpertiese = _session.LinqQueryAsync(
                          c => (from frlexp in c.GetTable<FreelancerExpertise>()
                                where frlexp.FreelancerId == item.Id &&
                                expertiseIds.Contains(frlexp.ExpertiseId)
                                select frlexp)
                              .ToListAsync()).Result;

                var currentServices = _session.LinqQueryAsync(
                       c => (from frlsrv in c.GetTable<FreelancerService>()
                             where frlsrv.FreelancerId == item.Id &&
                             expertiseIds.Contains(frlsrv.ServiceId)
                             select frlsrv)
                           .ToListAsync()).Result;


                if (currentExpertiese.Any() || currentServices.Any())
                    filteredResults.Add(item);
            }
        }
        else
        {
            filteredResults.AddRange(result);
        }

        if (!filteredResults.Any())
            return filteredResults;

        
        var destinationLocation = new LocationEx(new CoordinateEx(Convert.ToDouble(searchViewModel.Lat), Convert.ToDouble(searchViewModel.Long)));

        var originLocations = result.Select(c => new LocationEx(new CoordinateEx(Convert.ToDouble(c.Lat), Convert.ToDouble(c.Long))));

        var request = new DistanceMatrixRequest
        {
            Key = "AIzaSyAW1SgI7RCtbjx3t5yUIfjiDTW6fvn50OA",
            Origins = originLocations,
            Destinations = new[]
               {
                destinationLocation
            }
        };

        var distanceMatrix = GoogleMaps.DistanceMatrix.Query(request).Rows.ToArray();

        for (int i = 0; i < filteredResults.Count; i++)
        {
            var distanceElement = distanceMatrix[i].Elements.FirstOrDefault();
            if (distanceElement != null && distanceElement.Status == GoogleApi.Entities.Common.Enums.Status.Ok)
            {
                filteredResults[i].DistanceValue = distanceMatrix[i].Elements.FirstOrDefault().Distance.Value;
                filteredResults[i].DistanceText = distanceMatrix[i].Elements.FirstOrDefault().Distance.Text;
                filteredResults[i].DurationValue = distanceMatrix[i].Elements.FirstOrDefault().Duration.Value;
                filteredResults[i].DurationText = distanceMatrix[i].Elements.FirstOrDefault().Duration.Text;
            }
        }

        result = filteredResults.OrderBy(c => c.DistanceValue).ToList();
        return result;





       




    }



    public async Task<FreelancerDetailViewModel> GetFreelancerDetail(IContentManager contentManager, IContentHandleManager contentHandleManager, int id)
    {

        var freelancerUser = _session.LinqQueryAsync(c =>
           (from freelancer in c.GetTable<FreelancerUser>().Where(c => c.Id == id)
            join user in c.GetTable<UserIndex>()
            on freelancer.UserId equals user.UserId
            join country in c.GetTable<Country>()
            on freelancer.CountryId equals country.Id
            join region in c.GetTable<Region>()
            on freelancer.RegionId equals region.Id
            join city in c.GetTable<City>()
            on freelancer.CityId equals city.Id

            select new FreelancerDetailViewModel
            {
                Address = freelancer.Address,
                CityTitle = city.Name,
                CountryTitle = country.Name,
                Id = freelancer.Id,
                Lat = freelancer.Lat,
                Long = freelancer.Long,
                PostalCode = freelancer.PostalCode,
                RegionTitle = region.Name,
                Tel = freelancer.Tel,
                CompanyTel = freelancer.Tel,
                UserId = freelancer.UserId,
                VAT = freelancer.VAT,
                Mobile = freelancer.Mobile,
                BirthDate = freelancer.BirthDate,
                FirstName = freelancer.FirstName,
                LastName = freelancer.LastName,
                Website = freelancer.Website,
                Email = user.NormalizedEmail
            }).FirstOrDefaultAsync()).Result;


        if (freelancerUser != null)
        {
            var expertise = await contentManager.GetTaxonomyTermsAsync(contentHandleManager, nameof(Taxonomies.Expertise));

            var services = await contentManager.GetTaxonomyTermsAsync(contentHandleManager, nameof(Taxonomies.Services));


          

            List<string> expertiseValues = new List<string>();
          
            var firstExpertise = GetTaxonomies(expertise).Result;

            var filteredFirstExpertiseIds = GetFreelancerExpertiseIds(id, 1).Result;

            firstExpertise = firstExpertise.Where(c => filteredFirstExpertiseIds.Contains(c.Value));
            expertiseValues.AddRange(firstExpertise.Select(c => c.Text));

            foreach( var item in firstExpertise)
            {
                var secondExpertise = GetTaxonomies(expertise, item.Value).Result;

                var filteredSecondExpertiseIds = GetFreelancerExpertiseIds(id, 2).Result;
                secondExpertise = secondExpertise.Where(c => filteredSecondExpertiseIds.Contains(c.Value));
                expertiseValues.AddRange(secondExpertise.Select(c => c.Text));

            }

            List<string> servicesValues = new List<string>();
            var firstService = GetTaxonomies(services).Result;

            var filteredFirstServiceIds = GetFreelancerServiceIds(id, 1).Result;

            firstService = firstService.Where(c => filteredFirstServiceIds.Contains(c.Value));
            servicesValues.AddRange(firstService.Select(c => c.Text));

            foreach (var item in firstService)
            {
                var secondServices = GetTaxonomies(services, item.Value).Result;

                var filteredSecondServicesIds = GetFreelancerServiceIds(id, 2).Result;
                secondServices = secondServices.Where(c => filteredSecondServicesIds.Contains(c.Value));
                servicesValues.AddRange(secondServices.Select(c => c.Text));

            }




            //IEnumerable<SelectListItem> expertiseFiltered = GetTaxonomies(expertise, expertiseIds.ToArray()).Result;
            //IEnumerable<SelectListItem> servicesFiltered = GetTaxonomies(services, serviceIds.ToArray()).Result;

            freelancerUser.Expertise = expertiseValues;
            freelancerUser.Services = servicesValues;

            freelancerUser.Nationalities = GetFreelancerNationalities(id).Result;
            freelancerUser.Educations = GetFreelancerEducation(id).Result;
            freelancerUser.Certificates = GetFreelancerCertificate(id).Result;
            freelancerUser.Languages = GetFreelancerLanguages(id).Result;



            return freelancerUser;

        }
        return null;
    }



}
