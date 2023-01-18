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

    public async void AddUserProfile(IUser user)
    {

        var userInfo = await _userService.GetUserAsync(user.UserName) as User;
        //UserInfo userInfo = await _userService.GetUserInfor(user);
        //{
        if ( userInfo == null)
        {
            //log
            return;
        }
        if (userInfo.RoleNames.Contains(nameof(Roles.Freelancer)))
            AddFreelancerAsync(userInfo.UserId).Wait();
        else if (userInfo.RoleNames.Contains(nameof(Roles.Company)))
            AddCompanyAsync(new CompanyProfileViewModel
            {
                UserId = userInfo.UserId
            });
        else
        {
            //log
        }
    }
    private async Task<bool> AddFreelancerAsync(string userdId)
    {
        var result = await _session.LinqQueryAsync(c =>
            c.GetTable<FreelancerUser>()
            .FirstOrDefaultAsync( c=> c.UserId == userdId));

        if (result == null)
        {
            var insertedCount = await _session.LinqTableQueryAsync<FreelancerUser, int>(table => table
            .InsertAsync(
                () => new FreelancerUser
                {
                    UserId = userdId       
                }));
            return (insertedCount == 1);
        }
        return true;
    }

    private async Task<bool> AddCompanyAsync(CompanyProfileViewModel viewModel)
    {
        var result = await _session.LinqQueryAsync(c =>
           c.GetTable<CompanyUser>()
           .FirstOrDefaultAsync(c => c.UserId == viewModel.UserId));

        if (result == null)
        {
            var insertedCount = await _session.LinqTableQueryAsync<CompanyUser, int>(table => table
            .InsertAsync(
                () => new CompanyUser
                {
                    UserId = viewModel.UserId
                }));
            return (insertedCount == 1);
        }
        return true;

    }
}
