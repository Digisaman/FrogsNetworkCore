using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrogsNetwork.Freelancing.Models;
using OrchardCore.Security.Permissions;
using OrchardCore.Security.Services;

namespace FrogsNetwork.Freelancing;
public class Permissions : IPermissionProvider
{
    public static readonly Permission ManageFreelancerProfile = new Permission("ManageFreelancerProfile", "Manage profile information for freelancers", true);

    public static readonly Permission ManageCompanyProfile = new Permission("ManageCompanyProfile", "Manage profile information for companies", true);

    private readonly IRoleService _roleService;

    public Permissions(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
    {
        return new[] {
                new PermissionStereotype {
                    Name = nameof(Roles.Freelancer),
                    Permissions = new[] { ManageFreelancerProfile }
                },
                new PermissionStereotype {
                    Name = nameof(Roles.Company),
                    Permissions = new[] { ManageCompanyProfile }
                }
            };
    }

    public async Task<IEnumerable<Permission>> GetPermissionsAsync()
    {
        var list = new List<Permission>
            {
                ManageFreelancerProfile,
                ManageCompanyProfile
            };
        return list;
    }
}
