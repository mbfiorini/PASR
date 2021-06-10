using Abp.Authorization;
using PASR.Authorization.Roles;
using PASR.Authorization.Users;

namespace PASR.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
