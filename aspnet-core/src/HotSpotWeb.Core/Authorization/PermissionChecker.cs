using Abp.Authorization;
using HotSpotWeb.Authorization.Roles;
using HotSpotWeb.Authorization.Users;

namespace HotSpotWeb.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
