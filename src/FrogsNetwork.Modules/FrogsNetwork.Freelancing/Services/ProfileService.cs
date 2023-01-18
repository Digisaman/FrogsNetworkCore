using FrogsNetwork.Freelancing.Models;
using FrogsNetwork.Freelancing.ViewModels;
using LinqToDB;
using Lombiq.HelpfulLibraries.LinqToDb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        //UserInfo userInfo = await _userService.GetUserInfor(user);
        //{
        if ( userInfo == null)
        {
            //log
            return false;
        }
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
    private async Task<bool> AddFreelancerAsync(string userdId)
    {
        var result = _session.LinqQueryAsync(c =>
            c.GetTable<FreelancerUser>()
            .FirstOrDefaultAsync( c=> c.UserId == userdId)).Result;

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
}
