using Abp.AspNetCore.Mvc.ViewComponents;

namespace PASR.Web.Views
{
    public abstract class PASRViewComponent : AbpViewComponent
    {
        protected PASRViewComponent()
        {
            LocalizationSourceName = PASRConsts.LocalizationSourceName;
        }
    }
}
