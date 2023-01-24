using FrogsNetwork.Freelancing.Services;
using OrchardCore.Users;
using OrchardCore.Users.Events;

namespace FrogsNetwork.Freelancing.Handlers
{
    public class UserRegistrationHandler : IRegistrationFormEvents
    {
        private readonly ProfileService _profileService;

        public UserRegistrationHandler(ProfileService profileService)
        {
            _profileService = profileService;
        }

        public Task RegisteredAsync(IUser user)
        {
            return _profileService.AddUserProfile(user);
            //return Task.CompletedTask;
        }

        public Task RegistrationValidationAsync(Action<string, string> reportError)
        {
            return Task.CompletedTask;
        }
    }

}
