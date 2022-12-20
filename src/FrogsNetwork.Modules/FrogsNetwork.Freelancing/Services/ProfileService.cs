using FrogsNetwork.Freelancing.Models;
using FrogsNetwork.Freelancing.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OrchardCore.Users;
using OrchardCore.Users.Models;
using OrchardCore.Users.Services;

namespace FrogsNetwork.Freelancing.Services;
public class ProfileService : IProfileService
{
    private readonly IUserService _userService;

    public ProfileService(IUserService userService)
    {
        _userService = userService;
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
            AddFreelancer(new FreelancerProfileViewModel
            {
                UserId = userInfo.UserId
            });
        else if (userInfo.RoleNames.Contains(nameof(Roles.Company)))
            AddCompany(new CompanyProfileViewModel
            {
                UserId = userInfo.UserId
            });
        else
        {
            //log
        }
    }
    private void AddFreelancer(FreelancerProfileViewModel viewModel)
    {
        //_freelancerRepository.Create(new FreelancerUser
        //{
        //    UserId = viewModel.UserId
        //});
    }


    private void AddCompany(CompanyProfileViewModel viewModel)
    {
        //_companyRepository.Create(new CompanyUser
        //{
        //    UserId = viewModel.UserId
        //});

    }
}
