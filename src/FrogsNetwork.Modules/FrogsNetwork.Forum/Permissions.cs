using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrchardCore.Security.Permissions;
using OrchardCore.Security.Services;

namespace FrogsNetwork.Forum;
public class Permissions : IPermissionProvider
{
    public static readonly Permission ManageForum = new Permission("ManageForum", "Manage Forum", true);

    private readonly IRoleService _roleService;

    public Permissions(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
    {
        return new[]
        {
                new PermissionStereotype {
                    Name = "Freelancer",
                    Permissions = new[] { ManageForum }
                }
        };
    }

    public async Task<IEnumerable<Permission>> GetPermissionsAsync()
    {
        var list = new List<Permission>
            {
                ManageForum
            };
        return list;
    }
}

