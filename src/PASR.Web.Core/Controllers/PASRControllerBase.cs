using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace PASR.Controllers
{
    public abstract class PASRControllerBase: AbpController
    {
        protected PASRControllerBase()
        {
            LocalizationSourceName = PASRConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
