using Abp.Authorization;
using Radin.Authorization.Roles;
using Radin.Authorization.Users;

namespace Radin.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
