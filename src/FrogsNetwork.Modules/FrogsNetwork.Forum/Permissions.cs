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
    public static readonly Permission ManageForum = new Permission(nameof(ManageForum), "Manage forums for others");
    public static readonly Permission ManageOwnForums = new Permission(nameof(ManageOwnForums), "Manage own forums", new[] {ManageForum});

    public static readonly Permission MoveThread = new Permission(nameof(MoveThread), "Move any thread to another forum");
    public static readonly Permission MoveOwnThread = new Permission(nameof(MoveOwnThread), "Move your own thread to another forum", new[] {MoveThread});
    public static readonly Permission StickyThread = new Permission(nameof(StickyThread), "Allows you to mark any thread as Sticky");
    public static readonly Permission StickyOwnThread = new Permission(nameof(StickyOwnThread), "Allows you to mark your own thread as Sticky", new[] {StickyThread});

    public static readonly Permission CloseThread = new Permission(nameof(CloseThread), "Allows you to close any thread");
    public static readonly Permission CloseOwnThread = new Permission(nameof(CloseOwnThread), "Allows you to close your own thread", new[] { CloseThread });

    
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

