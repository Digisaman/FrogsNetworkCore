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
    public static readonly Permission ManageProfile = new Permission("ManageProfile", "Manage profile information");

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
                    Permissions = new[] { ManageProfile }
                },
                new PermissionStereotype {
                    Name = nameof(Roles.Company),
                    Permissions = new[] { ManageProfile }
                }
            };
    }

    public async Task<IEnumerable<Permission>> GetPermissionsAsync()
    {
        var list = new List<Permission>
            {
                ManageProfile
            };

        //var roles = new string[] {
        //    nameof(Roles.Freelancer),
        //    nameof(Roles.Company)
        //};

        //foreach (var role in roles)
        //{
        //    list.Add(CommonPermissions.CreatePermissionForManageUsersInRole(role));
        //}

        return list;
    }
}
