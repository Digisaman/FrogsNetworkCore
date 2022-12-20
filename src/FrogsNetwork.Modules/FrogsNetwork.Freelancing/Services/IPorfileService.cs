using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrogsNetwork.Freelancing.ViewModels;
using OrchardCore.Users;

namespace FrogsNetwork.Freelancing.Services;
public interface IProfileService
{
    void AddUserProfile(IUser user);
}
