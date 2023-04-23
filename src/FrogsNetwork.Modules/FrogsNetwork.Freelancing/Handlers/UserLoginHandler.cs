using FrogsNetwork.Freelancing.Models;
using FrogsNetwork.Freelancing.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using OrchardCore.Modules;
using OrchardCore.Users;
using OrchardCore.Users.Events;
using OrchardCore.Users.Models;

namespace FrogsNetwork.Freelancing.Handlers
{
    public class UserLoginHandler : ILoginFormEvent
    {
        private readonly ProfileService _profileService;
        private readonly IHttpContextAccessor _httpContext;

        public UserLoginHandler(ProfileService profileService,
            IHttpContextAccessor httpContext)
        {
            _profileService = profileService;
            _httpContext = httpContext;
        }

        public Task IsLockedOutAsync(IUser user) => Task.CompletedTask;

        public Task LoggedInAsync(IUser user)
        {
            Roles role = _profileService.GetUserRole(user);
            if (role == Roles.Freelancer)
               _httpContext.HttpContext.Session.SetString("returnUrl", "/FreelancerDashboard/Index");
            else if (role == Roles.Company)
                _httpContext.HttpContext.Session.SetString("returnUrl", "/CompanyDashboard/Index");
            return Task.CompletedTask;
        }

        public async Task LoggingInAsync(string userName, Action<string, string> reportError)
        {
            
        }

        public Task LoggingInFailedAsync(string userName)
        {
            
            return Task.CompletedTask;
        }

        public Task LoggingInFailedAsync(IUser user)
        {
           
            return Task.CompletedTask;
        }

       
    }

}

