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
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Administration, L("Administration"));
            #region UserPemissions
                var userManagement = context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
                userManagement.CreateChildPermission(PermissionNames.List_Users, L("List Users"));
                userManagement.CreateChildPermission(PermissionNames.Update_Users, L("Update Users"));
                userManagement.CreateChildPermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            #endregion
            #region LeadPemissions
                var leadManagement = context.CreatePermission(PermissionNames.Pages_Leads, L("Leads"));
                leadManagement.CreateChildPermission(PermissionNames.List_Leads, L("List Leads"));
                leadManagement.CreateChildPermission(PermissionNames.Update_Leads, L("Update Leads"));
            #endregion
            #region RolePemissions
                var roleManagement = context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
                roleManagement.CreateChildPermission(PermissionNames.List_Roles, L("List Roles"));
                roleManagement.CreateChildPermission(PermissionNames.Update_Roles, L("Update Roles"));
            #endregion
            #region CallPemissions
                var callManagement = context.CreatePermission(PermissionNames.Pages_Calls, L("Calls"));
                callManagement.CreateChildPermission(PermissionNames.List_Calls, L("List Calls"));
                callManagement.CreateChildPermission(PermissionNames.Update_Calls, L("Update Calls"));
                callManagement.CreateChildPermission(PermissionNames.Create_Calls, L("Create Calls"));
            #endregion
            #region TeamPemissions
                var teamManagement = context.CreatePermission(PermissionNames.Pages_Teams, L("Teams"));
                teamManagement.CreateChildPermission(PermissionNames.List_Teams, L("List Teams"));
                teamManagement.CreateChildPermission(PermissionNames.Update_Teams, L("Update Teams"));
            #endregion

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, PASRConsts.LocalizationSourceName);
        }

     }
     
}
