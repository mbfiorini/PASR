using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using PASR.Authorization;

namespace PASR.Web.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// </summary>
    public class PASRNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Home,
                        L("HomePage"),
                        url: "",
                        icon: "fas fa-home",
                        requiresAuthentication: true,
                        order: 0
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Administration,
                        L("Administration"),
                        icon: "fas fa-building",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Administration),
                        order: 1
                    ).AddItem(
                        new MenuItemDefinition(
                            PageNames.Users,
                            L("Users"),
                            icon: "fas fa-users",
                            url: "Users",
                            permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users)
                            )
                        ).AddItem(
                        new MenuItemDefinition(
                            PageNames.Roles,
                            L("Roles"),
                            icon: "fas fa-theater-masks",
                            url: "Roles",
                            permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Roles)
                            )
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Leads,
                        L("Leads"),
                        url: "Lead",
                        icon: "fas fa-address-card",
                        order: 2
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Teams,
                        L("Teams"),
                        url: "Teams",
                        icon: "fas fa-user-friends",
                        order: 3
                    )
                ).AddItem( // Menu items below is just for demonstration!
                    new MenuItemDefinition(
                        PageNames.Calls,
                        L("Calls"),
                        icon: "fas fa-phone",
                        order: 4
                    )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, PASRConsts.LocalizationSourceName);
        }
    }
}
