using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Localization;
using Abp.MultiTenancy;
using Microsoft.AspNetCore.Identity;
using PASR.Authorization.Users;
using System.Threading.Tasks;

namespace PASR.Authorization
{
    public class PASRAuthorizationProvider : AuthorizationProvider
    {
        
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Teams, L("Teams"));
            context.CreatePermission(PermissionNames.Pages_Calls, L("Calls"));
            context.CreatePermission(PermissionNames.Pages_Leads, L("Leads"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, PASRConsts.LocalizationSourceName);
        }

     }
}
